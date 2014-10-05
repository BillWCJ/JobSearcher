﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Data.EF.ClusterDB
{
    public class ClusterRepository : DbContext 
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>()
                .Configure(config => config.HasPrecision(18,10));
        }
        public ClusterRepository(): base()
        {
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<Disciplines> Disciplines { get; set; }
    }
}
