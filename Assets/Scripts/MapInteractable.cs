using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReincarnationCultivation
{
    /// <summary>
    /// 控制地图上的可交互物
    /// </summary>
    public class MapInteractable : MonoBehaviour
    {
        public string id;
        public string characterName;
        public ColliderButton button;
        public SpriteRenderer spriteRenderer;
    }
}
