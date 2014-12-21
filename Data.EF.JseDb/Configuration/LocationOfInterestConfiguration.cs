using System.Data.Entity.ModelConfiguration;
using Model.Entities;

namespace Data.EF.JseDb.Configuration
{
    public class LocationOfInterestConfiguration : EntityTypeConfiguration<JobLocation>
    {
        public LocationOfInterestConfiguration()
        {
            Property(c => c.Longitude).HasPrecision(18, 10);
            Property(c => c.Latitude).HasPrecision(18, 10);
        }
    }
}