#region Usings declarations

using System.Diagnostics;
using System.Drawing;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class Color : ValueType<Color> {

    #region Statics members declarations

    public static readonly Color Any = new(0, 0, 0);

    public static Color From(System.Drawing.Color color) {
        return new Color(color.R, color.G, color.B);
    }

    public static Color From(string hex) {
        if (hex is null) { throw new ArgumentNullException(nameof(hex)); }

        System.Drawing.Color systemColor = ColorTranslator.FromHtml(hex);

        return From(systemColor);
    }

    #endregion

    #region Constructors declarations

    public Color(byte red, byte green, byte blue) {
        Red   = red;
        Green = green;
        Blue  = blue;
    }

    #endregion

    public byte Red { get; }

    public byte Green { get; }

    public byte Blue { get; }

    /// <inheritdoc />
    public override string ToString() {
        System.Drawing.Color myColor = System.Drawing.Color.FromArgb(Red, Green, Blue);
        string               hex     = "#" + myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");

        return hex;
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { Red, Green, Blue };
    }

}