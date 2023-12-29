using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int> , int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)   
        {

        }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<AttachmentFile> AttachmentFile { get; set; }
    }
}
