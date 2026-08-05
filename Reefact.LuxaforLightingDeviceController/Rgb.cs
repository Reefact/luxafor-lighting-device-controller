#region Usings declarations

using System;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    internal sealed class Rgb : IEquatable<Rgb> {

        #region Constructors declarations

        public Rgb(byte red, byte green, byte blue) {
            Red   = red;
            Green = green;
            Blue  = blue;
        }

        #endregion

        public byte Red   { get; }
        public byte Green { get; }
        public byte Blue  { get; }

        /// <inheritdoc />
        public bool Equals(Rgb? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return Red == other.Red && Green == other.Green && Blue == other.Blue;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as Rgb);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            unchecked {
                int hashCode = Red;
                hashCode = (hashCode * 397) ^ Green;
                hashCode = (hashCode * 397) ^ Blue;

                return hashCode;
            }
        }

        public static bool operator ==(Rgb? left, Rgb? right) {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(Rgb? left, Rgb? right) {
            return !(left == right);
        }

    }

}
