using CardiacPatientMonitoring.Api.DTOs;
using FluentValidation;

namespace CardiacPatientMonitoring.Api.Validators;

// Validates medication catalog create and update requests.
public class CreateMedicationCatalogItemRequestValidator
    : AbstractValidator<CreateMedicationCatalogItemRequest>
{
    public CreateMedicationCatalogItemRequestValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(item => item.UnitPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(item => item.StockQuantity)
            .GreaterThanOrEqualTo(0);
    }
}

// Validates medication catalog update requests.
public class UpdateMedicationCatalogItemRequestValidator
    : AbstractValidator<UpdateMedicationCatalogItemRequest>
{
    public UpdateMedicationCatalogItemRequestValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(item => item.UnitPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(item => item.StockQuantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(item => item.RowVersion)
            .NotEmpty();
    }
}