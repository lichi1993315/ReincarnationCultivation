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
        public int? physique;
        public int? strength;
        public int? agility;
        public int? refining_equipment;
        public int? refining_pills;
        public int? cultivation;
    }
    public class FailConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        public string id;
        public string name_zh;
        public string name_en;
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
    }
    public class InteractableConfig:IdConfig
    {
        string IdConfig.id {get=>id;}
        public string id;
        public string name_zh;
        public string name_en;
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
}