using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion
{
    public static partial class ReplyQuestionResult
    {
        public interface IReplyQuestionResult { }
        public class QuestionConfirmed : IReplyQuestionResult
        {
            public User questionUser_ { get; }
            public string confirmationAck_ { get; set; }

            public QuestionConfirmed(User questionUser, string confirmationAck)
            {
                questionUser_ = questionUser;
                confirmationAck_ = confirmationAck;
            }
        }
        public class QuestionNotConfirmed : IReplyQuestionResult
        {
            public string reason_ { get; }
            public QuestionNotConfirmed(string reason)
            {
                reason_ = reason_;
            }

        }
        public class InvalidRequest : IReplyQuestionResult
        {
            public InvalidRequest(string message)
            {
                message_ = message;
            }
            public string message_ { get; }
        }
    }
}
