using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ArcoDesign.Extensions;
internal static class NameValueCollectionExtensions {

    //public static NameValueCollection AndIf(this NameValueCollection self, bool condition, (string name, object value) data) {
    //    //if(condition)
    //    //    self.Add(data.name, data.value);
    //    return self;
    //}

    public static string Build(this NameValueCollection collection) {
        var list = new List<string>();
        foreach(var key in collection.AllKeys) {
            var value = collection[key];
            list.Add(@$"{key}=""{value}""");
        }
        return string.Join(" ", list);
    }

}
