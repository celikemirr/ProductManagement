using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.Models;

namespace ProductManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ProductManagementContext context = new ProductManagementContext();

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get()
        {
            var users = context.Users.Where(x => x.Status == true).ToList();
            if (users.Count() == 0)
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get(int id)
        {
            var users = context.Users.FirstOrDefault(x => x.Status == true && x.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }


        [HttpGet("DeactivatedUsers")]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeactivatedUsers()
        {
            var users = context.Users.Where(x => x.Status == false).ToList();
            if (users.Count() == 0)
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Post(User model)
        {
            if (ModelState.IsValid)
            {
                model.Status = true;
                model.CreatedDate = DateTime.Now;
                context.Users.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
            return BadRequest();
        }
        [HttpPut("ActivateAccount/{id:int}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ActiveAccount(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id && x.Status == false);
            if (user == null)
                return NotFound();
            user.Status = true;
            context.SaveChanges();
            return Ok(user);
        }


        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Put(User model)
        {
            var editUser = context.Users.FirstOrDefault(x => x.Status == true && x.Id == model.Id);

            if (editUser == null)
            {
                return NotFound();
            }
            editUser.Email = model.Email;
            editUser.LastName = model.LastName;
            editUser.FirstName = model.FirstName;
            editUser.Password = model.Password;
            editUser.Phone = model.Phone;
            context.SaveChangesAsync();
            return Ok(editUser);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Delete(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Status == true && x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Status = false;
            context.SaveChanges();
            return Ok(user);
        }
    }
}

