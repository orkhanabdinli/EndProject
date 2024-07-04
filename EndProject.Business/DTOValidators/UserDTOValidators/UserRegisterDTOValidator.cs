using EndProject.Business.DTOs.UserDTOs;
using FluentValidation;

namespace EndProject.Business.DTOValidators.UserDTOValidators
{
    public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("FirstName can not be null")
                .NotEmpty().WithMessage("FirstName can not be empty")
                .MaximumLength(20).WithMessage("FirstName length can be max 20 characters");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("LastName cannot be null")
                .NotEmpty().WithMessage("LastName cannot be empty")
                .MaximumLength(30).WithMessage("LastName length can be max 30 characters");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("UserName cannot be null")
                .NotEmpty().WithMessage("UserName cannot be empty")
                .MaximumLength(50).WithMessage("UserName length can be max 50 characters");

            //RuleFor(x => x.Gender)
            //    .Must(BeAValidGender).WithMessage("Gender must be either Male, Female, or Prefer not to say");
        }
        //private bool BeAValidGender(string gender)
        //{
        //    var allowedGenders = new[] { "Male", "Female", "Prefer not to say" };
        //    return allowedGenders.Contains(gender);
        //}
    }
}
