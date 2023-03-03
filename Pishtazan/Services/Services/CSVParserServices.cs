using Domain.Entities;
using Domain.Services;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CSVParserServices : ICSVParser
    {
        public CSVParserServices()
        {
        }
        public List<PersonnelInfo> Parse(string data)
        {
            return ParserWithSplitter(data, ",");
        }
        public List<PersonnelInfo> ParserWithSplitter(string data,string splitter)
        {

            var header = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Take(1).ToArray();
            header[0] = header[0].Replace("\r", "");
            header[0] = header[0].Replace("\n", "");
            var headers = header[0].Split(splitter);

            var lines = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);

            List<PersonnelInfo> result = new List<PersonnelInfo>();

            foreach (var item in lines)
            {
                var itemRepired = item.Replace("\r", string.Empty);
                itemRepired = itemRepired.Replace("\n", string.Empty);

                var values = itemRepired.Split(splitter);
                var model = new PersonnelInfo();
                for (int i = 0; i < values.Count(); i++)
                {
                    var propInfo = model.GetType().GetProperties().FirstOrDefault(x => x.Name.Trim().ToLower() == headers[i].Trim().ToLower());
                    if (propInfo == null)
                        throw new Exception("Properties Not Defined : " + header[i]);
                    if (propInfo.PropertyType == typeof(int))
                        propInfo.SetValue(model, Convert.ToInt32(values[i]));
                    if (propInfo.PropertyType == typeof(DateTime))
                        propInfo.SetValue(model, Convert.ToDateTime(values[i]));
                    if (propInfo.PropertyType == typeof(string))
                        propInfo.SetValue(model, values[i]);
                }
                result.Add(model);
            }

            return result;
        }
    }
}
