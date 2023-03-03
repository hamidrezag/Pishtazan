﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public interface IBaseModel
    {
        long Id { get; set; }
        DateTime CreateDateTime { get; set; }
        DateTime? ModifiedDateTime { get; set; }
    }
}
