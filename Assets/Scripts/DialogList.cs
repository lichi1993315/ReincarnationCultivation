using System.Collections;
using UnityEngine;

namespace ReincarnationCultivation
{
    public class DialogList : MonoBehaviour
    {
        [System.Serializable]
        public class DialogInfo
        {
            public string content;
            public Sprite character;
        }
        public DialogInfo[] dialogInfos;

        [ContextMenu("Test")]
        void Test()
        {
            ShowDialog(dialogInfos);
        }
        public Dialog dialogUI;
        /// <summary>
        /// 文本播放结束的事件
        /// </summary>
        public System.Action OnEnd = ()=>Debug.Log("DialogList.OnEnd");
        /// <summary>
        /// 文本播放结束后点击鼠标的事件
        /// </summary>
        public System.Action OnEndClick = ()=>Debug.Log("DialogList.OnEndClick");

        public void ShowDialog(DialogInfo[] dialogInfos)
        {
            dialogUI.gameObject.SetActive(true);
            var current = 0;
            dialogUI.OnEndClick = ()=>{
                current+=1;
                if(current<dialogInfos.Length)
                {
                    ShowDialog(dialogInfos[current]);
                }
                else
                {
                    OnEndClick.Invoke();
                }
            };
            dialogUI.OnEnd = ()=>{
                if(current==dialogInfos.Length-1)
                {
                    OnEnd?.Invoke();
                }

            };
            ShowDialog(dialogInfos[0]);
        }
        void ShowDialog(DialogInfo dialogInfo)
        {
            dialogUI.ShowDialog(dialogInfo.content,dialogInfo.character);
        }
    }
}