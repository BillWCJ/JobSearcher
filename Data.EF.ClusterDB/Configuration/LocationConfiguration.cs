using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Data.EF.ClusterDB.Configuration
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            Property(c => c.Longitude).HasPrecision(18, 10);
            Property(c => c.Latitude).HasPrecision(18, 10);
        }
    }
}
