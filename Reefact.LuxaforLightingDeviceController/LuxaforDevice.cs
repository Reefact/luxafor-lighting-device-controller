#region Usings declarations

using System;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

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
        ///     Sends a <see cref="LightingCommand">lighting command</see> to the
        ///     <see cref="LuxaforDevice">Luxafor device</see>.
        /// </summary>
        /// <param name="command">The <see cref="LightingCommand">lighting command</see>.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="command" /> is null.</exception>
        void Send(LightingCommand command);

        /// <summary>Turn off all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see>.</summary>
        void TurnOff();

        /// <summary>Turn off the targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see>.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        void TurnOff(TargetedLeds targetedLeds);

        /// <summary>Turns on all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> in a bright color.</summary>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        void SetColor(BrightColor color);

        /// <summary>Turns on the targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> in a bright color.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        void SetColor(TargetedLeds targetedLeds, BrightColor color);

        /// <summary>Fade all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
        void FadeColor(BrightColor color, FadeDuration duration);

        /// <summary>Fade targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
        void FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration);

        /// <summary>Strobe all the LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
        /// <param name="repeat">The number of flashes.</param>
        void Strobe(BrightColor color, Speed speed, Repeat repeat);

        /// <summary>Strobe targeted LEDs of the <see cref="LuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
        /// <param name="repeat">The number of flashes.</param>
        void Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat);

        /// <summary>Play a wave pattern on the <see cref="LuxaforDevice">Luxafor device</see> based on a bright color.</summary>
        /// <param name="wavePattern">The wave <see cref="WavePattern">type</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="speed">The wave <see cref="Speed">speed</see>.</param>
        /// <param name="repeat">The waves number.</param>
        void PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat);

        /// <summary>Play a built-in pattern on the <see cref="LuxaforDevice">Luxafor device</see>.</summary>
        /// <param name="pattern">The selected built-in pattern to play.</param>
        /// <param name="repeat">The number of repetition of the pattern.</param>
        void PlayPattern(BuiltInPattern pattern, Repeat repeat);

    }

}