#region Usings declarations

using HidLibrary;

#endregion

namespace LuxaforDevicesController;

/// <summary>
///     Represents a <a href="http://luxafor.com">Luxafor</a> device.
/// </summary>
public sealed class LuxaforDevice {

    #region Fields declarations

    private readonly IHidDevice _target;

    #endregion

    #region Constructors declarations

    internal LuxaforDevice(IHidDevice target) {
        if (target is null) { throw new ArgumentNullException(nameof(target)); }

        _target = target;
    }

    #endregion

    /// <summary>
    ///     Get the path of the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    public string Path => _target.DevicePath;

    /// <summary>
    ///     Get a description of the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    public string Description => _target.Description;

    /// <summary>
    ///     Sends a <see cref="LightningCommand">lightning command</see> to the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    /// <param name="command">The <see cref="LightningCommand">lightning command</see>.</param>
    /// <exception cref="ArgumentNullException">Argument <paramref name="command" /> is null.</exception>
    public void Send(LightningCommand command) {
        if (command is null) { throw new ArgumentNullException(nameof(command)); }

        byte[] buffer = command.ToBuffer();

        _target.Write(buffer);
    }

    /// <summary>
    ///     Turn off the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    public void TurnOff() {
        var command = LightningCommand.CreateTurnOffCommand();
        Send(command);
    }

    /// <summary>
    ///     Turn off the targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    public void TurnOff(TargetedLeds targetedLeds) {
        var command = LightningCommand.CreateTurnOffCommand(targetedLeds);
        Send(command);
    }

    public void SetColor(BasicColor basicColor) {
        var command = LightningCommand.CreateSetColorCommand(basicColor);
        Send(command);
    }

    public void SetColor(CustomColor customColor) {
        var command = LightningCommand.CreateSetColorCommand(customColor);
        Send(command);
    }

    public void SetColor(TargetedLeds targetedLeds, BasicColor basicColor) {
        var command = LightningCommand.CreateSetColorCommand(targetedLeds, basicColor);
        Send(command);
    }

    public void SetColor(TargetedLeds targetedLeds, CustomColor color) {
        var command = LightningCommand.CreateSetColorCommand(targetedLeds, color);
        Send(command);
    }

    public void FadeColor(TargetedLeds targetedLeds, CustomColor color, FadeDuration duration) {
        var command = LightningCommand.CreateFadeColorCommand(targetedLeds, color, duration);
        Send(command);
    }

    public void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration) {
        var command = LightningCommand.CreateFadeColorCommand(targetedLeds, basicColor, duration);
        Send(command);
    }

    public void Wave(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateWaveCommand(waveType, customColor, speed, repeat);
        Send(command);
    }

    public void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateWaveCommand(waveType, basicColor, speed, repeat);
        Send(command);
    }

    public void Strobe(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateStrobeCommand(targetedLeds, customColor, speed, repeat);
        Send(command);
    }

    public void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat) {
        var command = LightningCommand.CreateStrobeCommand(targetedLeds, basicColor, speed, repeat);
        Send(command);
    }

    public void PlayPattern(BuiltInPattern pattern, Repeat repeat) {
        var command = LightningCommand.CreatePlayPatternCommand(pattern, repeat);
        Send(command);
    }

}