using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static ReincarnationCultivation.Type.StoryConfig;

namespace ReincarnationCultivation
{
    public class MapManager : MonoBehaviour
    {
        public Button mapButton;
        public GameObject world;
        public Region[] regions;
        public MapInteractable[] _npcs;

        public System.Action<string> OnInteractableSelected;

        GameObject _currentMap;
        public GameObject currentMap
        {
            set
            {
                _currentMap?.SetActive(false);
                value?.SetActive(true);
                _currentMap = value;
                mapButton.gameObject.SetActive(value!=world);
            }
        }

        void Awake()
        {
            mapButton.onClick.AddListener(ReturnToWorld);
            regions.ToList().ForEach(e=>e.enterButton.OnClick=()=>EnterRegion(e));
            currentMap = world;
            foreach(var npc in _npcs)
            {
                var id = npc.id;
                npc.button.OnClick =  ()=> _OnInteractableSelected(id);
            }
        }

        // /// <summary>
        // /// 设置所有地图可交互物,比如npc
        // /// </summary>
        // /// <param name="npcs"></param>
        // public void SetNPC(MapInteractable[] npcs)
        // {
        //     foreach(var npc in  npcs)
        //     {
        //         var id = npc.id;
        //         npc.button.OnClick = ()=> _OnInteractableSelected(id);
        //     }
        //     this._npcs = npcs;
        // }

        public MapInteractable GetNPC(string id)
        {
            return System.Array.Find(_npcs,e=>e.id==id);
        }

        /// <summary>
        /// 每回合后按配置移动npc
        /// </summary>
        /// <param name="npc"></param>
        public void MoveNPC(NpcConfig[] npc)
        {
            foreach(var region in regions)
            {
                var regionNpc = npc.Where(e=>e.regionId==region.id).Select(e=>GetNPC(e.id)).ToArray();
                region.MoveNPC( regionNpc );
            }
            // 剩下的npc隐藏
            _npcs.Where(e=>System.Array.FindIndex(npc,n=>n.id==e.id)<0).ToList().ForEach(e=>e.gameObject.SetActive(false));
        }

        void _OnInteractableSelected(string id)
        {
            OnInteractableSelected?.Invoke(id);
        }

        void ReturnToWorld()
        {
            currentMap = world;
        }
        void EnterRegion( Region region )
        {
            currentMap = region.gameObject;
        }
    }
}