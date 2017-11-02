using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;

    public class ProfileContext : DbContext
    {
        public ProfileContext (DbContextOptions<ProfileContext> options)
            : base(options)
        {
        }

        public DbSet<HRApplication.Models.Profile> Profile { get; set; }
    }
