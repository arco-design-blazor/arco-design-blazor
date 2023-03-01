namespace ArcoDesign.Core;

/// <summary>
/// class 构建器
/// </summary>
public sealed class ClassNameBuilder : ICssBuilder {
    private readonly HashSet<string> collection = new HashSet<string>();

    internal ClassNameBuilder() { }

    public ClassNameBuilder Add(string name) {
        if (collection.Contains(name)) {
            return this;
        }

        _ = collection.Add(name);
        return this;
    }

    public ClassNameBuilder AddIf(bool condition, string name) {
        if (condition) {
            _ = Add(name);
        }
        return this;
    }

    public ClassNameBuilder Remove(string name) {
        if (collection.Contains(name)) {
            _ = collection.Remove(name);
        }
        return this;
    }

    public ClassNameBuilder RemoveIf(bool condition, string name) {
        if (condition) {
            _ = Remove(name);
        }

        return this;
    }

    public string Build() {
        return string.Join(" ", collection);
    }
}
