using Access.Primitives.EFCore;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using StackUnderflow.EF.Models;
using System.Threading.Tasks;
using LanguageExt;
using System.Linq;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion;
using System;
using GrainInterfaces;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client;

        public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;
        }

        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand cmd)
        {
            var posts = _dbContext.Post.ToList();
            QuestionWriteContext ctx = new QuestionWriteContext(new EFList<Post>(_dbContext.Post));
            
            var dependencies = new QuestionDependencies();
            dependencies.SendConfirmationEmail = SendEmail;

            var expr = from questionResult in QuestionDomain.CreateQuestion(cmd.QuestionId, cmd.QuestionText,cmd.Title, cmd.Tags)
                       select questionResult;

            var result = await _interpreter.Interpret(expr, ctx, dependencies);

            _dbContext.SaveChanges();

            return result.Match(
                created => Ok(created),
                notCreated => (ActionResult)BadRequest($"Question  with title {cmd.Title} could not be created"),
                invalidRequest => BadRequest("Invalid request"));

            //return Ok();
        }

        private TryAsync<ReplyAcknowledgement> SendEmail(ReplyLetter letter) => async () =>
        {
            var emailSender = _client.GetGrain<IEmailSender>(0);
            await emailSender.SendEmailAsync(letter.Letter);
            return new ReplyAcknowledgement(Guid.NewGuid().ToString());
        };
    }
}