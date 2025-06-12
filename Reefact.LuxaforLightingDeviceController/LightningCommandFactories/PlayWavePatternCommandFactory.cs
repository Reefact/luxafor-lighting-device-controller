#region Usings declarations

using System;
using System.ComponentModel;

#endregion

namespace Reefact.LuxaforLightingDeviceController.LightingCommandFactories {

    internal sealed class PlayWavePatternCommandFactory : LightingCommandFactory {

        #region Fields declarations

        private readonly WavePattern _wavePattern;
        private readonly BrightColor _color;
        private readonly Speed       _speed;
        private readonly Repeat      _repeat;

        #endregion

        #region Constructors declarations

        public PlayWavePatternCommandFactory(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat) {
            if (color is null) { throw new ArgumentNullException(nameof(color)); }
            if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
            if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }
            if (!Enum.IsDefined(typeof(WavePattern), wavePattern)) { throw new InvalidEnumArgumentException(nameof(wavePattern), (int)wavePattern, typeof(WavePattern)); }

            _wavePattern = wavePattern;
            _color       = color;
            _speed       = speed;
            _repeat      = repeat;
        }

        #endregion

        /// <inheritdoc />
        public LightingCommand Create() {
            CommandMode     mode                 = CommandMode.From(_wavePattern);
            Option          option2              = Option.From(_repeat);
            Option          option3              = Option.From(_speed);
            string          stringRepresentation = CreateStringRepresentation();
            LightingCommand command              = new LightingCommand(CommandCode.ActivateWave, mode, _color, Option.UnUsed, option2, option3, stringRepresentation);

            return command;
        }

        private string CreateStringRepresentation() {
            if (_color == BrightColor.Black) { return "Turn off the device"; }

            return $"Play a wave pattern ({_wavePattern}) {_repeat} with a base color {_color} (speed={_speed})";
        }

    }

}