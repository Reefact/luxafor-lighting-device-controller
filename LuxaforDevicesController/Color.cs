#region Usings declarations

using System.Diagnostics;
using System.Drawing;

using Value;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class Color : ValueType<Color> {

    #region Statics members declarations

    public static readonly Color UnUsed = new(0, 0, 0);

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

    #region Nested types declarations

    public static class Basic {

        #region Statics members declarations

        public static readonly Color Red     = new(255, 0, 0);
        public static readonly Color Green   = new(0, 255, 0);
        public static readonly Color Blue    = new(0, 0, 255);
        public static readonly Color Cyan    = new(120, 120, 255);
        public static readonly Color Magenta = new(255, 0, 255);
        public static readonly Color Yellow  = new(255, 255, 0);
        public static readonly Color White   = new(255, 255, 255);
        public static readonly Color Off     = new(0, 0, 0);

        #endregion

    }

    #endregion

}