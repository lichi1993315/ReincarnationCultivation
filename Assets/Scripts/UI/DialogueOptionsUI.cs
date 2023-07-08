using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ReincarnationCultivation
{
    public class DialogueOptionsUI : MonoBehaviour
    {
        public class OptiDialogueOptionInfo
        {
            public string text;
            public System.Action onSelected;
        }
        public DialogueOptionButton buttonPrefab;
        public RectTransform contentTransform;
        public void ShowOptions(OptiDialogueOptionInfo[] options)
        {
            Clear();
            gameObject.SetActive(true);
            foreach(var info in options)
            {
                CreateOptions(info);
            }

        }
        void CreateOptions(OptiDialogueOptionInfo info)
        {
            var option = Instantiate(buttonPrefab);
            option.transform.SetParent(contentTransform,false);
            option.text = info.text;
            option.OnClick = ()=>{ Hide();info.onSelected.Invoke();};
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        void Clear()
        {
            for(var i=0;i<contentTransform.childCount;++i)
            {
                Destroy(contentTransform.GetChild(i).gameObject);
            }
        }
    }
}