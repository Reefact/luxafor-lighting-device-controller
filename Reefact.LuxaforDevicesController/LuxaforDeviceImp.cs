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

    public void SetColor(BasicColor basicColor) {
        var command = LightningCommand.CreateSetColorCommand(basicColor);
        Send(command);
    }

    public void SetColor(LightColor lightColor) {
        var command = LightningCommand.CreateSetColorCommand(lightColor);
        Send(command);
    }

    public void SetColor(TargetedLeds targetedLeds, BasicColor basicColor) {
        var command = LightningCommand.CreateSetColorCommand(targetedLeds, basicColor);
        Send(command);
    }

    public void SetColor(TargetedLeds targetedLeds, LightColor color) {
        var command = LightningCommand.CreateSetColorCommand(targetedLeds, color);
        Send(command);
    }

    /// <inheritdoc />
    public void FadeColor(BasicColor basicColor, FadeDuration duration) {
        FadeColor(TargetedLeds.All, basicColor, duration);
    }

    /// <inheritdoc />
    public void FadeColor(LightColor color, FadeDuration duration) {
        FadeColor(TargetedLeds.All, color, duration);
    }

    public void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration) {
        var command = LightningCommand.CreateFadeColorCommand(targetedLeds, basicColor, duration);
        Send(command);
    }

    public void FadeColor(TargetedLeds targetedLeds, LightColor color, FadeDuration duration) {
        var command = LightningCommand.CreateFadeColorCommand(targetedLeds, color, duration);
        Send(command);
    }

    /// <inheritdoc />
    public void Strobe(BasicColor basicColor, Speed speed, Repeat repeat) {
        Strobe(TargetedLeds.All, basicColor, speed, repeat);
    }

    /// <inheritdoc />
    public void Strobe(LightColor lightColor, Speed speed, Repeat repeat) {
        Strobe(TargetedLeds.All, lightColor, speed, repeat);
    }

    public void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateStrobeCommand(targetedLeds, basicColor, speed, repeat);
        Send(command);
    }

    public void Strobe(TargetedLeds targetedLeds, LightColor lightColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateStrobeCommand(targetedLeds, lightColor, speed, repeat);
        Send(command);
    }

    public void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateWaveCommand(waveType, basicColor, speed, repeat);
        Send(command);
    }

    public void Wave(WaveType waveType, LightColor lightColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateWaveCommand(waveType, lightColor, speed, repeat);
        Send(command);
    }

    public void PlayPattern(BuiltInPattern pattern, Repeat repeat) {
        var command = LightningCommand.CreatePlayPatternCommand(pattern, repeat);
        Send(command);
    }

}