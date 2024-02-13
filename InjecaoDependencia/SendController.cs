using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InjecaoDependencia
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendController : ControllerBase
    {

        private readonly ISendService _sendEmailSevice;
        private readonly ISendService _sendSmsService;

        // Uma outra forma de escrever a injeção de dependencia 
        public SendController([FromKeyedServices("EmailSend")] ISendService sendEmailSevice,
            [FromKeyedServices("SmsSend")] ISendService sendSmsService)
        {            
            _sendEmailSevice = sendEmailSevice;
            _sendSmsService = sendSmsService;
        }
    
        [HttpGet]
        public IActionResult Get(IServiceProvider serviceProvider, string type)
        {
            ISendService sendService = null;
            if(type?.ToLower() == "email")
                sendService = serviceProvider.GetRequiredKeyedService<ISendService>("EmailSend");
            else if(type?.ToLower() == "sms")
                sendService = serviceProvider.GetRequiredKeyedService<ISendService>("SmsSend");
            else if(type?.ToLower() == "push")
                sendService = serviceProvider.GetRequiredKeyedService<ISendService>("PushSend");  
            else
                return BadRequest("Tipo não encontrado");          
            
            return Ok(sendService.Send());
        }
    }
}
