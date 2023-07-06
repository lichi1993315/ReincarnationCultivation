using UnityEngine;
using UnityEngine.UI;

namespace ReincarnationCultivation
{
    public class Region : MonoBehaviour
    {
        public string id => gameObject.name;
        public ColliderButton enterButton;
        public Transform[] npcPosition;

        /// <summary>
        /// 将npc移动至此区域,并随机摆放到 npcPosition
        /// </summary>
        /// <param name="npc"></param>
        public void MoveNPC(MapInteractable[] npc)
        {
            var pos = ShuffleArray(npcPosition);
            for(var i=0;i<npc.Length;++i)
            {
                npc[i].transform.parent = pos[i];
                npc[i].transform.localPosition = Vector3.zero;
            }
        }

        public T[] ShuffleArray<T>(T[] array)
        {
            T[] newArray = new T[array.Length];
            System.Array.Copy(array, newArray, array.Length);

            int n = newArray.Length;
            while (n > 1)
            {
                int k = UnityEngine.Random.Range(0, n);
                n--;
                T temp = newArray[n];
                newArray[n] = newArray[k];
                newArray[k] = temp;
            }

            return newArray;
        }
    }
}