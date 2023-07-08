using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using ReincarnationCultivation.Type;

namespace ReincarnationCultivation
{
    public class ItemUI : MonoBehaviour, IPointerEnterHandler
    {
        public Image icon;
        public TextMeshProUGUI nameText;
        ItemConfig _config;
        public ItemConfig config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
                icon.sprite = value.icon;
                nameText.text = value.name;
            }
        }
        public Toggle toggle;
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerOver?.Invoke();
        }

        public System.Action OnPointerOver;
    }
}