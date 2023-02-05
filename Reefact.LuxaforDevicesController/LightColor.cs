#region Usings declarations

using System.Diagnostics;
using System.Drawing;

using Value;

#endregion

namespace Reefact.LuxaforDevicesController;

/// <summary>
///     Represents the color of the light of a LED.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class LightColor : ValueType<LightColor> {

    #region Statics members declarations

    internal static readonly LightColor Off    = new(0, 0, 0);
    internal static readonly LightColor UnUsed = new(0, 0, 0);

    /// <summary>
    ///     Create a <see cref="LightColor">custom color</see> from a <see cref="BasicColor">basic color</see>.
    /// </summary>
    /// <param name="basicColor">The source <see cref="BasicColor">basic color</see>.</param>
    /// <returns>A <see cref="LightColor">custom color</see>.</returns>
    public static LightColor From(BasicColor basicColor) {
        return basicColor switch {
            BasicColor.Off     => new LightColor(0, 0, 0),
            BasicColor.Red     => new LightColor(255, 0, 0),
            BasicColor.Green   => new LightColor(0, 255, 0),
            BasicColor.Blue    => new LightColor(0, 0, 255),
            BasicColor.Cyan    => new LightColor(0, 255, 255),
            BasicColor.Magenta => new LightColor(255, 0, 255),
            BasicColor.Yellow  => new LightColor(255, 255, 0),
            BasicColor.White   => new LightColor(255, 255, 255),
            _                  => throw new ArgumentOutOfRangeException(nameof(basicColor), basicColor, null)
        };
    }

    /// <summary>
    ///     Create a <see cref="LightColor">custom color</see> from an hexadecimal representation (e.g. #6A11A).
    /// </summary>
    /// <param name="hex">The hexadecimal value.</param>
    /// <returns>A <see cref="LightColor">custom color</see>.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Argument
    ///     <param ref="hex" />
    ///     is null.
    /// </exception>
    public static LightColor From(string hex) {
        if (hex is null) { throw new ArgumentNullException(nameof(hex)); }

        Color systemColor = ColorTranslator.FromHtml(hex);

        return new LightColor(systemColor.R, systemColor.G, systemColor.B);
    }

    #endregion

    #region Fields declarations

    private readonly Rgb _rgb;

    #endregion

    #region Constructors declarations

    /// <summary>
    ///     Instantiate a new <see cref="LightColor">custom color</see>.
    /// </summary>
    /// <param name="red">The red component of the <see cref="LightColor">custom color</see>.</param>
    /// <param name="green">The green component of the <see cref="LightColor">custom color</see>.</param>
    /// <param name="blue">The blue component of the <see cref="LightColor">custom color</see>.</param>
    public LightColor(byte red, byte green, byte blue) {
        _rgb = new Rgb(red, green, blue);
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() {
        Color  myColor = Color.FromArgb(_rgb.Red, _rgb.Green, _rgb.Blue);
        string hex     = "#" + myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");

        return hex;
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { _rgb };
    }

    internal Rgb ToRgb() {
        return _rgb;
    }

}