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
    [System.Serializable]
    public class PlayerData
    {
        // 体质
        public int physique;
        // 力量
        public int strength;
        // 敏捷
        public int agility;
        // 炼器
        public int refining_equipment;
        // 炼丹
        public int refining_pills;
        // 修为
        public int cultivation;
        // 循环次数。第一次进入剧情时为0
        public int loopCount;
        // 道具
        public string[] items = new string[]{};
        // 历史故事线id数组
        public string[] story = new string[]{};
        // 单日回合数，目前每天三个行动点数。0 1 2 代表早中晚
        public int time;
        public int day;
        public int remain_turn = 15;
        public int turn = 0;
        public int max_turn = 15;
    }
    public class PlayerManager : MonoBehaviour
    {
        public AttributeUI attributeUI;
        public PlayerData data;
        public List<ItemConfig> items = new List<ItemConfig>();
        public List<string> story;
        public ItemGetTip itemGetTip;
        void Start()
        {
            attributeUI.UpdateUI(data);
        }
        public void OnReincarnation()
        {
            data.turn = 0;
            data.day = 0;
            data.time = 0;
            data.loopCount+=1;
            data.remain_turn = data.max_turn;
            data.story = new string[]{};
        }
        public void AddStory(string id)
        {
            story.Add(id);
            data.story = story.ToArray();
            data.time+=1;
            if(data.time>=3)
            {
                data.time = 0;
                data.day +=1;
            }
            data.remain_turn -= 1;
            data.turn += 1;
            if(data.remain_turn<0)
            {
                data.remain_turn = 0;
            }
        }
        public void AddItem(ItemConfig item)
        {
            items.Add(item);
            data.items = items.Select(e=>e.id).ToArray();
            data.physique += item.physique??0;
            data.strength += item.strength??0;
            data.agility += item.agility??0;
            data.refining_equipment += item.refining_equipment??0;
            data.refining_pills += item.refining_pills??0;
            data.cultivation += item.cultivation??0;
            itemGetTip.Show(item);
        }
        public void RemoveAllItem()
        {
            var items = this.items.ToArray();
            foreach(var i in items)
            {
                RemoveItem(i);
            }
        }
        public void UpdateUI()
        {
            attributeUI.UpdateUI(data);
        }
        public void RemoveItem(ItemConfig item)
        {
            var index = items.IndexOf(item);
            if(index>=0)
            {
                data.physique -= item.physique??0;
                data.strength -= item.strength??0;
                data.agility -= item.agility??0;
                data.refining_equipment -= item.refining_equipment??0;
                data.refining_pills -= item.refining_pills??0;
                data.cultivation -= item.cultivation??0;
                items.RemoveAt(index);
            }
        }
        /// <summary>
        /// 获取threshold
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        public int GetThreshold(NpcStoryConfig.MissionConfig mission)
        {
            switch(mission.attribute.Value)
            {
                case CharacterAttribute.physique:return data.physique;
                case CharacterAttribute.strength:return data.strength;
                case CharacterAttribute.agility:return data.agility;
                case CharacterAttribute.refining_equipment:return data.refining_equipment;
                case CharacterAttribute.refining_pills:return data.refining_pills;
                case CharacterAttribute.cultivation:return data.cultivation;
            }
            Debug.LogError(mission.attribute.Value);
            return 0;
        }
    }
}