using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class GetRangeDto : PaginationReqDto
    {
        /// <summary>
        /// از تاریخ
        /// </summary>
        /// <example>14001010</example>
        /// <param name="From">تاریخ</param>
        [MaxLength(8)]
        [DisplayName("از تاریخ")]
        [Required]
        public string From { get; set; }
        [MaxLength(8)]
        [DisplayName("تا تاریخ")]
        [Required]
        /// <summary>
        /// از تاریخ
        /// </summary>
        /// <example>14001010</example>
        /// <param name="To">تاریخ</param>
        public string To { get; set; }
    }
}
