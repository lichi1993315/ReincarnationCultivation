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
        public string[] items;
        // 历史故事线id数组
        public string[] story;
        // 单日回合数，目前每天三个行动点数。0 1 2 代表早中晚
        public int time;
    }
    public class PlayerManager : MonoBehaviour
    {
        public PlayerData data;
    }
}