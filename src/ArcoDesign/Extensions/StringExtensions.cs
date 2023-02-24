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

}
