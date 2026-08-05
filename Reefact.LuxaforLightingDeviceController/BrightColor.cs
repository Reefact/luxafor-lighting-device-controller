#region Usings declarations

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents the color of the light of a LED.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class BrightColor : IEquatable<BrightColor> {

        #region Statics members declarations

        /// <summary>The primary color red.</summary>
        public static readonly BrightColor Red = new BrightColor(255, 0, 0);
        /// <summary>The primary color green.</summary>
        public static readonly BrightColor Green = new BrightColor(0, 255, 0);
        /// <summary>The primary color blue.</summary>
        public static readonly BrightColor Blue = new BrightColor(0, 0, 255);
        /// <summary>The secondary color yellow.</summary>
        public static readonly BrightColor Yellow = new BrightColor(255, 255, 0);
        /// <summary>The secondary color cyan.</summary>
        public static readonly BrightColor Cyan = new BrightColor(0, 255, 255);
        /// <summary>The secondary color magenta.</summary>
        public static readonly BrightColor Magenta = new BrightColor(255, 0, 255);
        /// <summary>The white color.</summary>
        public static readonly BrightColor White = new BrightColor(255, 255, 255);
        /// <summary>The black color (aka off).</summary>
        public static readonly BrightColor Black = new BrightColor(0, 0, 0);

        private static readonly Regex HexParser = new Regex(@"^#(?'red'[A-F0-9]{2})(?'green'[A-F0-9]{2})(?'blue'[A-F0-9]{2})$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        ///     Create a <see cref="BrightColor">bright color</see> from an hexadecimal representation (e.g. #0F11A8).
        /// </summary>
        /// <param name="hex">The hexadecimal value.</param>
        /// <returns>A <see cref="BrightColor">bright color</see>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="hex" /> is null.</exception>
        /// <exception cref="FormatException">
        ///     Argument <paramref name="hex" /> is not a valid hexadecimal color representation.
        /// </exception>
        public static BrightColor From(string hex) {
            if (hex is null) { throw new ArgumentNullException(nameof(hex)); }

            Match match = HexParser.Match(hex);
            if (!match.Success) { throw new FormatException($"'{hex}' is not a valid hexadecimal color representation (expected format: #RRGGBB)."); }

            byte        red   = ToByte(match.Groups, "red");
            byte        green = ToByte(match.Groups, "green");
            byte        blue  = ToByte(match.Groups, "blue");
            BrightColor color = new BrightColor(red, green, blue);

            return color;
        }

        /// <summary>
        ///     Create a new <see cref="BrightColor">bright color</see>.
        /// </summary>
        /// <param name="red">The red component of the <see cref="BrightColor">bright color</see>.</param>
        /// <param name="green">The green component of the <see cref="BrightColor">bright color</see>.</param>
        /// <param name="blue">The blue component of the <see cref="BrightColor">bright color</see>.</param>
        /// <returns>A <see cref="BrightColor">bright color</see>.</returns>
        public static BrightColor From(byte red, byte green, byte blue) {
            return new BrightColor(red, green, blue);
        }

        private static byte ToByte(GroupCollection groups, string groupName) {
            string valueAsString = groups[groupName].Value;
            byte   value         = Convert.ToByte(valueAsString, 16);

            return value;
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
            string hex = "#" + _rgb.Red.ToString("X2", CultureInfo.InvariantCulture) + _rgb.Green.ToString("X2", CultureInfo.InvariantCulture) + _rgb.Blue.ToString("X2", CultureInfo.InvariantCulture);

            return hex;
        }

        /// <inheritdoc />
        public bool Equals(BrightColor? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _rgb.Equals(other._rgb);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as BrightColor);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _rgb.GetHashCode();
        }

        /// <summary>Indicates whether two <see cref="BrightColor">bright colors</see> are equal.</summary>
        /// <param name="left">The first <see cref="BrightColor">bright color</see> to compare.</param>
        /// <param name="right">The second <see cref="BrightColor">bright color</see> to compare.</param>
        /// <returns>true if both values are equal, otherwise false.</returns>
        public static bool operator ==(BrightColor? left, BrightColor? right) {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>Indicates whether two <see cref="BrightColor">bright colors</see> are different.</summary>
        /// <param name="left">The first <see cref="BrightColor">bright color</see> to compare.</param>
        /// <param name="right">The second <see cref="BrightColor">bright color</see> to compare.</param>
        /// <returns>true if both values are different, otherwise false.</returns>
        public static bool operator !=(BrightColor? left, BrightColor? right) {
            return !(left == right);
        }

        internal Rgb ToRgb() {
            return _rgb;
        }

    }

}
