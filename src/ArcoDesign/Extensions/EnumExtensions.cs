namespace ArcoDesign.Extensions;
internal static class EnumExtensions {
    public static string ToEnumName(this Enum self, bool isLower = true) {
        var s = self.ToString();

        if (isLower) {
            return s.ToLower();
        }

        return s;
    }
}
