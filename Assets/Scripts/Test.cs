using UnityEngine;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using ReincarnationCultivation.Type;
using Newtonsoft.Json;

namespace ReincarnationCultivation
{
    public class Test : MonoBehaviour
    {
        public TextAsset config;
        [ContextMenu("TestCSVConfig")]
        void TestCSVConfig()
        {
            using (var csv = new CsvReader(new StringReader(config.text), CultureInfo.InvariantCulture))
            {
                csv.Configuration.MemberTypes = CsvHelper.Configuration.MemberTypes.Fields;
                var records = csv.GetRecords<ItemConfig>();
                records.ToList().ForEach(e=>Debug.Log(
                    $"{e.id} {e.name_zh}  {e.name_en} {e.physique} {e.strength}"
                    ));
            }
        }
        public TextAsset story;
        [ContextMenu("TestStory")]
        void TestStory()
        {
            
            var value = JsonConvert.DeserializeObject<StoryConfig[]>(story.text);
            Debug.Log(JsonConvert.SerializeObject(value));
        }
    }
}