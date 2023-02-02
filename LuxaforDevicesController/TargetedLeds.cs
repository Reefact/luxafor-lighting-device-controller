#region Usings declarations

#endregion

#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class TargetedLeds : ValueType<TargetedLeds> {

    #region Statics members declarations

    public static readonly TargetedLeds Led_1    = new(LuxaforProtocol.Lux.Led_1);
    public static readonly TargetedLeds Led_2    = new(LuxaforProtocol.Lux.Led_2);
    public static readonly TargetedLeds Led_3    = new(LuxaforProtocol.Lux.Led_3);
    public static readonly TargetedLeds Led_4    = new(LuxaforProtocol.Lux.Led_4);
    public static readonly TargetedLeds Led_5    = new(LuxaforProtocol.Lux.Led_5);
    public static readonly TargetedLeds Led_6    = new(LuxaforProtocol.Lux.Led_6);
    public static readonly TargetedLeds BackSide = new(LuxaforProtocol.Lux.BackSide);
    public static readonly TargetedLeds TabSide  = new(LuxaforProtocol.Lux.TabSide);
    public static readonly TargetedLeds All      = new(LuxaforProtocol.Lux.All);

    public static TargetedLeds FromIndex(byte index) {
        if (index is < 1 or > 6) { throw new ArgumentOutOfRangeException(nameof(index)); }

        return new TargetedLeds(index);
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    private TargetedLeds(byte value) {
        _value = value;
    }

    private TargetedLeds(char c) : this(Convert.ToByte(c)) { }

    #endregion

    /// <inheritdoc />
    public override string ToString() {
        return _value.ToString();
    }

    public byte ToByte() {
        return _value;
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { _value };
    }

}