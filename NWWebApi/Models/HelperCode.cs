using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace NWWebApi.Models
{
    public class HelperCode
    {
        public static List<accordionDB> GetCode(string fileName)
        {
            var filePathServerCode = HostingEnvironment.MapPath("~/App_Data/code/" + fileName);
            var jsonServerCode = System.IO.File.ReadAllText(filePathServerCode);
            var result = JsonConvert.DeserializeObject<List<accordionDB>>(jsonServerCode);
         
            return result;
        }
        public static List<T> GetJson<T>(string FileName)
        {
            var filePathServerCode = HostingEnvironment.MapPath("~/App_Data/" + FileName);
            var jsonServerCode = System.IO.File.ReadAllText(filePathServerCode);
            var result = JsonConvert.DeserializeObject<List<T>>(jsonServerCode);
            return result;
        }
        public static bool WriteJsont<T>(List<T> value, string FileName)
        {
            var filePath = HostingEnvironment.MapPath("~/App_Data/" + FileName);
            var json = JsonConvert.SerializeObject(value, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
            return true;
        }
    }

    public class accordionDB
    {
        public int id { get; set; }
        public string accordionName { get; set; }
        public string title { get; set; }
        public string hrefName { get; set; }
        public string href { get; set; }
        public string fileLocation { get; set; }
        public string fileContent { get; set; }
        public string group { get; set; }
        public string copyid { get; set; }
    }
    public class accordionManager
    {
        public List<accordionDB> client { get; set; }
        public List<accordionDB> server { get; set; }
    }
}
