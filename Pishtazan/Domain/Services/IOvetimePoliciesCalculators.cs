using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IOvetimePoliciesCalculators
    {
        int CalcurlatorA(int salary);
        int CalcurlatorB(int salary);
        int CalcurlatorC(int salary);
    }
}
