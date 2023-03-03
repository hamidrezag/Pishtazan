using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IParser
    {
        public List<PersonnelInfo> Parse(string type, string data);
        
    }
}
