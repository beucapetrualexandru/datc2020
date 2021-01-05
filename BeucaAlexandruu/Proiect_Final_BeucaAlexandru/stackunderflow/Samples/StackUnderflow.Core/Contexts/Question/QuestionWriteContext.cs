using System.Collections.Generic;
using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionWriteContext
    {
        public ICollection<Post> Questions { get; }

        public QuestionWriteContext(ICollection<Post> questionSummary)
        {
            Questions = questionSummary ?? new List<Post>(0);
        }
    }
}
