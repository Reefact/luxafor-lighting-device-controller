#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class Option : ValueType<Option> {

    #region Statics members declarations

    public static readonly Option Any = new(0);

    public static Option From(ChangingTime changingTime) {
        return new Option(changingTime.ToByte());
    }

    public static Option From(Repeat repeat) {
        return new Option(repeat.ToByte());
    }

    public static Option From(Speed speed) {
        return new Option(speed.ToByte());
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    public Option(byte value) {
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