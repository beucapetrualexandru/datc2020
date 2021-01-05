using Access.Primitives.Extensions.Cloning;
using StackUnderflow.EF.Models;
using CSharp.Choices;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult : IDynClonable { }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Post Question { get; }

            public QuestionCreated(Post question)
            {
                Question = question;
            }
            public object Clone() => this.ShallowClone();
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; private set; }
            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
            public object Clone() => this.ShallowClone();
        }

        public class InvalidRequest : ICreateQuestionResult
        {
            public string Message { get; }
            public InvalidRequest(string message)
            {
                Message = message;
            }
            public object Clone() => this.ShallowClone();
        }
    }
}
