using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class GetRangeDto:PaginationReqDto
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}
