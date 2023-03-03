using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CustomParserServices : ICustomParser
    {
        private readonly ICSVParser _csvParser;
        public CustomParserServices(ICSVParser csvParser)
        {
            _csvParser = csvParser;
        }
        public List<PersonnelInfo> Parse(string data)
        {
            return _csvParser.ParserWithSplitter(data, "/");
        }
    }
}
