#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace Reefact.LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
internal sealed class Option : ValueType<Option> {

    #region Statics members declarations

    public static readonly Option UnUsed = new(0);

    public static Option From(FadeDuration changingTime) {
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