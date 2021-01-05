using Access.Primitives.IO;
using EarlyPay.Primitives.ValidationAttributes;
using LanguageExt;
using StackUnderflow.EF.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion
{
    public struct ReplyQuestionCommand
    {
        [OptionValidator(typeof(RequiredAttribute))]
        public Option<User> QuestionUser { get; }

        public ReplyQuestionCommand(Option<User> questionUser)
        {
            QuestionUser = questionUser;
        }
    }

    public enum ReplyQuestionCommandInput
    {
        Valid,
        UserIsNone
    }

    public class ConfirmationQuestionInputGen : InputGenerator<ReplyQuestionCommand, ReplyQuestionCommandInput>
    {
        public ConfirmationQuestionInputGen()
        {
            mappings.Add(ReplyQuestionCommandInput.Valid, () =>
                new ReplyQuestionCommand(
                    Option<User>.Some(new User()
                    {
                        DisplayName = Guid.NewGuid().ToString(),
                        Email = $"{Guid.NewGuid()}@mailinator.com"
                    }))
            );

            mappings.Add(ReplyQuestionCommandInput.UserIsNone, () =>
                new ReplyQuestionCommand(
                    Option<User>.None
                    )
            );
        }
    }
}
