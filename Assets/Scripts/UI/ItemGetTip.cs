using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReincarnationCultivation.Type;
using TMPro;

namespace ReincarnationCultivation
{
    public class ItemGetTip : MonoBehaviour
    {
        public Animator animator;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public Image icon;
        public void Show(ItemConfig item)
        {
            icon.sprite = item.icon;
            nameText.text = item.name;
            descriptionText.text = string.Join("\n",item.description);
            animator.Play("GetItem",0,0);
        }
    }
}