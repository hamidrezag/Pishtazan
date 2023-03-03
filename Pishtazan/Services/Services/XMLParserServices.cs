using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Services.Services
{
    public class XMLParserServices : IXmlParser
    {
        public List<PersonnelInfo> Parse(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PersonnelInfo>), new XmlRootAttribute("Employee"));
            using (TextReader reader = new StringReader(data))
            {
                var res =  (List<PersonnelInfo>)serializer.Deserialize(reader);
                return res;
            }
        }
    }
}
