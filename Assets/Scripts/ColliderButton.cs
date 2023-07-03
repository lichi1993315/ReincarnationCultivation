using UnityEngine;

namespace ReincarnationCultivation
{
    public class ColliderButton : MonoBehaviour
    {
        public System.Action OnClick;
        void OnMouseDown() 
        {
            OnClick?.Invoke();
        }
    }
}