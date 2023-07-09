using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace ReincarnationCultivation
{
    public class LocalizationText : MonoBehaviour
    {
        public string key;
        public TextMeshProUGUI ui;
        public TextMeshPro ui2;
        void Start()
        {
            var _ui = ui ?? GetComponent<TextMeshProUGUI>();
            if(_ui)
            {
                _ui.text = Localization.Get(key);
            }
            var _ui2 = ui2 ?? GetComponent<TextMeshPro>();
            if(_ui2)
            {
                _ui2.text = Localization.Get(key);
            }
        }
    }
}
