using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CsharpAssessmentSmartShare
{
    public class SmartShareContext : DbContext
    {
        // TODO define context and models
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=127.0.0.1;port=5432;database=example;userid=postgres;password=bondstone");
        }
    }

}