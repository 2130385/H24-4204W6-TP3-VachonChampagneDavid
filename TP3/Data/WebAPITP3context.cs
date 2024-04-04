using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TP3.Models;

namespace Labo8api.Data
{
    public class WebAPITP3context : IdentityDbContext<User>
    {
        public WebAPITP3context(DbContextOptions<WebAPITP3context> options)
            : base(options)
        {
        }

        public DbSet<TP3.Models.Score> Score { get; set; } = default!;
    }
}
