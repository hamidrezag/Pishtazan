using Domain.Services;
using OvetimePolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OvetimePoliciesServices: IOvertimePoliciesServices
    {
        private readonly IOvetimePoliciesCalculators _overTimeServices;
        public OvetimePoliciesServices(IOvetimePoliciesCalculators overTimeServices)
        {
            _overTimeServices = overTimeServices;
        }
        public int Calculate(string name,int salary)
        {
            var method = _overTimeServices.GetType().GetMethod(name);
            if (method == null)
                throw new Exception("OverTime Calculator Has Error");
            object[] parameters = new object[1] ;
            parameters[0] = salary;
            return (int)method.Invoke(_overTimeServices, parameters);
        }
    }
}
