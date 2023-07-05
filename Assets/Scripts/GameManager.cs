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

        public Sprite[] characterPortrait;
        public Sprite[] characterMapIcon;
        public Sprite[] itemIcons;
        

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
            itemMap = ReadConfig<ItemConfig>(item_config.text);
            regionMap = ReadConfig<RegionConfig>(region_config.text);
            var story = JsonConvert.DeserializeObject<StoryConfig[]>(story_config.text);
            storyMap = new Dictionary<string,StoryConfig> (story.Select(e=> KeyValuePair.Create(e.id,e))) ;
            npcStoryConfigs = JsonConvert.DeserializeObject<NpcStoryConfig[]>(npcStory.text);
        }
        void Awake()
        {
            ReadConfig();
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