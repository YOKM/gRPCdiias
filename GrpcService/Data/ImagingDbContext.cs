using GrpcService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Data
{
    public class ImagingDbContext : DbContext
    {
        public ImagingDbContext(DbContextOptions <ImagingDbContext> options) : base(options) { }
             

        public DbSet<ImagingScheduleJob> ImagingScheduleJob { get; set; }

        public DbSet<ImagingScheduleJob_Detail> imaging_JOBdetails { get; set; }


    }

}
