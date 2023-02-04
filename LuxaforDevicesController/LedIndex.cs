#region Usings declarations

using Value;

#endregion

namespace LuxaforDevicesController;

/// <summary>
///     Represents a LED index.
/// </summary>
public sealed class LedIndex : ValueType<LedIndex> {

    #region Statics members declarations

    public static readonly LedIndex _1 = Create(1);
    public static readonly LedIndex _2 = Create(2);
    public static readonly LedIndex _3 = Create(3);
    public static readonly LedIndex _4 = Create(4);
    public static readonly LedIndex _5 = Create(5);
    public static readonly LedIndex _6 = Create(6);

    private static Dictionary<byte, LedIndex>? _indexes;

    public static LedIndex[] GetAll() {
        return _indexes!.Values.ToArray();
    }

    public static LedIndex From(byte value) {
        if (!_indexes!.TryGetValue(value, out LedIndex? ledIndex)) { throw new ArgumentOutOfRangeException(); }

        return ledIndex;
    }

    private static LedIndex Create(byte index) {
        _indexes ??= new Dictionary<byte, LedIndex>();
        LedIndex ledIndex = new(index);
        _indexes.Add(index, ledIndex);

        return ledIndex;
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    private LedIndex(byte value) {
        _value = value;
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() {
        return _value.ToString();
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { _value };
    }

    internal byte ToByte() {
        return _value;
    }

    //public static explicit operator LedIndex(byte value) {
    //    return From(value);
    //}

}