using FluentValidation;

namespace BluePrint.Application.Contexts.TestTodoServices.Commands;

public class CreateTestTodoCommandValidation : AbstractValidator<CreateTestTodoCommand>
{
    public CreateTestTodoCommandValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .MaximumLength(120);
    }
}