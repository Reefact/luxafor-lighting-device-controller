#region Usings declarations

using System;
using System.Collections.Generic;
using System.Linq;

using Value;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents a LED index.
    /// </summary>
    public sealed class LedIndex : ValueType<LedIndex> {

        #region Statics members declarations

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

        private static Dictionary<byte, LedIndex> _indexes;

        /// <summary>Gets all the LED indexes.</summary>
        /// <returns>An array of <see cref="LedIndex">LED index</see>.</returns>
        public static LedIndex[] GetAll() {
            return _indexes.Values.ToArray();
        }

        /// <summary>Create a <see cref="LedIndex" /> from a byte value.</summary>
        /// <param name="value"></param>
        /// <returns>A <see cref="LedIndex">LED index</see>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Argument <paramref name="value" /> is out of range.</exception>
        public static LedIndex From(byte value) {
            if (!_indexes.TryGetValue(value, out LedIndex ledIndex)) { throw new ArgumentOutOfRangeException(); }

            return ledIndex;
        }

        private static LedIndex Create(byte index) {
            if (_indexes == null) {
                _indexes = new Dictionary<byte, LedIndex>();
            }
            LedIndex ledIndex = new LedIndex(index);
            _indexes.Add(index, ledIndex);

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
            return _value.ToString();
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
            return new object[] { _value };
        }

        internal byte ToByte() {
            return _value;
        }

    }

}