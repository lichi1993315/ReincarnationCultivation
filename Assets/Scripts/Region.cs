using UnityEngine;
using UnityEngine.UI;

namespace ReincarnationCultivation
{
    public class Region : MonoBehaviour
    {
        public string id => gameObject.name;
        public ColliderButton enterButton;
        public Transform[] npcPosition;
    }
}