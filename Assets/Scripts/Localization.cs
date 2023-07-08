using UnityEngine;
using System.Collections.Generic;
using ReincarnationCultivation.Type;

namespace ReincarnationCultivation
{
    public enum Language
    {
        en,
        zh
    }
    public static class Localization
    {
        public class Config : Type.IdConfig
        {
            public string id;
            public string zh;
            public string en;

            string IdConfig.id => id;
        }
        public static Language language = Language.zh;

        static Dictionary<string,Config> data;
        
        public static void Init(Dictionary<string,Config> _data)
        {
            data = _data;
        }

        static string GetString(Config config)
        {
            if(language==Language.zh)
            {
                return config.zh;
            }
            return config.en;
        }

        public static string Get(string id)
        {
            return data.TryGetValue(id,out var value) ? GetString(value) : null;
        }
    }
}