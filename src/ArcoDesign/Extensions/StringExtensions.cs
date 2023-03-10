using System.Text;
using System.Text.RegularExpressions;

namespace ArcoDesign.Extensions;
internal static class StringExtensions {
    public static bool IsNullOrWhiteSpace(this string value) {
        return string.IsNullOrWhiteSpace(value);
    }
    public static bool IsNullOrEmpty(this string value) {
        return string.IsNullOrEmpty(value);
    }
    public static bool IsNotNullOrWhiteSpace(this string value) {
        return !string.IsNullOrWhiteSpace(value);
    }
    public static bool IsNotNullOrEmpty(this string value) {
        return !string.IsNullOrEmpty(value);
    }

    public static string RenderIf(this string value, bool condition) {
        if(condition) {
            return value;
        }
        return string.Empty;
    }
    public static bool IsNumeric(this string? obj) {
        if (string.IsNullOrWhiteSpace(obj)) {
            return false;
        }

        return Regex.IsMatch(obj, @"^(-?\d+)(\.\d+)?$");
    }
    /// <summary>
    /// 将大驼峰命名转为css命名
    /// </summary>
    public static string ToCSSCase(this string str) {
        var builder = new StringBuilder();
        var name = str;
        var previousUpper = false;

        for (var i = 0; i < name.Length; i++) {
            var c = name[i];
            if (char.IsUpper(c)) {
                if (i > 0 && !previousUpper) {
                    builder = builder.Append("-");
                }
                builder = builder.Append(char.ToLowerInvariant(c));
                previousUpper = true;
            } else {
                builder = builder.Append(c);
                previousUpper = false;
            }
        }
        return builder.ToString();
    }
}
