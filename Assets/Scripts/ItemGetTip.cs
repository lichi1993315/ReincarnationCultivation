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
            var values = new List<string>();
            if(item.physique!=null)
            {
                values.Add( Localization.Get("physique") +"+"+item.physique.Value);
            }
            if(item.strength!=null)
            {
                values.Add( Localization.Get("strength") +"+"+item.strength.Value);
            }
            if(item.agility!=null)
            {
                values.Add( Localization.Get("agility") +"+"+item.agility.Value);
            }
            if(item.refining_equipment!=null)
            {
                values.Add( Localization.Get("refining_equipment") +"+"+item.refining_equipment.Value);
            }
            if(item.refining_pills!=null)
            {
                values.Add( Localization.Get("refining_pills") +"+"+item.refining_pills.Value);
            }
            if(item.cultivation!=null)
            {
                values.Add( Localization.Get("cultivation") +"+"+item.cultivation.Value);
            }
            descriptionText.text = string.Join("\n",values);
            animator.Play("GetItem",0,0);
        }
    }
}