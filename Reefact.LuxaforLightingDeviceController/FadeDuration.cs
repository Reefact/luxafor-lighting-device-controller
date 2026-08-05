#region Usings declarations

using System;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents a fade duration.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class FadeDuration : IEquatable<FadeDuration> {

        #region Statics members declarations

        /// <summary>
        ///     Create a <see cref="FadeDuration">fade duration</see> from a byte value.
        /// </summary>
        /// <param name="value">The duration, in device units.</param>
        /// <returns>A <see cref="FadeDuration">fade duration</see>.</returns>
        public static FadeDuration From(byte value) {
            return new FadeDuration(value);
        }

        #endregion

        #region Fields declarations

        private readonly byte _value;

        #endregion

        #region Constructors declarations

        private FadeDuration(byte value) {
            _value = value;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString() {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public bool Equals(FadeDuration? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as FadeDuration);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        /// <summary>Indicates whether two <see cref="FadeDuration">fade durations</see> are equal.</summary>
        /// <param name="left">The first <see cref="FadeDuration">fade duration</see> to compare.</param>
        /// <param name="right">The second <see cref="FadeDuration">fade duration</see> to compare.</param>
        /// <returns>true if both values are equal, otherwise false.</returns>
        public static bool operator ==(FadeDuration? left, FadeDuration? right) {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>Indicates whether two <see cref="FadeDuration">fade durations</see> are different.</summary>
        /// <param name="left">The first <see cref="FadeDuration">fade duration</see> to compare.</param>
        /// <param name="right">The second <see cref="FadeDuration">fade duration</see> to compare.</param>
        /// <returns>true if both values are different, otherwise false.</returns>
        public static bool operator !=(FadeDuration? left, FadeDuration? right) {
            return !(left == right);
        }

        internal byte ToByte() {
            return _value;
        }

    }

}
