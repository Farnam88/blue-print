using FluentValidation;

namespace TestAssignment.Application.Contexts.TestAssignmentServices.Commands
{
    public class CreateTestAssignmentCommandValidation : AbstractValidator<CreateTestAssignmentCommand>
    {
        public CreateTestAssignmentCommandValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(120);
        }

    }
}