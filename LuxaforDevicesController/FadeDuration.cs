#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace LuxaforDevicesController;

/// <summary>
///     Represents a fade duration.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class FadeDuration : ValueType<FadeDuration> {

    #region Statics members declarations

    /// <summary>
    ///     Create a <see cref="FadeDuration">fade duration</see> from a byte value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>A <see cref="FadeDuration">fade duration</see>.</returns>
    public static FadeDuration From(byte value) {
        return new FadeDuration(value);
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    private FadeDuration(byte value) {
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

    //public static explicit operator FadeDuration(byte value) {
    //    return new FadeDuration(value);
    //}

}