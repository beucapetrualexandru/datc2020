using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion
{
    public class ReplyAcknowledgement
    {
        public string Receipt { get; private set; }
        public ReplyAcknowledgement(string receipt) => Receipt = receipt;
    }
}
