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
        void Start()
        {
            var _ui = ui ?? GetComponent<TextMeshProUGUI>();
            if(_ui)
            {
                _ui.text = Localization.Get(key);
            }
        }
    }
}
