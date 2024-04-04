using Labo8api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using TP3.Data;
using TP3.Models;

namespace TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private ScoreService _scoreService;

        public ScoreController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpGet]
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
        public async Task<ActionResult<IEnumerable<Score>>> GetMyScores()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myScores = await _scoreService.GetMyScoresAsync(userId);
            if (myScores == null)
            {
                return NotFound();
            }
            return Ok(myScores);
        }


        [HttpPut]
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
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var createdScore = await _scoreService.AddScoreAsync(score, userId);
                return CreatedAtAction(nameof(PostScore), new { id = createdScore.Id }, createdScore);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
