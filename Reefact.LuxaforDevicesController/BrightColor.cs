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
public sealed class BrightColor : ValueType<BrightColor> {

    #region Statics members declarations

    /// <summary>The primary color red.</summary>
    public static readonly BrightColor Red = new(255, 0, 0);
    /// <summary>The primary color green.</summary>
    public static readonly BrightColor Green = new(0, 255, 0);
    /// <summary>The primary color blue.</summary>
    public static readonly BrightColor Blue = new(0, 0, 255);
    /// <summary>The secondary color yellow.</summary>
    public static readonly BrightColor Yellow = new(255, 255, 0);
    /// <summary>The secondary color cyan.</summary>
    public static readonly BrightColor Cyan = new(0, 255, 255);
    /// <summary>The secondary color magenta.</summary>
    public static readonly BrightColor Magenta = new(255, 0, 255);
    /// <summary>The white color.</summary>
    public static readonly BrightColor White = new(255, 255, 255);
    /// <summary>The black color (aka off).</summary>
    public static readonly BrightColor Black = new(0, 0, 0);

    /// <summary>
    ///     Create a <see cref="BrightColor">bright color</see> from an hexadecimal representation (e.g. #6A11A).
    /// </summary>
    /// <param name="hex">The hexadecimal value.</param>
    /// <returns>A <see cref="BrightColor">bright color</see>.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Argument
    ///     <param ref="hex" />
    ///     is null.
    /// </exception>
    public static BrightColor From(string hex) {
        if (hex is null) { throw new ArgumentNullException(nameof(hex)); }

        Color systemColor = ColorTranslator.FromHtml(hex);

        return new BrightColor(systemColor.R, systemColor.G, systemColor.B);
    }

    /// <summary>
    ///     Create a new <see cref="BrightColor">bright color</see>.
    /// </summary>
    /// <param name="red">The red component of the <see cref="BrightColor">bright color</see>.</param>
    /// <param name="green">The green component of the <see cref="BrightColor">bright color</see>.</param>
    /// <param name="blue">The blue component of the <see cref="BrightColor">bright color</see>.</param>
    public static BrightColor From(byte red, byte green, byte blue) {
        return new BrightColor(red, green, blue);
    }

    #endregion

    #region Fields declarations

    private readonly Rgb _rgb;

    #endregion

    #region Constructors declarations

    /// <summary>
    ///     Instantiate a new <see cref="BrightColor">bright color</see>.
    /// </summary>
    /// <param name="red">The red component of the <see cref="BrightColor">bright color</see>.</param>
    /// <param name="green">The green component of the <see cref="BrightColor">bright color</see>.</param>
    /// <param name="blue">The blue component of the <see cref="BrightColor">bright color</see>.</param>
    public BrightColor(byte red, byte green, byte blue) {
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