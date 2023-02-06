#region Usings declarations

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

using Value;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents the color of the light of a LED.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class BrightColor : ValueType<BrightColor> {

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

            Match match = HexParser.Match(hex);
            if (!match.Success) { throw new FormatException(); }

            byte red   = ToByte(match.Groups, "red");
            byte green = ToByte(match.Groups, "green");
            byte blue  = ToByte(match.Groups, "blue");
            var  color = new BrightColor(red, green, blue);

            return color;
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

        private static byte ToByte(GroupCollection groups, string groupName) {
            string valueAsString = groups[groupName].Value;
            var    value         = Convert.ToByte(valueAsString, 16);

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
            string hex = "#" + _rgb.Red.ToString("X2") + _rgb.Green.ToString("X2") + _rgb.Blue.ToString("X2");

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

}