#region Usings declarations

using System.Diagnostics;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class ChangingTime : ValueType<ChangingTime> {

    #region Statics members declarations

    public static ChangingTime From(byte value) {
        return new ChangingTime(value);
    }

    public static ChangingTime From(TimeSpan timeSpan) {
        if (timeSpan.TotalSeconds > 7.7) { throw new ArgumentOutOfRangeException(); }

        double d = timeSpan.TotalSeconds * 33;
        var    b = (byte)d;

        return new ChangingTime(b);
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    private ChangingTime(byte value) {
        _value = value;
    }

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