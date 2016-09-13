﻿using FCGagarin.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.DAL.Concrete
{
    public class FCGagarinContext : DbContext
    {
        public FCGagarinContext() : base("Name=FCGagarinContext") { }
        
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Training> Trainings { get; set; }
    }
}
