﻿using Labo8api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TP3.Models;

namespace TP3.Data
{
    public class ScoreService
    {
        private readonly WebAPITP3context _context;

        public ScoreService(WebAPITP3context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Score>> GetPublicScoresAsync()
        {
            return await _context.Scores.Where(score => score.IsPublic).ToListAsync();
        }
        
        public async Task<IEnumerable<Score>> GetMyScoresAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.Scores).FirstOrDefaultAsync(u => u.Id == userId);
            return user?.Scores;
        }

        public async Task UpdateScoreVisibilityAsync(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score != null)
            {
                score.IsPublic = !score.IsPublic;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddScoreAsync(Score score, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                score.User = user;
                score.Pseudo = score.User.UserName;
                score.Date = DateTime.Now.ToString();
                score.IsPublic = true;
                user.Scores.Add(score);
                _context.Scores.Add(score);
                await _context.SaveChangesAsync();
            }
        }

    }
}
