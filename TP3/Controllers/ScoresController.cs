using Labo8api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TP3.Models;

namespace TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly WebAPITP3context _context;

        public ScoresController(WebAPITP3context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetPublicScores()
        {
            // Retrieve public scores from the database
            var publicScores = await _context.Scores
                .Where(score => score.IsPublic) // Assuming there's a boolean property IsPublic in Score model
                .ToListAsync();

            return publicScores;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetMyScores()
        {
            if(_context.Score == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _context.Users.FindAsync(userId);

            if(user !=null)
            {
                return user.Scores;
            }

            return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé."});

        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Score>>> ChangeScoreVisibility(id)
        {
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Score>>> PostScore(Score score)
        {
            if(_context.Score == null)
            {
                return Problem("Entity is null.");
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _context.Users.FindAsync(userId);

            if(user != null)
            {
                score.User = user;
                user.Scores.Add(score);
            
                _context.Score.Add(score);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetScore", new {id = score.Id }, score);
            }
            return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé." });
        }
    }
}
