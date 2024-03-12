using System.ComponentModel.DataAnnotations;
using System.Globalization;

public class MaxDecimalPlacesAttribute : ValidationAttribute
{
    private readonly int _maxDecimalPlaces;

    public MaxDecimalPlacesAttribute(int maxDecimalPlaces)
    {
        _maxDecimalPlaces = maxDecimalPlaces;
        ErrorMessage = ErrorMessage ?? $"O número não pode ter mais de {_maxDecimalPlaces} casas decimais.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        var stringValue = value.ToString();
        var decimalPlaces = stringValue.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            ? stringValue.Split(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)[1].Length
            : 0;

        if (decimalPlaces > _maxDecimalPlaces)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
