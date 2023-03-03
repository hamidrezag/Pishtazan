using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPersonnelInfoServices:IBaseServices<PersonnelInfo>
    {
        public List<PersonnelInfo> GetPersonnelInfoFromData(string dataType, ProcessDataAndOverTimeCalculator info);
    }
}
