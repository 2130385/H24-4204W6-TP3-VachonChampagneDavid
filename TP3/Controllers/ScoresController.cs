﻿using Labo8api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Security.Claims;
using TP3.Data;
using TP3.Models;

namespace TP3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private ScoreService _scoreService;

        public ScoresController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Score>>> GetPublicScores()
        {
            var publicScores = await _scoreService.GetPublicScoresAsync();
            if (publicScores == null)
            {
                return NotFound();
            }
            return Ok(publicScores);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Score>>> GetMyScores()
        {
            if (User == null)
            {
                return Unauthorized();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myScores = await _scoreService.GetMyScoresAsync(userId);
            if (myScores == null)
            {
                return NotFound();
            }
            return Ok(myScores);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeScoreVisibility(int id)
        {
            try
            {
                await _scoreService.UpdateScoreVisibilityAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Score>> PostScore(ScoreDTO scoredto)
        {
            var score = new Score();
            score.ScoreValue = scoredto.ScoreValue;
            score.TimeInSeconds = scoredto.TimeInSeconds;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _scoreService.AddScoreAsync(score, userId);
            return Ok();
        }
    }
}
