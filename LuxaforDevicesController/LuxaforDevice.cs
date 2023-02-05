#region Usings declarations

#endregion

namespace Reefact.LuxaforDevicesController;

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

    /// <summary>Turn off all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see>.</summary>
    void TurnOff();

    /// <summary>Turn off the targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see>.</summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    void TurnOff(TargetedLeds targetedLeds);

    /// <summary>Turns on all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> in a basic color.</summary>
    /// <param name="basicColor">The <see cref="BasicColor">color</see> to set.</param>
    void SetColor(BasicColor basicColor);

    /// <summary>Turns on all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> in a custom color.</summary>
    /// <param name="customColor">The <see cref="CustomColor">color</see> to set.</param>
    void SetColor(CustomColor customColor);

    /// <summary>Turns on the targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> in a basic color.</summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    /// <param name="basicColor">The <see cref="BasicColor">color</see> to set.</param>
    void SetColor(TargetedLeds targetedLeds, BasicColor basicColor);

    /// <summary>Turns on the targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> in a custom color.</summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    /// <param name="customColor">The <see cref="CustomColor">color</see> to set.</param>
    void SetColor(TargetedLeds targetedLeds, CustomColor customColor);

    /// <summary>Fade all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a basic color.</summary>
    /// <param name="basicColor">The <see cref="BasicColor">color</see> to set.</param>
    /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
    void FadeColor(BasicColor basicColor, FadeDuration duration);

    /// <summary>Fade all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a custom color.</summary>
    /// <param name="customColor">The <see cref="CustomColor">color</see> to set.</param>
    /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
    void FadeColor(CustomColor customColor, FadeDuration duration);

    /// <summary>Fade targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a basic color.</summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    /// <param name="basicColor">The <see cref="BasicColor">color</see> to set.</param>
    /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
    void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration);

    /// <summary>Fade targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a custom color.</summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    /// <param name="customColor">The <see cref="CustomColor">color</see> to set.</param>
    /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
    void FadeColor(TargetedLeds targetedLeds, CustomColor customColor, FadeDuration duration);

    /// <summary>Strobe all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a basic color.</summary>
    /// <param name="basicColor">The <see cref="BasicColor">color</see> to set.</param>
    /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
    /// <param name="repeat">The number of flashes.</param>
    void Strobe(BasicColor basicColor, Speed speed, Repeat repeat);

    /// <summary>Strobe all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a custom color.</summary>
    /// <param name="customColor">The <see cref="CustomColor">color</see> to set.</param>
    /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
    /// <param name="repeat">The number of flashes.</param>
    void Strobe(CustomColor customColor, Speed speed, Repeat repeat);

    /// <summary>Strobe targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a basic color.</summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    /// <param name="basicColor">The <see cref="BasicColor">color</see> to set.</param>
    /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
    /// <param name="repeat">The number of flashes.</param>
    void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat);

    /// <summary>Strobe targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a custom color.</summary>
    /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
    /// <param name="customColor">The <see cref="CustomColor">color</see> to set.</param>
    /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
    /// <param name="repeat">The number of flashes.</param>
    void Strobe(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat);

    /// <summary>Play a wave pattern on the <see cref="LuxaforDevice">Luxafor device</see> based on a basic color.</summary>
    /// <param name="waveType">The wave <see cref="WaveType">type</see>.</param>
    /// <param name="basicColor">The <see cref="BasicColor">color</see> to set.</param>
    /// <param name="speed">The wave <see cref="Speed">speed</see>.</param>
    /// <param name="repeat">The waves number.</param>
    void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat);

    /// <summary>Play a wave pattern on the <see cref="LuxaforDevice">Luxafor device</see> based on a custom color.</summary>
    /// <param name="waveType">The wave <see cref="WaveType">type</see>.</param>
    /// <param name="customColor">The <see cref="CustomColor">color</see> to set.</param>
    /// <param name="speed">The wave <see cref="Speed">speed</see>.</param>
    /// <param name="repeat">The waves number.</param>
    void Wave(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat);

    /// <summary>Play a built-in pattern on the <see cref="LuxaforDevice">Luxafor device</see>.</summary>
    /// <param name="pattern">The selected built-in pattern to play.</param>
    /// <param name="repeat">The number of repetition of the pattern.</param>
    void PlayPattern(BuiltInPattern pattern, Repeat repeat);

}