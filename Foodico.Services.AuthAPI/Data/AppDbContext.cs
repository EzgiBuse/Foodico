﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foodico.Services.AuthAPI.Data
{
   
        public class AppDbContext : IdentityDbContext<IdentityUser>
        {
            
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
           

            //protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{base.OnModelCreating(modelBuilder);
               
            //}
        }
    
}
