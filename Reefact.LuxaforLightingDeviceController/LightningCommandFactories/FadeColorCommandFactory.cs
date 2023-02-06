using System;

namespace Reefact.LuxaforLightingDeviceController.LightingCommandFactories {

    internal sealed class FadeColorCommandFactory : LightingCommandFactory {

        #region Fields declarations

        private readonly TargetedLeds _targetedLeds;
        private readonly BrightColor  _color;
        private readonly FadeDuration _duration;

        #endregion

        #region Constructors declarations

        public FadeColorCommandFactory(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration) {
            if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
            if (color is null) { throw new ArgumentNullException(nameof(color)); }
            if (duration is null) { throw new ArgumentNullException(nameof(duration)); }

            _targetedLeds = targetedLeds;
            _color        = color;
            _duration     = duration;
        }

        #endregion

        public LightingCommand Create() {
            CommandMode     mode                 = CommandMode.From(_targetedLeds);
            Option          option1              = Option.From(_duration);
            string          stringRepresentation = CreateStringRepresentation();
            LightingCommand command              = new LightingCommand(CommandCode.SetColorWithFade, mode, _color, option1, Option.UnUsed, Option.UnUsed, stringRepresentation);

            return command;
        }

        private string CreateStringRepresentation() {
            if (_color == BrightColor.Black) { return "Turn off the device"; }

            return $"Fade {_targetedLeds} color to {_color} over a duration od {_duration} units";
        }

    }

}