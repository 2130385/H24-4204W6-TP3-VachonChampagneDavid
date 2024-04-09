using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TP3.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Labo8api.Data
{
    public class WebAPITP3context : IdentityDbContext<User>
    {
        public WebAPITP3context(DbContextOptions<WebAPITP3context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User u1 = new User
            {
                Id = "fea253ae-b7a8-487f-8263-fc1fd99abcd7",
                Email = "user1@gmail.com",
                UserName = "User1",
                NormalizedEmail="USER1@GMAIL.COM",
                NormalizedUserName="USER1"
            };
            u1.PasswordHash = hasher.HashPassword(u1, "Passw0rd!");
            User u2 = new User
            {
                Id = "4b0a7226-b081-496f-b996-4f37cee69a48",
                Email = "user2@gmail.com",
                UserName = "User2",
                NormalizedEmail = "USER2@GMAIL.COM",
                NormalizedUserName = "USER2"
            };
            u2.PasswordHash = hasher.HashPassword(u2, "Passw0rd!");

            builder.Entity<User>().HasData(u1);
            builder.Entity<User>().HasData(u2);

            builder.Entity<Score>().HasData(
                new Score { Id = 1, Pseudo = "User1", Date = DateTime.Now.ToString(), TimeInSeconds = "10", ScoreValue = 10, IsPublic = true, UserId= "fea253ae-b7a8-487f-8263-fc1fd99abcd7" },
                new Score { Id = 2, Pseudo = "User1", Date = DateTime.Now.ToString(), TimeInSeconds = "25", ScoreValue = 25, IsPublic = false, UserId = "fea253ae-b7a8-487f-8263-fc1fd99abcd7" },
                new Score { Id = 3, Pseudo = "User2", Date = DateTime.Now.ToString(), TimeInSeconds = "5", ScoreValue = 5, IsPublic = true, UserId = "4b0a7226-b081-496f-b996-4f37cee69a48" },
                new Score { Id = 4, Pseudo = "User2", Date = DateTime.Now.ToString(), TimeInSeconds = "30", ScoreValue = 30, IsPublic = false, UserId = "4b0a7226-b081-496f-b996-4f37cee69a48" }
                ); 
        }

        public DbSet<Score> Scores { get; set; } = default!;
    }
}
