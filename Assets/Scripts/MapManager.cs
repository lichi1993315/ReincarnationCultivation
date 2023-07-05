using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ReincarnationCultivation
{
    public class MapManager : MonoBehaviour
    {
        public Button mapButton;
        public GameObject world;
        public Region[] regions;

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