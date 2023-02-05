#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace Reefact.LuxaforDevicesController;

/// <summary>
///     Represents the speed of an effect (wave, strobe, ...)
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class Speed : ValueType<Speed> {

    #region Statics members declarations

    /// <summary>
    ///     Create a new <see cref="Speed" /> instance.
    /// </summary>
    /// <param name="value">The speed value as <see cref="byte" />.</param>
    /// <returns>A <see cref="Speed" /> value.</returns>
    public static Speed FromByte(byte value) {
        return new Speed(value);
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    private Speed(byte value) {
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

}