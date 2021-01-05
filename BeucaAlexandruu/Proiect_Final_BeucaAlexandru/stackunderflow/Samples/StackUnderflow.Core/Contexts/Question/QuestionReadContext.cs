using System.Collections.Generic;
using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    class QuestionReadContext
    {
        public IEnumerable<Post> Questions { get; }

        public QuestionReadContext(IEnumerable<Post> questions)
        {
            Questions = questions;
        }
    }
}
