#region Usings declarations

#endregion

namespace LuxaforDevicesController;

public sealed class LuxaforDevice {

    #region Fields declarations

    private readonly IHidDevice _target;

    #endregion

    #region Constructors declarations

    public LuxaforDevice(IHidDevice target) {
        if (target is null) { throw new ArgumentNullException(nameof(target)); }

        _target = target;
    }

    #endregion

    public string Path        => _target.DevicePath;
    public string Description => _target.Description;

    public void Send(Command command) {
        if (command is null) { throw new ArgumentNullException(nameof(command)); }

        byte[] buffer = command.ToBuffer();

        _target.Write(buffer);
    }

    public void Off() {
        var command = Command.CreateSetBasicColorCommand(BasicColor.Off);
        Send(command);
    }

    public void SetBasicColor(BasicColor basicColor) {
        var command = Command.CreateSetBasicColorCommand(basicColor);
        Send(command);
    }

    public void SetColorWithoutFade(TargetedLeds targetedLeds, Color color) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (color is null) { throw new ArgumentNullException(nameof(color)); }

        var command = Command.CreateSetColorWithoutFadeCommand(targetedLeds, color);
        Send(command);
    }

    public void SetColorWithFade(TargetedLeds targetedLeds, Color color, ChangingTime changingTime) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (color is null) { throw new ArgumentNullException(nameof(color)); }
        if (changingTime is null) { throw new ArgumentNullException(nameof(changingTime)); }

        var command = Command.CreateSetColorWithFadeCommand(targetedLeds, color, changingTime);
        Send(command);
    }

    public void ActivateWave(WaveType waveType, Color color, Speed speed, Repeat repeat) {
        if (color is null) { throw new ArgumentNullException(nameof(color)); }
        if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

        var command = Command.CreateActivateWaveCommand(waveType, color, speed, repeat);
        Send(command);
    }

    public void ActivateStrobe(TargetedLeds targetedLeds, Color color, Speed speed, Repeat repeat) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (color is null) { throw new ArgumentNullException(nameof(color)); }
        if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

        var command = Command.CreateActivateStrobeCommand(targetedLeds, color, speed, repeat);
        Send(command);
    }

    public void ActivateBuiltInPattern(BuiltInPattern pattern, Repeat repeat) {
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

        var command = Command.CreateActivateBuiltInPatternCommand(pattern, repeat);
        Send(command);
    }

}