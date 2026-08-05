#region Usings declarations

using System;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents a number of effect repetitions (wave, strobe, ...).
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class Repeat : IEquatable<Repeat> {

        #region Statics members declarations

        /// <summary>Execute a lighting pattern one time.</summary>
        public static readonly Repeat Once = new Repeat(1);
        /// <summary>Execute a lighting pattern two times.</summary>
        public static readonly Repeat Twice = new Repeat(2);

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
            switch (_value) {
                case 0:  return "none";
                case 1:  return "once";
                case 2:  return "twice";
                default: return $"{_value.ToString(CultureInfo.InvariantCulture)} times";
            }
        }

        /// <inheritdoc />
        public bool Equals(Repeat? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as Repeat);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        /// <summary>Indicates whether two <see cref="Repeat">repetition counts</see> are equal.</summary>
        /// <param name="left">The first <see cref="Repeat">repetition count</see> to compare.</param>
        /// <param name="right">The second <see cref="Repeat">repetition count</see> to compare.</param>
        /// <returns>true if both values are equal, otherwise false.</returns>
        public static bool operator ==(Repeat? left, Repeat? right) {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>Indicates whether two <see cref="Repeat">repetition counts</see> are different.</summary>
        /// <param name="left">The first <see cref="Repeat">repetition count</see> to compare.</param>
        /// <param name="right">The second <see cref="Repeat">repetition count</see> to compare.</param>
        /// <returns>true if both values are different, otherwise false.</returns>
        public static bool operator !=(Repeat? left, Repeat? right) {
            return !(left == right);
        }

        internal byte ToByte() {
            return _value;
        }

    }

}
