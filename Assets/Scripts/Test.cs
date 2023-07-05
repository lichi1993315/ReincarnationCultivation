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
    public class Test : MonoBehaviour
    {
        public TextAsset config;
        [ContextMenu("TestCSVConfig")]
        void TestCSVConfig()
        {
            using (var csv = new CsvReader(new StringReader(config.text), CultureInfo.InvariantCulture))
            {
                csv.Configuration.MemberTypes = CsvHelper.Configuration.MemberTypes.Fields;
                var records = csv.GetRecords<ItemConfig>();
                records.ToList().ForEach(e=>Debug.Log(
                    $"{e.id} {e.name_zh}  {e.name_en} {e.physique} {e.strength}"
                    ));
            }
        }
        public TextAsset story;
        [ContextMenu("TestStory")]
        void TestStory()
        {
            
            var value = JsonConvert.DeserializeObject<StoryConfig[]>(story.text);
            Debug.Log(JsonConvert.SerializeObject(value));
        }
        
        public TextAsset npcStory;
        [ContextMenu("GetTaskContent")]
        Dictionary<string, NpcStoryConfig.DialogueConfig> GetTaskContent(string npc, PlayerData data)
        {
            var value = JsonConvert.DeserializeObject<NpcStoryConfig[]>(npcStory.text);
            var storyMap = new Dictionary<string,NpcStoryConfig> (value.Select(e=> KeyValuePair.Create(e.id,e))) ;
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