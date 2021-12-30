using System.Threading.Tasks;
using MeetingAppAPI.Data.Models;

using Microsoft.AspNetCore.Mvc;
using PusherServer;




namespace MeetingAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class ChatController : ControllerBase
    {
        [HttpPost]
        [ActionName("messages")]
        public async Task<ActionResult> Message(MessageDTO dto)
        {
            var options = new PusherOptions
            {
                Cluster = "",
                Encrypted = true
            };

            var pusher = new Pusher(
                "",
                "",
                "",
                options);

            await pusher.TriggerAsync(
                "chat",
                "message",
                new
                {
                    username = dto.Username,
                    message = dto.Message
                });

            return Ok(new string[] { });
        }
    }
}