using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion;
using System;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionDependencies
    {
        public Func<ReplyLetter, TryAsync<ReplyAcknowledgement>> SendConfirmationEmail { get; set; }
    }
}
