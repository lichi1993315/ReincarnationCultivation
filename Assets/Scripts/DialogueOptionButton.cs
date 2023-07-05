using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ReincarnationCultivation
{
    public class DialogueOptionButton : MonoBehaviour
    {
        public TextMeshProUGUI textUI;
        public Button button;

        public System.Action OnClick;

        public string text
        {
            get=>textUI.text;
            set=>textUI.text = value;
        }

        void Awake()
        {
            button.onClick.AddListener(_OnClick);
        }

        void _OnClick()
        {
            OnClick?.Invoke();
        }
    }
}