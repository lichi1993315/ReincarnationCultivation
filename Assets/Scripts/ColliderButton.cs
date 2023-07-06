using UnityEngine;
using UnityEngine.EventSystems;

namespace ReincarnationCultivation
{
    public class ColliderButton : MonoBehaviour
    {
        public System.Action OnClick;
        void OnMouseDown() 
        {
            if(! EventSystem.current.IsPointerOverGameObject ())
            {
                OnClick?.Invoke();
            }
        }
    }
}