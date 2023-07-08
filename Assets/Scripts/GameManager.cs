using UnityEngine;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using ReincarnationCultivation.Type;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReincarnationCultivation
{
    public class GameManager : MonoBehaviour
    {
        public TextAsset fail_config;
        public TextAsset interactable_config;
        public TextAsset item_config;
        public TextAsset region_config;
        public TextAsset story_config;
        public TextAsset npcStory;
        public TextAsset localization_config;

        public Dictionary<string,FailConfig> failMap;
        public Dictionary<string,InteractableConfig> interactableMap;
        public Dictionary<string,ItemConfig> itemMap;
        public Dictionary<string,RegionConfig> regionMap;
        public Dictionary<string,StoryConfig> storyMap;
        public NpcStoryConfig[] npcStoryConfigs;

        public MapManager mapManager;
        public PlayerManager playerManager;
        public DialogList dialogListUI;
        public DialogueOptionsUI dialogueOptionsUI;

        /// <summary>
        /// 立绘素材
        /// </summary>
        public Sprite[] characterPortrait;
        /// <summary>
        /// 地图上的可交互素材
        /// </summary>
        public Sprite[] interactableMapIcon;
        /// <summary>
        /// 道具图标
        /// </summary>
        public Sprite[] itemIcons;

        public MapInteractable mapCharacterPrefab;
        

        Dictionary<string,T> ReadConfig<T>(string text) where T:IdConfig
        {
            using (var csv = new CsvReader(new StringReader(text), CultureInfo.InvariantCulture))
            {
                csv.Configuration.MemberTypes = CsvHelper.Configuration.MemberTypes.Fields;
                var records = csv.GetRecords<T>();
                return new Dictionary<string,T> (records.Select(e=> KeyValuePair.Create(e.id,e))) ;
            }
        }

        void ReadConfig()
        {
            failMap = ReadConfig<FailConfig>(fail_config.text);
            interactableMap = ReadConfig<InteractableConfig>(interactable_config.text);
            interactableMap.Values.ToList().ForEach(e=>{
                e.portrait = System.Array.Find(characterPortrait,p=>p.name==e.id);
                e.mapIcon = System.Array.Find(interactableMapIcon,p=>p.name==e.id);
            });
            itemMap = ReadConfig<ItemConfig>(item_config.text);
            itemMap.Values.ToList().ForEach(e=>{
                e.icon = System.Array.Find(itemIcons,p=>p.name==e.id);
            });
            regionMap = ReadConfig<RegionConfig>(region_config.text);
            var story = JsonConvert.DeserializeObject<StoryConfig[]>(story_config.text);
            storyMap = new Dictionary<string,StoryConfig> (story.Select(e=> KeyValuePair.Create(e.id,e))) ;
            npcStoryConfigs = JsonConvert.DeserializeObject<NpcStoryConfig[]>(npcStory.text);

            Localization.Init( ReadConfig<Localization.Config>(localization_config.text) );
        }


        MapInteractable CreateMapInteractable(InteractableConfig config)
        {
            var character = Instantiate(mapCharacterPrefab);
            character.id = config.id;
            character.spriteRenderer.sprite = config.mapIcon;
            character.characterName = config.name;
            return character;
        }
        void CreateNPCs()
        {
            mapManager.SetNPC( interactableMap.Values.Select(CreateMapInteractable).ToArray() );
            mapManager.OnInteractableSelected = OnInteractableSelect;
        }
        void Awake()
        {
            ReadConfig();
            CreateNPCs();
            SetStory("S");
        }
        void OnInteractableSelect(string interactableId)
        {
            Debug.Log("OnInteractableSelect "+interactableId);
            var interactable = interactableMap[interactableId];
            dialogListUI.ShowDialog(
                new DialogList.DialogInfo[]{
                    new DialogList.DialogInfo(){
                        content = Localization.Get("ask"),
                        character = interactable.portrait,
                        characterName = interactable.name,
                    }
                }
            );
            dialogListUI.OnEnd = ()=>ShowDialogueOptions(interactableId);
            dialogListUI.OnEndClick = null;
        }
        void ShowDialogueOptions(string interactableId)
        {
            Debug.Log("ShowDialogueOptions "+interactableId);
            ShowDialogueOptions( GetTaskContent(interactableId,playerManager.data).Values.ToArray() );
        }
        void ShowDialogueOptions(NpcStoryConfig.DialogueConfig[] configs)
        {
            var options = configs.Select(e=>new DialogueOptionsUI.OptiDialogueOptionInfo(){
                text = e.mission.name,
                onSelected = ()=>SelectDialogueOption(e)
            }).ToList();
            options.Add(new DialogueOptionsUI.OptiDialogueOptionInfo(){
                text = Localization.Get("cancel"),
                onSelected = ()=> dialogListUI.Hide()
            });
            dialogueOptionsUI.ShowOptions(options.ToArray());
        }

        void AddItem(ItemConfig item)
        {
            playerManager.AddItem( item );
        }

        void OnSucceed(NpcStoryConfig.MissionConfig mission)
        {
            if(mission.awardId!=null)
            {
                AddItem(itemMap[mission.awardId]);
            }
            SetStory(mission.succeedId);
            dialogListUI.Hide();
        }
        void OnFailed(NpcStoryConfig.MissionConfig mission)
        {
            SetStory(mission.failId);
            dialogListUI.Hide();
        }
        void CheckSuccess(NpcStoryConfig.MissionConfig mission)
        {
            var point = Mathf.Max(0,mission.threshold.Value - playerManager.GetThreshold(mission)) ;
            dialogListUI.ContinueDialog(string.Format(Localization.Get("dice_tip"),point));
            dialogListUI.OnEnd = null;
            dialogListUI.OnEndClick =()=> {
                var d  = Dice.D12();
                if(d>=point)
                {
                    dialogListUI.ContinueDialog($"{d} "+Localization.Get("success"));
                    dialogListUI.OnEndClick = ()=>OnSucceed(mission);
                }
                else
                {
                    dialogListUI.ContinueDialog($"{d} "+Localization.Get("fail"));
                    dialogListUI.OnEndClick = ()=>OnFailed(mission);
                }
            };
        }
        void SelectDialogueOption(NpcStoryConfig.DialogueConfig config)
        {
            var content = config.content;
            var awardId = config.mission.awardId;
            playerManager.AddStory(config.mission.type);
            dialogListUI.ContinueDialog(content);
            // 临时直接给与奖励
            // content.Add($"奖励 {itemMap[awardId].name_zh}");content.ToArray());
            dialogListUI.OnEndClick =()=> {
                if(config.mission.attribute!=null)
                {
                    CheckSuccess(config.mission);
                }
                else
                {
                    OnSucceed(config.mission);
                }
            };
            dialogListUI.OnEnd = null;
        }
        public void SetStory(string storyId)
        {
            Debug.Log("SetStory "+storyId);
            SetStory(storyMap[storyId]);
        }
        public void SetStory(StoryConfig story)
        {
            mapManager.MoveNPC(story.npc);
        }
        Dictionary<string, NpcStoryConfig.DialogueConfig> GetTaskContent(string npc, PlayerData data)
        {
            var storyMap = new Dictionary<string,NpcStoryConfig> (npcStoryConfigs.Select(e=> KeyValuePair.Create(e.id,e))) ;
            var npcDialogue = new Dictionary<string, NpcStoryConfig.DialogueConfig>();
            
            if (npc == "alchemist")
            {
                var storyList = storyMap[npc].dialogue;
                foreach (var s in storyList)
                {
                    if (s.mission.type == "采药")
                    {
                        // 默认随时允许采药
                        npcDialogue.Add(s.mission.type, s);
                    }

                    if (s.mission.type == "炼丹")
                    {
                        //仅完成采药任务且炼丹属性>3,才可以炼丹
                        if (data.refining_pills <= 3) break;
                        foreach (var d in data.story)
                        {
                            if (d.Contains("采药"))
                            {
                                npcDialogue.Add(s.mission.type, s);
                                break;
                            }
                        }
                    }

                    if (s.mission.type == "修炼")
                    {
                        // 需要完成炼丹任务且修炼过五府锻元诀,且力量>8
                        
                        if (data.physique <= 8) break;
                        bool item_flag = false;
                        foreach (var item in data.items)
                        {
                            if (item.Contains("五府锻元诀"))
                            {
                                item_flag = true;
                                break;
                            }
                        }
                        bool history_flag = false;
                        foreach (var d in data.story)
                        {
                            if (d.Contains("炼丹"))
                            {
                                history_flag = true;
                                break;
                            }
                        }

                        if (history_flag && item_flag)
                        {
                            npcDialogue.Add(s.mission.type, s);
                        }
                    }
                }
            }
            
            return npcDialogue;
            
        }
    }
}