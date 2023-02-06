#region Usings declarations

using System.Collections.Generic;
using System.Diagnostics;

using Value;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents a number of effect repetitions (wave, strobe, ...).
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class Repeat : ValueType<Repeat> {

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
                default: return $"{_value} times";
            }
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