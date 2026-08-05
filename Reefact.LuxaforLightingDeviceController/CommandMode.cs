#region Usings declarations

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

using Reefact.LuxaforLightingDeviceController.Converters;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    [DebuggerDisplay("{ToString()}")]
    internal sealed class CommandMode : IEquatable<CommandMode> {

        #region Statics members declarations

        public static CommandMode From(TargetedLeds targetedLeds) {
            if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }

            return new CommandMode(targetedLeds.ToLuxCode());
        }

        public static CommandMode From(WavePattern wavePattern) {
            if (!Enum.IsDefined(typeof(WavePattern), wavePattern)) { throw new InvalidEnumArgumentException(nameof(wavePattern), (int)wavePattern, typeof(WavePattern)); }

            byte wavePatternAsByte = WavePatternConverter.ToByte(wavePattern);

            return new CommandMode(wavePatternAsByte);
        }

        public static CommandMode From(BuiltInPattern builtInPattern) {
            if (!Enum.IsDefined(typeof(BuiltInPattern), builtInPattern)) { throw new InvalidEnumArgumentException(nameof(builtInPattern), (int)builtInPattern, typeof(BuiltInPattern)); }

            byte builtInPatternAsByte = BuiltInPatternConverter.ToByte(builtInPattern);

            return new CommandMode(builtInPatternAsByte);
        }

        #endregion

        #region Fields declarations

        private readonly byte _value;

        #endregion

        #region Constructors declarations

        private CommandMode(byte value) {
            _value = value;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString() {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public bool Equals(CommandMode? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as CommandMode);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        public static bool operator ==(CommandMode? left, CommandMode? right) {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(CommandMode? left, CommandMode? right) {
            return !(left == right);
        }

        internal byte ToByte() {
            return _value;
        }

    }

}
