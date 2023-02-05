#region Usings declarations

using System.ComponentModel;

#endregion

namespace Reefact.LuxaforDevicesController.LightningCommandFactories {

    internal sealed class SetBasicColorCommandFactory : LightningCommandFactory {

        #region Fields declarations

        private readonly BasicColor _basicColor;

        #endregion

        #region Constructors declarations

        public SetBasicColorCommandFactory(BasicColor basicColor) {
            if (!Enum.IsDefined(typeof(BasicColor), basicColor)) { throw new InvalidEnumArgumentException(nameof(basicColor), (int)basicColor, typeof(BasicColor)); }

            _basicColor = basicColor;
        }

        #endregion

        public LightningCommand Create() {
            CommandMode      mode                 = CommandMode.From(_basicColor);
            string           stringRepresentation = CreateStringRepresentation();
            LightningCommand command              = new(CommandCode.SetBasicColor, mode, CustomColor.UnUsed, Option.UnUsed, Option.UnUsed, Option.UnUsed, stringRepresentation);

            return command;
        }

        private string CreateStringRepresentation() {
            if (_basicColor == BasicColor.Off) { return Resources.LCF_TurnOffDevice; }

            return $"Set all LEDs color to {_basicColor}";
        }

    }

}