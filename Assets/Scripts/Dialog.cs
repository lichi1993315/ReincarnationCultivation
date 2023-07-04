using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ReincarnationCultivation
{
    public class Dialog : MonoBehaviour
    {
        public Image characterUI;
        public TextMeshProUGUI textUI;
        /// <summary>
        /// 文本播放结束的事件
        /// </summary>
        public System.Action OnEnd;
        /// <summary>
        /// 文本播放结束后点击鼠标的事件
        /// </summary>
        public System.Action OnEndClick;
        
        public string text;
        public float normalInterval = 0.2f;
        public float speedUpInterval = 0.03f;
        float interval;

        [ContextMenu("Test")]
        void Test()
        {
            ShowDialog(text,null);
        }

        public void ShowDialog(string content,Sprite character)
        {
            StopAllCoroutines();
            textUI.text = "";
            text = content;
            characterUI.sprite = character;
            characterUI.enabled = character!=null;
            interval = normalInterval;
            enabled = true;
            StartCoroutine(ShowText(content));
        }

        IEnumerator ShowText(string content)
        {
            for(var i=0;i<content.Length;++i)
            {
                textUI.text = content.Substring(0,i+1);
                yield return new WaitForSeconds(interval);
            }
            OnEnd?.Invoke();
            // enabled = false;
        }

        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(textUI.text.Length==text.Length)
                {
                    OnEndClick?.Invoke();
                }
                else
                {
                    interval = speedUpInterval;
                }
            }
        }
        
    }
}