using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace ReincarnationCultivation
{
    public class AttributeUI : MonoBehaviour
    {
        public TextMeshProUGUI physique;
        public TextMeshProUGUI strength;
        public TextMeshProUGUI agility;
        public TextMeshProUGUI refining_equipment;
        public TextMeshProUGUI refining_pills;
        public TextMeshProUGUI cultivation;

        public void UpdateUI(PlayerData data)
        {
            physique.text = data.physique.ToString();
            strength.text = data.strength.ToString();
            agility.text = data.agility.ToString();
            refining_equipment.text = data.refining_equipment.ToString();
            refining_pills.text = data.refining_pills.ToString();
            cultivation.text = data.cultivation.ToString();
        }
    }
}
