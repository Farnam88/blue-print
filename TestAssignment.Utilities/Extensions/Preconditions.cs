using System;

namespace TestAssignment.Utilities.Extensions
{
    public static class Preconditions
    {
        public static void CheckNull<TObject>(TObject input, string name = "")
        {
            if (input is null)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException(message: "input is null", paramName: nameof(input));
                throw new ArgumentNullException(message: $"{name} is null", paramName: nameof(input));
            }
        }
    }
}