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

        public void Dispose() {
            _target.Dispose();
        }

        #endregion

        public string Path => _target.DevicePath;

        public string Description => _target.Description;

        public void Send(LightingCommand command) {
            if (command is null) { throw new ArgumentNullException(nameof(command)); }

            byte[] buffer = command.ToBuffer();

            _target.Write(buffer);
        }

        public void TurnOff() {
            var command = LightingCommand.CreateTurnOffCommand();
            Send(command);
        }

        public void TurnOff(TargetedLeds targetedLeds) {
            var command = LightingCommand.CreateTurnOffCommand(targetedLeds);
            Send(command);
        }

        public void SetColor(BrightColor color) {
            var command = LightingCommand.CreateSetColorCommand(color);
            Send(command);
        }

        public void SetColor(TargetedLeds targetedLeds, BrightColor color) {
            var command = LightingCommand.CreateSetColorCommand(targetedLeds, color);
            Send(command);
        }

        /// <inheritdoc />
        public void FadeColor(BrightColor color, FadeDuration duration) {
            FadeColor(TargetedLeds.All, color, duration);
        }

        public void FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration) {
            var command = LightingCommand.CreateFadeColorCommand(targetedLeds, color, duration);
            Send(command);
        }

        /// <inheritdoc />
        public void Strobe(BrightColor color, Speed speed, Repeat repeat) {
            Strobe(TargetedLeds.All, color, speed, repeat);
        }

        public void Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat) {
            var command = LightingCommand.CreateStrobeCommand(targetedLeds, color, speed, repeat);
            Send(command);
        }

        public void PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat) {
            var command = LightingCommand.CreatePlayWavePatternCommand(wavePattern, color, speed, repeat);
            Send(command);
        }

        public void PlayPattern(BuiltInPattern builtInPattern, Repeat repeat) {
            var command = LightingCommand.CreatePlayBuiltInPatternCommand(builtInPattern, repeat);
            Send(command);
        }

    }

}