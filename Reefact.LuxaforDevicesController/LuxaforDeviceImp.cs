#region Usings declarations

using HidLibrary;

#endregion

namespace Reefact.LuxaforDevicesController;

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

    public void Send(LightningCommand command) {
        if (command is null) { throw new ArgumentNullException(nameof(command)); }

        byte[] buffer = command.ToBuffer();

        _target.Write(buffer);
    }

    public void TurnOff() {
        var command = LightningCommand.CreateTurnOffCommand();
        Send(command);
    }

    public void TurnOff(TargetedLeds targetedLeds) {
        var command = LightningCommand.CreateTurnOffCommand(targetedLeds);
        Send(command);
    }

    public void SetColor(BrightColor color) {
        var command = LightningCommand.CreateSetColorCommand(color);
        Send(command);
    }

    public void SetColor(TargetedLeds targetedLeds, BrightColor color) {
        var command = LightningCommand.CreateSetColorCommand(targetedLeds, color);
        Send(command);
    }

    /// <inheritdoc />
    public void FadeColor(BrightColor color, FadeDuration duration) {
        FadeColor(TargetedLeds.All, color, duration);
    }

    public void FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration) {
        var command = LightningCommand.CreateFadeColorCommand(targetedLeds, color, duration);
        Send(command);
    }

    /// <inheritdoc />
    public void Strobe(BrightColor color, Speed speed, Repeat repeat) {
        Strobe(TargetedLeds.All, color, speed, repeat);
    }

    public void Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateStrobeCommand(targetedLeds, color, speed, repeat);
        Send(command);
    }

    public void Wave(WaveType waveType, BrightColor color, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateWaveCommand(waveType, color, speed, repeat);
        Send(command);
    }

    public void PlayPattern(BuiltInPattern pattern, Repeat repeat) {
        var command = LightningCommand.CreatePlayPatternCommand(pattern, repeat);
        Send(command);
    }

}