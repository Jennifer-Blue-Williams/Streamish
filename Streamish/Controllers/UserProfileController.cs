using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streamish.Models;
using Streamish.Repositories;

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepo;

        public UserProfileController(IUserProfileRepository userProfRepo)
        {
            _userProfileRepo = userProfRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var prof = _userProfileRepo.GetById(id);
            if (prof == null)
            {
                return NotFound();
            }

            return Ok(prof);
        }

        [HttpPost]
        public IActionResult Post(UserProfile user)
        {
            _userProfileRepo.Add(user);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _userProfileRepo.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepo.Delete(id);
            return NoContent();
        }

        [HttpGet("UserProfileVideos/{id}")]
        public IActionResult GetUserVids(int id)
        {
            var vids = _userProfileRepo.GetUserVids(id);
            return Ok(vids);
        }
    }
}