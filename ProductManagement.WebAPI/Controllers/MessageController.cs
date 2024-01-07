using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Entities.Models;
using ProductManagement.WebAPI.Models.VievModels;

namespace ProductManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        ProductManagementContext context = new ProductManagementContext();

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get()
        {
            var messages = context.Messages.Where(x => x.Status == true).ToList();
            if (messages.Count() == 0)
            {
                return NoContent();
            }
            return Ok(messages);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get(int id)
        {
            var messages = context.Messages.FirstOrDefault(x => x.Status == true && x.Id == id);
            if (messages == null)
            {
                return NotFound();
            }
            return Ok(messages);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Post(MessageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message();
                message.Status = true;
                message.CreatedDate = DateTime.Now;
                message.Email = model.Email;
                message.Details = model.Details;
                message.Phone = model.Phone;
                message.FullName = model.FullName;
                message.Subject = model.Subject;
                context.Messages.Add(message);
                context.SaveChanges();
                return Ok(model);
            }
            return BadRequest();
        }
        [HttpPut]
        [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Put(MessageUpdateViewModel model)
        {
            var message = context.Messages.Find(model.Id);

            if (message == null)
            {
                return NotFound();
            }
            message.Status = model.status;
            context.SaveChanges();
            return Ok(message);
        }
    }
}
