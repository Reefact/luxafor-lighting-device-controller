#region Usings declarations

#endregion

#region Usings declarations

using System.Diagnostics;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class TargetedLeds : ValueType<TargetedLeds> {

    #region Statics members declarations

    public static readonly TargetedLeds Led_1     = new(1);
    public static readonly TargetedLeds Led_2     = new(2);
    public static readonly TargetedLeds Led_3     = new(3);
    public static readonly TargetedLeds Led_4     = new(4);
    public static readonly TargetedLeds Led_5     = new(5);
    public static readonly TargetedLeds Led_6     = new(6);
    public static readonly TargetedLeds FrontSide = new('A');
    public static readonly TargetedLeds BackSide  = new('B');
    public static readonly TargetedLeds All       = new(255);

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