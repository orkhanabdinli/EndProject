using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;
using FluentValidation;

namespace EndProject.Business.DTOValidators.UserAboutDTOValidators;

public class UserAboutPutDTOValidator : AbstractValidator<UserAboutPutDTO>
{
    public UserAboutPutDTOValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId can not be null")
            .NotEmpty().WithMessage("UserId can not be empty");
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id can not be null")
            .NotEmpty().WithMessage("Id can not be empty");
        RuleFor(x => x.Bio)
            .MaximumLength(300).WithMessage("Bio length can be max 300");
        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Country length can be max 100");
        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City length can be max 100");
        RuleFor(x => x.Gender)
        .Must(BeAValidGender).WithMessage("Gender must be either Male, Female, or Prefer not to say");
    }

    private bool BeAValidGender(string gender)
    {
        var allowedGenders = new[] { "Male", "Female", "Prefer not to say" };
        return allowedGenders.Contains(gender);
    }
}

