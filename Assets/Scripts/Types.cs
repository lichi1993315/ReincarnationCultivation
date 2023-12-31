using System.Collections.Generic;
using UnityEngine;

namespace ReincarnationCultivation.Type
{
    public interface IdConfig
    {
        string id{get;}
    }
    
    public class ItemConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        public string id;
        public string name_zh;
        public string name_en;
        public string name
        {
            get
            {
                if(Localization.language==Language.zh)
                {
                    return name_zh;
                }
                return name_en;
            }
        }
        public int? physique;
        public int? strength;
        public int? agility;
        public int? refining_equipment;
        public int? refining_pills;
        public int? cultivation;
        public Sprite icon;
        public List<string> description
        {
            get
            {
                var values = new List<string>();
                var item = this;
                if(item.physique!=null)
                {
                    values.Add( Localization.Get("physique") +"+"+item.physique.Value);
                }
                if(item.strength!=null)
                {
                    values.Add( Localization.Get("strength") +"+"+item.strength.Value);
                }
                if(item.agility!=null)
                {
                    values.Add( Localization.Get("agility") +"+"+item.agility.Value);
                }
                if(item.refining_equipment!=null)
                {
                    values.Add( Localization.Get("refining_equipment") +"+"+item.refining_equipment.Value);
                }
                if(item.refining_pills!=null)
                {
                    values.Add( Localization.Get("refining_pills") +"+"+item.refining_pills.Value);
                }
                if(item.cultivation!=null)
                {
                    values.Add( Localization.Get("cultivation") +"+"+item.cultivation.Value);
                }
                return values;
            }
        }
    }
    public class FailConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        public string id;
        public string name_zh;
        public string name_en;
        public string name
        {
            get
            {
                if(Localization.language==Language.zh)
                {
                    return name_zh;
                }
                return name_en;
            }
        }
        public int? physique;
        public int? strength;
        public int? agility;
        public int? refining_equipment;
        public int? refining_pills;
        public int? cultivation;
    }
    public class RegionConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        public string id;
        public string name_zh;
        public string name_en;
        public string name
        {
            get
            {
                if(Localization.language==Language.zh)
                {
                    return name_zh;
                }
                return name_en;
            }
        }
    }
    public class InteractableConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        public string id;
        public string name_zh;
        public string name_en;
        public string name
        {
            get
            {
                if(Localization.language==Language.zh)
                {
                    return name_zh;
                }
                return name_en;
            }
        }
        public Sprite portrait;
        public Sprite mapIcon;
    }
    public enum CharacterAttribute
    {
        // 体质
        physique,
        // 力量
        strength,
        // 敏捷
        agility,
        // 炼器
        refining_equipment,
        // 炼丹
        refining_pills,
        // 修为
        cultivation
    }
    
    [System.Serializable]
    public class StoryConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        [System.Serializable]
        public class MissionConfig
        {
            public string type;
            public string name_zh;
            public string name_en;
            public string name
            {
                get
                {
                    if(Localization.language==Language.zh)
                    {
                        return name_zh;
                    }
                    return name_en;
                }
            }
            public string condition;
            public CharacterAttribute? attribute;
            public int? threshold;
            public string succeedId;
            public string failId;
            public string awardId;
            public string punishId;
        }
        [System.Serializable]
        public class DialogueConfig
        {
            public string[] content_zh;
            public string[] content_en;
            public string[] content
            {
                get
                {
                    if(Localization.language==Language.zh)
                    {
                        return content_zh;
                    }
                    return content_en;
                }
            }
            public MissionConfig mission;
        }
        [System.Serializable]
        public class NpcConfig
        {
            public string id;
            public string regionId;
            public DialogueConfig dialogue;
        }
        public string id;
        public NpcConfig[] npc;
    }
    
    [System.Serializable]
    public class NpcStoryConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        [System.Serializable]
        public class MissionConfig
        {
            public string type;
            public string name_zh;
            public string name_en;
            public string name
            {
                get
                {
                    if(Localization.language==Language.zh)
                    {
                        return name_zh;
                    }
                    return name_en;
                }
            }
            public string condition;
            public CharacterAttribute? attribute;
            public int? threshold;
            public string succeedId;
            public string failId;
            public string awardId;
            public string punishId;
            /// <summary>
            /// 是否是需要上交物品的任务
            /// </summary>
            public bool needSubmit;
        }
        [System.Serializable]
        public class DialogueConfig
        {
            public string[] content_zh;
            public string[] content_en;
            public string[] content
            {
                get
                {
                    if(Localization.language==Language.zh)
                    {
                        return content_zh;
                    }
                    return content_en;
                }
            }
            public MissionConfig mission;
        }
        
        public string id;
        public string regionId;
        public DialogueConfig[] dialogue;
    }
}