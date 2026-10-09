#region Usings declarations

using System;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents a <a href="http://luxafor.com">Luxafor</a> device.
    /// </summary>
    public interface ILuxaforDevice : IDisposable {

        /// <summary>
        ///     Get the path of the <see cref="ILuxaforDevice">Luxafor device</see>.
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     Get a description of the <see cref="ILuxaforDevice">Luxafor device</see>.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Gets whether the <see cref="ILuxaforDevice">Luxafor device</see> is still plugged in.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Asks Windows whether the <see cref="Path">path</see> of the device is still among the HID
        ///         devices present. Nothing is opened and nothing is written, so the device can be polled
        ///         without being disturbed, and a device that is gone reports <c>false</c> rather than
        ///         throwing, unlike <see cref="Luxafor.GetDevice(string)" />.
        ///     </para>
        ///     <para>
        ///         A device unplugged and plugged back into the same USB port is present again, so this
        ///         property goes back to <c>true</c>, but the handle this instance holds does not survive the
        ///         removal: its commands keep returning <c>false</c>. Get the device again to drive it.
        ///     </para>
        /// </remarks>
        bool IsConnected { get; }

        /// <summary>
        ///     Sends a <see cref="LightingCommand">lighting command</see> to the
        ///     <see cref="ILuxaforDevice">Luxafor device</see>.
        /// </summary>
        /// <param name="command">The <see cref="LightingCommand">lighting command</see>.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="command" /> is null.</exception>
        bool Send(LightingCommand command);

        /// <summary>Turn off all the LEDs of the <see cref="ILuxaforDevice">Luxafor device</see>.</summary>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool TurnOff();

        /// <summary>Turn off the targeted LEDs of the <see cref="ILuxaforDevice">Luxafor device</see>.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool TurnOff(TargetedLeds targetedLeds);

        /// <summary>Turns on all the LEDs of the <see cref="ILuxaforDevice">Luxafor device</see> in a bright color.</summary>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool SetColor(BrightColor color);

        /// <summary>Turns on the targeted LEDs of the <see cref="ILuxaforDevice">Luxafor device</see> in a bright color.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool SetColor(TargetedLeds targetedLeds, BrightColor color);

        /// <summary>Fade all the LEDs of the <see cref="ILuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool FadeColor(BrightColor color, FadeDuration duration);

        /// <summary>Fade targeted LEDs of the <see cref="ILuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration);

        /// <summary>Strobe all the LEDs of the <see cref="ILuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
        /// <param name="repeat">The number of flashes.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool Strobe(BrightColor color, Speed speed, Repeat repeat);

        /// <summary>Strobe targeted LEDs of the <see cref="ILuxaforDevice">Luxafor device</see> to a bright color.</summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="speed">The flicker <see cref="Speed">speed</see>.</param>
        /// <param name="repeat">The number of flashes.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat);

        /// <summary>Play a wave pattern on the <see cref="ILuxaforDevice">Luxafor device</see> based on a bright color.</summary>
        /// <param name="wavePattern">The wave <see cref="WavePattern">type</see>.</param>
        /// <param name="color">The <see cref="BrightColor">color</see> to set.</param>
        /// <param name="speed">The wave <see cref="Speed">speed</see>.</param>
        /// <param name="repeat">The waves number.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat);

        /// <summary>Play a built-in pattern on the <see cref="ILuxaforDevice">Luxafor device</see>.</summary>
        /// <param name="pattern">The selected built-in pattern to play.</param>
        /// <param name="repeat">The number of repetition of the pattern.</param>
        /// <returns>true if the command succeeded, otherwise false</returns>
        bool PlayPattern(BuiltInPattern pattern, Repeat repeat);

    }

}