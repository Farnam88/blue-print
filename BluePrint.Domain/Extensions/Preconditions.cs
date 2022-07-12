using BluePrint.Domain.Common.Data;
using BluePrint.Domain.Exceptions;

namespace BluePrint.Domain.Extensions;

public static class Preconditions
{
    public static void CheckNull<TObject>(TObject input, string name = "")
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ObjectNullException(message: "object reference not set to an instance",
                    new List<ErrorDetail>
                    {
                        new(nameof(input), $"{nameof(input)} is null")
                    });
            throw new ObjectNullException(message: "object reference not set to an instance", new List<ErrorDetail>
            {
                new(name, $"{name} is null")
            });
        }
    }
}