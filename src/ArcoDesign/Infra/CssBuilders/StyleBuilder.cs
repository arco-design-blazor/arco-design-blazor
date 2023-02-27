using System.Text.Json;

namespace ArcoDesign.Infra.CssBuilders;

/// <summary>
/// style 构建器
/// </summary>
public sealed class StyleBuilder : KeyValueBuilder<string, string> {
    public StyleBuilder AddIfNotNullOrEmpty(object value) {
        if(value == null) {
            return this;
        }
        var type = value.GetType();
        foreach(var property in type.GetProperties()) {
            var propertyValue = property.GetValue(value);
            _ =this.Add(property.Name.ToLower(), propertyValue.ToString());
        }
        return this;    
    }
}
