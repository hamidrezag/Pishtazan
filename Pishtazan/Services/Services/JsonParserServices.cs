using Domain.Entities;
using Domain.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class JsonParserServices : IJsonParser
    {
        public List<PersonnelInfo> Parse(string data)
        {
            return JsonConvert.DeserializeObject<List<PersonnelInfo>>(data, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" });
        }
    }
}
