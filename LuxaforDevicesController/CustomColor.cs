#region Usings declarations

using System.Diagnostics;
using System.Drawing;

using Value;

#endregion

namespace Reefact.LuxaforDevicesController;

/// <summary>
///     Represents a <a href="https://luxafor.com/">Luxafor</a> device color.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class CustomColor : ValueType<CustomColor> {

    #region Statics members declarations

    internal static readonly CustomColor Off    = new(0, 0, 0);
    internal static readonly CustomColor UnUsed = new(0, 0, 0);

    /// <summary>
    ///     Create a <see cref="CustomColor">custom color</see> from a <see cref="System.Drawing.Color">system color</see>.
    /// </summary>
    /// <param name="color">The source <see cref="System.Drawing.Color">system color</see>.</param>
    /// <returns>A <see cref="CustomColor">custom color</see>.</returns>
    public static CustomColor From(Color color) {
        return new CustomColor(color.R, color.G, color.B);
    }

    /// <summary>
    ///     Create a <see cref="CustomColor">custom color</see> from a <see cref="BasicColor">basic color</see>.
    /// </summary>
    /// <param name="basicColor">The source <see cref="BasicColor">basic color</see>.</param>
    /// <returns>A <see cref="CustomColor">custom color</see>.</returns>
    public static CustomColor From(BasicColor basicColor) {
        return basicColor switch {
            BasicColor.Off     => new CustomColor(0, 0, 0),
            BasicColor.Red     => new CustomColor(255, 0, 0),
            BasicColor.Green   => new CustomColor(0, 255, 0),
            BasicColor.Blue    => new CustomColor(0, 0, 255),
            BasicColor.Cyan    => new CustomColor(120, 120, 255),
            BasicColor.Magenta => new CustomColor(255, 0, 255),
            BasicColor.Yellow  => new CustomColor(255, 255, 0),
            BasicColor.White   => new CustomColor(255, 255, 255),
            _                  => throw new ArgumentOutOfRangeException(nameof(basicColor), basicColor, null)
        };
    }

    /// <summary>
    ///     Create a <see cref="CustomColor">custom color</see> from an hexadecimal representation (e.g. #6A11A).
    /// </summary>
    /// <param name="hex">The hexadecimal value.</param>
    /// <returns>A <see cref="CustomColor">custom color</see>.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Argument
    ///     <param name="hex" />
    ///     is null.
    /// </exception>
    public static CustomColor From(string hex) {
        if (hex is null) { throw new ArgumentNullException(nameof(hex)); }

        Color systemColor = ColorTranslator.FromHtml(hex);

        return From(systemColor);
    }

    #endregion

    #region Constructors declarations

    /// <summary>
    ///     Instantiate a new <see cref="CustomColor">custom color</see>.
    /// </summary>
    /// <param name="red">The red component of the <see cref="CustomColor">custom color</see>.</param>
    /// <param name="green">The green component of the <see cref="CustomColor">custom color</see>.</param>
    /// <param name="blue">The blue component of the <see cref="CustomColor">custom color</see>.</param>
    public CustomColor(byte red, byte green, byte blue) {
        Red   = red;
        Green = green;
        Blue  = blue;
    }

    #endregion

    /// <summary>
    ///     The red component of the <see cref="CustomColor">custom color</see>.
    /// </summary>
    public byte Red { get; }

    /// <summary>
    ///     The green component of the <see cref="CustomColor">custom color</see>.
    /// </summary>
    public byte Green { get; }

    /// <summary>
    ///     The blue component of the <see cref="CustomColor">custom color</see>.
    /// </summary>
    public byte Blue { get; }

    /// <inheritdoc />
    public override string ToString() {
        Color  myColor = Color.FromArgb(Red, Green, Blue);
        string hex     = "#" + myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");

        return hex;
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { Red, Green, Blue };
    }

}