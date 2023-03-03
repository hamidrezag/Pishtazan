using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ParserServices : IParser
    {
        private readonly IJsonParser _jsonParser;
        private readonly IXmlParser _xmlParser;
        private readonly ICSVParser _csvParser;
        private readonly ICustomParser _customParser;

        public ParserServices(IJsonParser jsonParser, IXmlParser xmlParser, ICSVParser csvParser, ICustomParser customParser)
        {
            _jsonParser = jsonParser;
            _xmlParser = xmlParser;
            _csvParser = csvParser;
            _customParser = customParser;
        }
        public List<PersonnelInfo> Parse(string type, string data)
        {
            switch (type.ToLower())
            {
                case "json":
                    return _jsonParser.Parse(data);
                case "csv":
                    return _csvParser.Parse(data);
                case "xml":
                    return _xmlParser.Parse(data);
                case "custom":
                    return _customParser.Parse(data);
                default:
                    {
                        throw new Exception("Invalid Type Of Parser");
                    }

            }
        }
    }
}
