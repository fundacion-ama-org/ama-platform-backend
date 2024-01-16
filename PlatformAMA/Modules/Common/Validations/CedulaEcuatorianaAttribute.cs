using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class CedulaEcuatorianaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string cedula = value.ToString();

            // Preguntamos si la cédula consta de 10 dígitos
            if (cedula.Length == 10)
            {
                // Obtenemos el dígito de la región que son los dos primeros dígitos
                var digitoRegion = int.Parse(cedula.Substring(0, 2));

                // Preguntamos si la región existe, Ecuador se divide en 24 regiones
                if (digitoRegion >= 1 && digitoRegion <= 24)
                {
                    // Extraemos el último dígito
                    var ultimoDigito = int.Parse(cedula.Substring(9, 1));

                    // Agrupamos todos los pares y los sumamos
                    var pares = int.Parse(cedula.Substring(1, 1)) + int.Parse(cedula.Substring(3, 1)) +
                                int.Parse(cedula.Substring(5, 1)) + int.Parse(cedula.Substring(7, 1));

                    // Agrupamos los impares, los multiplicamos por un factor de 2
                    // Si el resultado es mayor que 9, le restamos 9
                    var numero1 = MultiplyAndSubtractNine(int.Parse(cedula.Substring(0, 1)));
                    var numero3 = MultiplyAndSubtractNine(int.Parse(cedula.Substring(2, 1)));
                    var numero5 = MultiplyAndSubtractNine(int.Parse(cedula.Substring(4, 1)));
                    var numero7 = MultiplyAndSubtractNine(int.Parse(cedula.Substring(6, 1)));
                    var numero9 = MultiplyAndSubtractNine(int.Parse(cedula.Substring(8, 1)));

                    var impares = numero1 + numero3 + numero5 + numero7 + numero9;

                    // Suma total
                    var sumaTotal = pares + impares;

                    // Extraemos el primer dígito
                    var primerDigitoSuma = int.Parse(sumaTotal.ToString().Substring(0, 1));

                    // Obtenemos la decena inmediata
                    var decena = (primerDigitoSuma + 1) * 10;

                    // Obtenemos la resta de la decena inmediata - la sumaTotal
                    // Esto nos da el dígito validador
                    var digitoValidador = decena - sumaTotal;

                    // Si el dígito validador es igual a 10, toma el valor de 0
                    if (digitoValidador == 10)
                    {
                        digitoValidador = 0;
                    }

                    // Validamos que el dígito validador sea igual al de la cédula
                    if (digitoValidador == ultimoDigito)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("El dígito verificador no es válido.");
                    }
                }
                else
                {
                    return new ValidationResult("Esta cédula no pertenece a ninguna región.");
                }
            }
            else
            {
                return new ValidationResult("La cédula tiene menos o más de 10 dígitos.");
            }
        }

        return ValidationResult.Success; // Si el valor es nulo, se considera válido (puedes ajustar según tus necesidades)
    }

    private static int MultiplyAndSubtractNine(int number)
    {
        var result = number * 2;
        return result > 9 ? result - 9 : result;
    }
}