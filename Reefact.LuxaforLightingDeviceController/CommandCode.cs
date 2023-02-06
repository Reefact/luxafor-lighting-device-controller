#region Usings declarations

using System.Collections.Generic;
using System.Diagnostics;

using Reefact.LuxaforLightingDeviceController.Protocol;

using Value;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    [DebuggerDisplay("{ToString()}")]
    internal sealed class CommandCode : ValueType<CommandCode> {

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
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
            return new object[] { _value };
        }

        internal byte ToByte() {
            return _value;
        }

    }

}