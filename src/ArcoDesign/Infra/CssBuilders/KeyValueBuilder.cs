namespace ArcoDesign.Infra.CssBuilders;

/// <summary>
/// 通用 key value 构建器
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public abstract class KeyValueBuilder<TKey,TValue> : ICssBuilder  where TKey: notnull {

    private readonly Dictionary<TKey, TValue> collection = new Dictionary<TKey, TValue>();

    protected KeyValueBuilder() { }

    public KeyValueBuilder<TKey, TValue> Add(TKey name, TValue value) {
        if (collection.ContainsKey(name)) {
            collection[name] = value;
            return this;
        }
        collection.Add(name, value);
        return this;
    }

    public KeyValueBuilder<TKey, TValue> Remove(TKey name) {
        if (collection.ContainsKey(name)) {
            _ = collection.Remove(name);
        }
        return this;
    }

    public KeyValueBuilder<TKey, TValue> AddIf(bool condition, (TKey name, TValue value) data) {
        if (!condition) {
            return this;
        }

        return Add(data.name, data.value);
    }

    public string Build() {
        var builder = new List<string>();
        foreach (var item in collection) {
            builder.Add($"{item.Key}: {item.Value};");
        }
        return string.Join(";", builder);
    }
}
