using System;
using TestAssignment.Utilities.Common.Enums;

namespace TestAssignment.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum StringToEnum<TEnum>(this string enumValue) where TEnum : struct, Enum
        {
            if (Enum.TryParse(enumValue, true, out TEnum enumObject))
            {
                return enumObject;
            }

            return default;
        }

        public static int ToStatusCode(this ErrorCodes errorCode)
        {
            return errorCode switch
            {
                ErrorCodes.NotFound => 404,
                ErrorCodes.InternalServerError => 500,
                ErrorCodes.ObjectNull => 500,
                ErrorCodes.InvalidRequest => 400,
                _ => 500
            };
        }
    }
}