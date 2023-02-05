#region Usings declarations

#endregion

namespace LuxaforDevicesController;

/// <summary>
///     Represents a <a href="http://luxafor.com">Luxafor</a> device.
/// </summary>
public interface LuxaforDevice {

    /// <summary>
    ///     Get the path of the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    string Path { get; }
    /// <summary>
    ///     Get a description of the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    string Description { get; }

    /// <summary>
    ///     Sends a <see cref="LightningCommand">lightning command</see> to the
    ///     <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    /// <param name="command">The <see cref="LightningCommand">lightning command</see>.</param>
    /// <exception cref="ArgumentNullException">Argument <paramref name="command" /> is null.</exception>
    void Send(LightningCommand command);

    /// <summary>
    ///     Turn off the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    void TurnOff();

    /// <summary>
    ///     Turn off the targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see>.
    /// </summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    void TurnOff(TargetedLeds targetedLeds);

    void SetColor(BasicColor basicColor);

    void SetColor(CustomColor customColor);

    void SetColor(TargetedLeds targetedLeds, BasicColor basicColor);

    void SetColor(TargetedLeds targetedLeds, CustomColor color);

    void FadeColor(BasicColor basicColor, FadeDuration duration);

    void FadeColor(CustomColor color, FadeDuration duration);

    void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration);

    void FadeColor(TargetedLeds targetedLeds, CustomColor color, FadeDuration duration);

    void Strobe(BasicColor basicColor, Speed speed, Repeat repeat);

    void Strobe(CustomColor customColor, Speed speed, Repeat repeat);

    void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat);

    void Strobe(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat);

    void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat);

    void Wave(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat);

    void PlayPattern(BuiltInPattern pattern, Repeat repeat);

}