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
            public string characterName;
        }
        // public DialogInfo[] dialogInfos;

        // [ContextMenu("Test")]
        // void Test()
        // {
        //     ShowDialog(dialogInfos);
        // }
        public DialogUI dialogUI;
        /// <summary>
        /// 文本播放结束的事件
        /// </summary>
        public System.Action OnEnd = ()=>Debug.Log("DialogList.OnEnd");
        /// <summary>
        /// 文本播放结束后点击鼠标的事件
        /// </summary>
        public System.Action OnEndClick = ()=>Debug.Log("DialogList.OnEndClick");

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 保持原有的角色设置,显示接下来的文字
        /// </summary>
        /// <param name="dialog"></param>
        public void ContinueDialog(string[] dialog)
        {
            gameObject.SetActive(true);
            dialogUI.gameObject.SetActive(true);
            var current = 0;
            dialogUI.OnEndClick = ()=>{
                current+=1;
                if(current<dialog.Length)
                {
                    dialogUI.ContinueDialog(dialog[current]);
                }
                else
                {
                    OnEndClick?.Invoke();
                }
            };
            dialogUI.OnEnd = ()=>{
                if(current==dialog.Length-1)
                {
                    OnEnd?.Invoke();
                }

            };
            dialogUI.ContinueDialog(dialog[0]);
        }

        public void ShowDialog(DialogInfo[] dialogInfos)
        {
            gameObject.SetActive(true);
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
                    OnEndClick?.Invoke();
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
            dialogUI.ShowDialog(dialogInfo.content,dialogInfo.character,dialogInfo.characterName);
        }
    }
}