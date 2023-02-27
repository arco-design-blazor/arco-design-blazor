namespace ArcoDesign.Extensions;
internal static class IEnumerableExtensions {
    public static IEnumerable<(int, T)> Enumerate<T>(this IEnumerable<T> self) {
        if(self == null) {
            yield break;
        }
        var i = 0;
        foreach(var item in self) { 
            yield return (i, item); 
        }
        yield break;
    }
}
