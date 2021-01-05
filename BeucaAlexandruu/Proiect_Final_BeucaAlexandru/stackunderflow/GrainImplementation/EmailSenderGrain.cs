using GrainInterfaces;
using StackUnderflow.EF.Models;
using System;
using System.Threading.Tasks;

namespace GrainImplementation
{
    public class EmailSenderGrain : Orleans.Grain, IEmailSender
    {
        private StackUnderflowContext _dbContext;
        public EmailSenderGrain(StackUnderflowContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<string> SendEmailAsync(string message)
        {
            //todo send e-mail

            return Task.FromResult(message);
        }
    }

}
