#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace Reefact.LuxaforDevicesController;

/// <summary>
///     Represents a number of effect repetitions (wave, strobe, ...).
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class Repeat : ValueType<Repeat> {

    #region Statics members declarations

    public static readonly Repeat Once  = new(1);
    public static readonly Repeat Twice = new(2);

    /// <summary>
    ///     Creates a new <see cref="Repeat" /> instance.
    /// </summary>
    /// <param name="value">The number of repetitions.</param>
    /// <returns>A <see cref="Repeat" /> value.</returns>
    public static Repeat Count(byte value) {
        return new Repeat(value);
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    private Repeat(byte value) {
        _value = value;
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() {
        return _value switch {
            0 => "none",
            1 => "once",
            2 => "twice",
            _ => $"{_value} times"
        };
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { _value };
    }

    internal byte ToByte() {
        return _value;
    }

}