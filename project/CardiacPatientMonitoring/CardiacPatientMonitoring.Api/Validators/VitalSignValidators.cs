using CardiacPatientMonitoring.Api.DTOs;
using FluentValidation;

namespace CardiacPatientMonitoring.Api.Validators;

// Validates data required to create and update vital-sign records.
public class CreateVitalSignRequestValidator : AbstractValidator<CreateVitalSignRequest>
{
    public CreateVitalSignRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty();

        RuleFor(x => x.RecordedAt)
            .NotEmpty();

        RuleFor(x => x.HeartRate)
            .InclusiveBetween(30, 220);

        RuleFor(x => x.SystolicBloodPressure)
            .InclusiveBetween(70, 250);

        RuleFor(x => x.DiastolicBloodPressure)
            .InclusiveBetween(40, 150);

        RuleFor(x => x.OxygenSaturation)
            .InclusiveBetween(50, 100);

        RuleFor(x => x.Temperature)
            .InclusiveBetween(30, 45);

        RuleFor(x => x.RespiratoryRate)
            .InclusiveBetween(5, 60);
    }
}

public class UpdateVitalSignRequestValidator : AbstractValidator<UpdateVitalSignRequest>
{
    public UpdateVitalSignRequestValidator()
    {
        RuleFor(x => x.RecordedAt)
            .NotEmpty();

        RuleFor(x => x.HeartRate)
            .InclusiveBetween(30, 220);

        RuleFor(x => x.SystolicBloodPressure)
            .InclusiveBetween(70, 250);

        RuleFor(x => x.DiastolicBloodPressure)
            .InclusiveBetween(40, 150);

        RuleFor(x => x.OxygenSaturation)
            .InclusiveBetween(50, 100);

        RuleFor(x => x.Temperature)
            .InclusiveBetween(30, 45);

        RuleFor(x => x.RespiratoryRate)
            .InclusiveBetween(5, 60);
    }
}
