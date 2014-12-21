using System.Data.Entity.ModelConfiguration;
using Model.Entities;

namespace Data.EF.JseDb.Configuration
{
    public class JobLocationConfiguration : EntityTypeConfiguration<JobLocation>
    {
        public JobLocationConfiguration()
        {
            Property(c => c.Longitude).HasPrecision(18, 10);
            Property(c => c.Latitude).HasPrecision(18, 10);
        }
    }
}
