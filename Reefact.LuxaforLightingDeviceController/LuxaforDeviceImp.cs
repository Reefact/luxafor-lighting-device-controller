#region Usings declarations

using System;

using HidLibrary;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    internal sealed class LuxaforDeviceImp : LuxaforDevice {

        #region Fields declarations

        private readonly IHidDevice _target;

        #endregion

        #region Constructors declarations

        internal LuxaforDeviceImp(IHidDevice target) {
            if (target is null) { throw new ArgumentNullException(nameof(target)); }

            _target = target;
        }

        #endregion

        public string Path => _target.DevicePath;

        public string Description => _target.Description;

        public void Dispose() {
            _target.Dispose();
        }

        public bool Send(LightingCommand command) {
            if (command is null) { throw new ArgumentNullException(nameof(command)); }

            byte[] buffer = command.ToBuffer();

            return _target.Write(buffer);
        }

        public bool TurnOff() {
            LightingCommand command = LightingCommand.CreateTurnOffCommand();

            return Send(command);
        }

        public bool TurnOff(TargetedLeds targetedLeds) {
            LightingCommand command = LightingCommand.CreateTurnOffCommand(targetedLeds);

            return Send(command);
        }

        public bool SetColor(BrightColor color) {
            LightingCommand command = LightingCommand.CreateSetColorCommand(color);

            return Send(command);
        }

        public bool SetColor(TargetedLeds targetedLeds, BrightColor color) {
            LightingCommand command = LightingCommand.CreateSetColorCommand(targetedLeds, color);

            return Send(command);
        }

        /// <inheritdoc />
        public bool FadeColor(BrightColor color, FadeDuration duration) {
            return FadeColor(TargetedLeds.All, color, duration);
        }

        public bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration) {
            LightingCommand command = LightingCommand.CreateFadeColorCommand(targetedLeds, color, duration);

            return Send(command);
        }

        /// <inheritdoc />
        public bool Strobe(BrightColor color, Speed speed, Repeat repeat) {
            return Strobe(TargetedLeds.All, color, speed, repeat);
        }

        public bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat) {
            LightingCommand command = LightingCommand.CreateStrobeCommand(targetedLeds, color, speed, repeat);

            return Send(command);
        }

        public bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat) {
            LightingCommand command = LightingCommand.CreatePlayWavePatternCommand(wavePattern, color, speed, repeat);

            return Send(command);
        }

        public bool PlayPattern(BuiltInPattern builtInPattern, Repeat repeat) {
            LightingCommand command = LightingCommand.CreatePlayBuiltInPatternCommand(builtInPattern, repeat);

            return Send(command);
        }

    }

}