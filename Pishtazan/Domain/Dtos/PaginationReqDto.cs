using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class PaginationReqDto
    { 
        public string Filter { get; set; } = "";
        [Required]
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
        [Required]
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;
        [Required]
        [DefaultValue(false)]
        public bool AscSort { get; set; } = false;
        [DefaultValue("Id")]
        public string SrtField { get; set; } = "Id";
    }
}
