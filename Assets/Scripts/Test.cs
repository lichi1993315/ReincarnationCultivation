using UnityEngine;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using ReincarnationCultivation.Type;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReincarnationCultivation
{
    public class Test : MonoBehaviour
    {
        public DialogueOptionsUI dialogueOptionsUI;
        [ContextMenu("TestDialogue")]
        public void TestDialogue()
        {
            dialogueOptionsUI.ShowOptions( new  DialogueOptionsUI.OptiDialogueOptionInfo[]{
                new DialogueOptionsUI.OptiDialogueOptionInfo(){
                    text = "修炼功法",
                    onSelected = ()=>Debug.Log("修炼功法")
                },
                new DialogueOptionsUI.OptiDialogueOptionInfo(){
                    text = "收集药材,炼制生骨丹",
                    onSelected = ()=>Debug.Log("收集药材,炼制生骨丹")
                }
            });
        }
    }
}