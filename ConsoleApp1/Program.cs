using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var designDataList = GetA();

            var json = File.ReadAllText("D:\\Development\\ConsoleApp1\\ConsoleApp1\\json1.json");
            var deserializeObject = JsonConvert.DeserializeObject<List<DesignData>>(json, new JsonSerializerSettings() { Converters = { new TestConverter() } });
        }

        private static List<DesignData> GetA()
        {
            return new List<DesignData>()
            {
                new DesignData()
                {
                    Type = Type.Template,
                    Name = "Header",
                    Designs = new List<DataClass>
                    {
                            new DataClass(){ Key = "CustomerGroup", Data = "testTitle", Type = DataType.String} ,
                            new DataClass(){ Key = "CreatedDate", Data = $"(as at {DateTime.Now.ToString("dd/MM/yyyy")})", Type = DataType.String}
                    }
                },
                new DesignData()
                {
                    Type = Type.Template,
                    Name = "Logo",
                    Designs = new List<DataClass>
                    {
                        new DataClass(){ Key = "LogoWithWebLink", Data = new WebLink(){ Data = File.OpenRead("D:\\Development\\ConsoleApp1\\ConsoleApp1\\images\\samplejpg.jpg") as Stream, Url = "http://AAAAAA.com" }, Type = DataType.WebLinkWithImage},
                    }
                },
                new DesignData()
                {
                    Type = Type.Template,
                    Name = "Footer",
                    Designs = new List<DataClass>
                    {
                        new DataClass(){ Key = "webLink", Data = new WebLink(){ Data = "AAAAAA", Url = "https://AAAAAA.com" }, Type = DataType.WebLinkWithText},
                    }
                }
            };
        }
    }

    public class TestConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(System.Type objectType)
        {
            return (objectType == typeof(DataClass));
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            /*
             * How can I?
             */
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class DataClass
    {
        public string Key { get; set; }
        public object Data { get; set; }
        public DataType Type { get; set; }
        public bool IsCondition { get; set; }
    }
    public class DesignData
    {

        public Type Type { get; set; }
        public string Name { get; set; }
        public IEnumerable<DataClass> Designs { get; set; }
    }

    public class WebLink
    {
        public object Data { get; set; }
        public string Url { get; set; }
    }

    public enum DataType
    {
        String = 0,
        Boolean = 1,
        Image = 2,
        PageNumber = 3,
        WebLink = 4,
        WebLinkWithText = 5,
        WebLinkWithImage = 6,
        Condition = 7
    }

    public enum Type
    {
        Product = 0,
        Group = 1,
        Template = 2,
        Component = 3
    }
}
