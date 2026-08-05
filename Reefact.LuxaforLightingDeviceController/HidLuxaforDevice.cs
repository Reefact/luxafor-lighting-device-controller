#region Usings declarations

using System;

using Reefact.LuxaforLightingDeviceController.Hid;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     The <see cref="LuxaforDevice">Luxafor device</see> driven through a HID connection.
    /// </summary>
    internal sealed class HidLuxaforDevice : LuxaforDevice {

        #region Fields declarations

        private readonly IHidDeviceHandle _target;

        #endregion

        #region Constructors declarations

        internal HidLuxaforDevice(IHidDeviceHandle target) {
            if (target is null) { throw new ArgumentNullException(nameof(target)); }

            _target = target;
        }

        #endregion

        /// <inheritdoc />
        public string Path => _target.DevicePath;

        /// <inheritdoc />
        public string Description => _target.Description;

        /// <inheritdoc />
        public void Dispose() {
            _target.Dispose();
        }

        /// <inheritdoc />
        public bool Send(LightingCommand command) {
            if (command is null) { throw new ArgumentNullException(nameof(command)); }

            byte[] buffer = command.ToBuffer();

            return _target.Write(buffer);
        }

        /// <inheritdoc />
        public bool TurnOff() {
            LightingCommand command = LightingCommand.CreateTurnOffCommand();

            return Send(command);
        }

        /// <inheritdoc />
        public bool TurnOff(TargetedLeds targetedLeds) {
            LightingCommand command = LightingCommand.CreateTurnOffCommand(targetedLeds);

            return Send(command);
        }

        /// <inheritdoc />
        public bool SetColor(BrightColor color) {
            LightingCommand command = LightingCommand.CreateSetColorCommand(color);

            return Send(command);
        }

        /// <inheritdoc />
        public bool SetColor(TargetedLeds targetedLeds, BrightColor color) {
            LightingCommand command = LightingCommand.CreateSetColorCommand(targetedLeds, color);

            return Send(command);
        }

        /// <inheritdoc />
        public bool FadeColor(BrightColor color, FadeDuration duration) {
            return FadeColor(TargetedLeds.All, color, duration);
        }

        /// <inheritdoc />
        public bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration) {
            LightingCommand command = LightingCommand.CreateFadeColorCommand(targetedLeds, color, duration);

            return Send(command);
        }

        /// <inheritdoc />
        public bool Strobe(BrightColor color, Speed speed, Repeat repeat) {
            return Strobe(TargetedLeds.All, color, speed, repeat);
        }

        /// <inheritdoc />
        public bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat) {
            LightingCommand command = LightingCommand.CreateStrobeCommand(targetedLeds, color, speed, repeat);

            return Send(command);
        }

        /// <inheritdoc />
        public bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat) {
            LightingCommand command = LightingCommand.CreatePlayWavePatternCommand(wavePattern, color, speed, repeat);

            return Send(command);
        }

        /// <inheritdoc />
        public bool PlayPattern(BuiltInPattern pattern, Repeat repeat) {
            LightingCommand command = LightingCommand.CreatePlayBuiltInPatternCommand(pattern, repeat);

            return Send(command);
        }

    }

}
