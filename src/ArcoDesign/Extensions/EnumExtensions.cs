namespace ArcoDesign.Extensions;
internal static class EnumExtensions {
    public static string ToCSSName(this Enum self) {
        var s = self.ToString().ToCSSCase();
        return s;
    }
}
