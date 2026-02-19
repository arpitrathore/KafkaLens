using System.Buffers.Binary;

namespace KafkaLens.Formatting;

public abstract class NumericFormatterBase : IMessageFormatter
{
    public abstract string Name { get; }

    public string? Format(byte[] data, bool prettyPrint)
    {
        return TryFormat(data, out var formatted) ? formatted : null;
    }

    public string? Format(byte[] data, string searchText, bool useObjectFilter = true)
    {
        var formatted = Format(data, true);
        if (formatted != null && (string.IsNullOrEmpty(searchText) || formatted.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
        {
            return formatted;
        }

        return null;
    }

    protected abstract bool TryFormat(byte[] data, out string formatted);
}

public sealed class Int8Formatter : NumericFormatterBase
{
    public override string Name => "Int8";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 1) return false;
        formatted = unchecked((sbyte)data[0]).ToString();
        return true;
    }
}

public sealed class UInt8Formatter : NumericFormatterBase
{
    public override string Name => "UInt8";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 1) return false;
        formatted = data[0].ToString();
        return true;
    }
}

public sealed class Int16Formatter : NumericFormatterBase
{
    public override string Name => "Int16";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 2) return false;
        formatted = BinaryPrimitives.ReadInt16BigEndian(data).ToString();
        return true;
    }
}

public sealed class UInt16Formatter : NumericFormatterBase
{
    public override string Name => "UInt16";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 2) return false;
        formatted = BinaryPrimitives.ReadUInt16BigEndian(data).ToString();
        return true;
    }
}

public sealed class Int32Formatter : NumericFormatterBase
{
    public override string Name => "Int32";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 4) return false;
        formatted = BinaryPrimitives.ReadInt32BigEndian(data).ToString();
        return true;
    }
}

public sealed class UInt32Formatter : NumericFormatterBase
{
    public override string Name => "UInt32";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 4) return false;
        formatted = BinaryPrimitives.ReadUInt32BigEndian(data).ToString();
        return true;
    }
}

public sealed class Int64Formatter : NumericFormatterBase
{
    public override string Name => "Int64";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 8) return false;
        formatted = BinaryPrimitives.ReadInt64BigEndian(data).ToString();
        return true;
    }
}

public sealed class UInt64Formatter : NumericFormatterBase
{
    public override string Name => "UInt64";

    protected override bool TryFormat(byte[] data, out string formatted)
    {
        formatted = "";
        if (data.Length != 8) return false;
        formatted = BinaryPrimitives.ReadUInt64BigEndian(data).ToString();
        return true;
    }
}