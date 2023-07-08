using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ReincarnationCultivation
{
    public class DialogUI : MonoBehaviour
    {
        public Image characterUI;
        public TextMeshProUGUI characterNameUI;
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
            ShowDialog(text,null,"测试角色");
        }

        /// <summary>
        /// 保持原有的角色设置,显示接下来的文字
        /// </summary>
        /// <param name="dialog"></param>
        public void ContinueDialog(string content)
        {
            StopAllCoroutines();
            textUI.text = "";
            text = content;
            interval = normalInterval;
            enabled = true;
            StartCoroutine(ShowText(content));
        }


        public void ShowDialog(string content,Sprite character,string characterName)
        {
            characterUI.sprite = character;
            characterUI.enabled = character!=null;
            characterNameUI.text = characterName??"";
            ContinueDialog(content);
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