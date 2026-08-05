#region Usings declarations

using System;
using System.Diagnostics;

using Reefact.LuxaforLightingDeviceController.Protocol;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    [DebuggerDisplay("{ToString()}")]
    internal sealed class CommandCode : IEquatable<CommandCode> {

        #region Statics members declarations

        public static readonly CommandCode SetColorWithoutFade     = new CommandCode(LuxaforProtocol.CommandCode.StaticsColourWithoutFade, "set color without fade");
        public static readonly CommandCode SetColorWithFade        = new CommandCode(LuxaforProtocol.CommandCode.ChangeColourWithFade, "set color with fade");
        public static readonly CommandCode ActivateStrobe          = new CommandCode(LuxaforProtocol.CommandCode.Strob, "activate strobe");
        public static readonly CommandCode ActivateWave            = new CommandCode(LuxaforProtocol.CommandCode.Wave, "activate wave");
        public static readonly CommandCode ActivateBuiltInPatterns = new CommandCode(LuxaforProtocol.CommandCode.BuildInPatterns, "activate built-in pattern");

        #endregion

        #region Fields declarations

        private readonly byte   _value;
        private readonly string _stringRepresentation;

        #endregion

        #region Constructors declarations

        private CommandCode(byte value, string stringRepresentation) {
            _value                = value;
            _stringRepresentation = stringRepresentation;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString() {
            return _stringRepresentation;
        }

        /// <inheritdoc />
        public bool Equals(CommandCode? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as CommandCode);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        public static bool operator ==(CommandCode? left, CommandCode? right) {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(CommandCode? left, CommandCode? right) {
            return !(left == right);
        }

        internal byte ToByte() {
            return _value;
        }

    }

}
