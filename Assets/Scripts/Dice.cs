using System.Collections;
using UnityEngine;

namespace ReincarnationCultivation
{
    public static class Dice
    {
        public static int D6()
        {
            return Random.Range(1,7);
        }
        public static int D12()
        {
            return Random.Range(1,13);
        }
    }
}