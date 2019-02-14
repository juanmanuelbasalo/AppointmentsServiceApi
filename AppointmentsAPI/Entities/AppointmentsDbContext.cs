using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    public class AppointmentsDbContext : DbContext
    {
        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Appointment>();
            modelBuilder.Entity<AppointmentsAdmin>();
            modelBuilder.Entity<AppointmentsClient>().HasKey(k => k.Id);
            modelBuilder.Entity<DetailsAppointments>();
            modelBuilder.Entity<StatusAppointments>();
        }

    }
}
