#region Usings declarations

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using Reefact.LuxaforLightingDeviceController.Converters;

using Value;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    [DebuggerDisplay("{ToString()}")]
    internal sealed class CommandMode : ValueType<CommandMode> {

        #region Statics members declarations

        public static CommandMode From(TargetedLeds targetedLeds) {
            if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }

            return new CommandMode(targetedLeds.ToLuxCode());
        }

        public static CommandMode From(WavePattern wavePattern) {
            if (!Enum.IsDefined(typeof(WavePattern), wavePattern)) { throw new InvalidEnumArgumentException(nameof(wavePattern), (int)wavePattern, typeof(WavePattern)); }

            var wavePatternAsByte = WavePatternConverter.ToByte(wavePattern);

            return new CommandMode(wavePatternAsByte);
        }

        public static CommandMode From(BuiltInPattern builtInPattern) {
            if (!Enum.IsDefined(typeof(BuiltInPattern), builtInPattern)) { throw new InvalidEnumArgumentException(nameof(builtInPattern), (int)builtInPattern, typeof(BuiltInPattern)); }

            var builtInPatternAsByte = BuiltInPatternConverter.ToByte(builtInPattern);

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