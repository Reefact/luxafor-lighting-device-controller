#region Usings declarations

using System;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    [DebuggerDisplay("{ToString()}")]
    internal sealed class Option : IEquatable<Option> {

        #region Statics members declarations

        public static readonly Option UnUsed = new Option(0);

        public static Option From(FadeDuration changingTime) {
            return new Option(changingTime.ToByte());
        }

        public static Option From(Repeat repeat) {
            return new Option(repeat.ToByte());
        }

        public static Option From(Speed speed) {
            return new Option(speed.ToByte());
        }

        #endregion

        #region Fields declarations

        private readonly byte _value;

        #endregion

        #region Constructors declarations

        public Option(byte value) {
            _value = value;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString() {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public bool Equals(Option? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as Option);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        public static bool operator ==(Option? left, Option? right) {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(Option? left, Option? right) {
            return !(left == right);
        }

        public byte ToByte() {
            return _value;
        }

    }

}
