using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Config
{
    public class PersonnelInfoConfig : BaseConfig, IEntityTypeConfiguration<PersonnelInfo>
    {
        public void Configure(EntityTypeBuilder<PersonnelInfo> builder)
        {
        }
    }
}
