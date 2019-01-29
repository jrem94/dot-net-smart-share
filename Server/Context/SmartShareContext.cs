using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace CsharpAssessmentSmartShare
{
    public class SmartShareContext : DbContext
    {
        public DbSet<Model> Files { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //My pgAdmin server is set to port 8080. This line of code may need to be changed in order to connect on another system.
            optionsBuilder.UseNpgsql("server=127.0.0.1;port=8080;database=smartShare;userid=postgres;password=bondstone");
        }
    }

}