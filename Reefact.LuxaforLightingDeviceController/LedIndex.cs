#region Usings declarations

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents a LED index.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class LedIndex : IEquatable<LedIndex> {

        #region Statics members declarations

        // Declared first: the LED indexes below register themselves in this dictionary as they are created.
        private static readonly Dictionary<byte, LedIndex> Indexes = new Dictionary<byte, LedIndex>();

        /// <summary>The LED n° 1.</summary>
        public static readonly LedIndex _1 = Create(1);
        /// <summary>The LED n° 2.</summary>
        public static readonly LedIndex _2 = Create(2);
        /// <summary>The LED n° 3.</summary>
        public static readonly LedIndex _3 = Create(3);
        /// <summary>The LED n° 4.</summary>
        public static readonly LedIndex _4 = Create(4);
        /// <summary>The LED n° 5.</summary>
        public static readonly LedIndex _5 = Create(5);
        /// <summary>The LED n° 6.</summary>
        public static readonly LedIndex _6 = Create(6);

        /// <summary>Gets all the LED indexes.</summary>
        /// <returns>An array of <see cref="LedIndex">LED index</see>.</returns>
        public static LedIndex[] GetAll() {
            return Indexes.Values.ToArray();
        }

        /// <summary>Create a <see cref="LedIndex" /> from a byte value.</summary>
        /// <param name="value">The index of the LED (from 1 to 6).</param>
        /// <returns>A <see cref="LedIndex">LED index</see>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Argument <paramref name="value" /> is out of range.</exception>
        public static LedIndex From(byte value) {
            if (!Indexes.TryGetValue(value, out LedIndex ledIndex)) { throw new ArgumentOutOfRangeException(nameof(value), value, "LED index shall be between 1 and 6."); }

            return ledIndex;
        }

        private static LedIndex Create(byte index) {
            LedIndex ledIndex = new LedIndex(index);
            Indexes.Add(index, ledIndex);

            return ledIndex;
        }

        #endregion

        #region Fields declarations

        private readonly byte _value;

        #endregion

        #region Constructors declarations

        private LedIndex(byte value) {
            _value = value;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString() {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public bool Equals(LedIndex? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as LedIndex);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        /// <summary>Indicates whether two <see cref="LedIndex">LED indexes</see> are equal.</summary>
        /// <param name="left">The first <see cref="LedIndex">LED index</see> to compare.</param>
        /// <param name="right">The second <see cref="LedIndex">LED index</see> to compare.</param>
        /// <returns>true if both values are equal, otherwise false.</returns>
        public static bool operator ==(LedIndex? left, LedIndex? right) {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>Indicates whether two <see cref="LedIndex">LED indexes</see> are different.</summary>
        /// <param name="left">The first <see cref="LedIndex">LED index</see> to compare.</param>
        /// <param name="right">The second <see cref="LedIndex">LED index</see> to compare.</param>
        /// <returns>true if both values are different, otherwise false.</returns>
        public static bool operator !=(LedIndex? left, LedIndex? right) {
            return !(left == right);
        }

        internal byte ToByte() {
            return _value;
        }

    }

}
