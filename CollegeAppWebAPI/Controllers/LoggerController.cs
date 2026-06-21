using CollegeApp.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly IMyLogger _myLogger;

        public LoggerController(IMyLogger _myLogger)
        {
            this._myLogger = _myLogger;
        }

        [HttpGet("LogMessage", Name = "LogMessage")]
        public ActionResult LogMessage(string message)
        {
            _myLogger.Log(message);
            return Ok("Message logged successfully");
        }
    }
}
