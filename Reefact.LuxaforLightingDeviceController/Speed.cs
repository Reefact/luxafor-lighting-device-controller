#region Usings declarations

using System;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents the speed of an effect (wave, strobe, ...)
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class Speed : IEquatable<Speed> {

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
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public bool Equals(Speed? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as Speed);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        /// <summary>Indicates whether two <see cref="Speed">speeds</see> are equal.</summary>
        /// <param name="left">The first <see cref="Speed">speed</see> to compare.</param>
        /// <param name="right">The second <see cref="Speed">speed</see> to compare.</param>
        /// <returns>true if both values are equal, otherwise false.</returns>
        public static bool operator ==(Speed? left, Speed? right) {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>Indicates whether two <see cref="Speed">speeds</see> are different.</summary>
        /// <param name="left">The first <see cref="Speed">speed</see> to compare.</param>
        /// <param name="right">The second <see cref="Speed">speed</see> to compare.</param>
        /// <returns>true if both values are different, otherwise false.</returns>
        public static bool operator !=(Speed? left, Speed? right) {
            return !(left == right);
        }

        internal byte ToByte() {
            return _value;
        }

    }

}
