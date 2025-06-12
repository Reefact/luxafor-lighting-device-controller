#region Usings declarations

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using Reefact.LuxaforLightingDeviceController.LightingCommandFactories;

using Value;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents a lighting command to a Luxafor <see cref="LuxaforDevice">device</see>.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class LightingCommand : ValueType<LightingCommand> {

        #region Statics members declarations

        /// <summary>
        ///     Creates a <see cref="LightingCommand">command</see> to turn off a Luxafor <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        public static LightingCommand CreateTurnOffCommand() {
            return CreateSetColorCommand(BrightColor.Black);
        }

        /// <summary>
        ///     Creates a <see cref="LightingCommand">command</see> to turn off
        ///     <paramref name="targetedLeds">targeted LEDs</paramref> of a <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="targetedLeds" /> is null.</exception>
        public static LightingCommand CreateTurnOffCommand(TargetedLeds targetedLeds) {
            return CreateSetColorCommand(targetedLeds, BrightColor.Black);
        }

        /// <summary>
        ///     Creates a <see cref="LightingCommand">command</see> to turn on/off all LEDs of a
        ///     <see cref="LuxaforDevice">device</see>
        ///     in a <paramref name="color">bright color</paramref>.
        /// </summary>
        /// <param name="color">The <see cref="BrightColor">bright color</see>.</param>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="color" /> is null.</exception>
        public static LightingCommand CreateSetColorCommand(BrightColor color) {
            return CreateSetColorCommand(TargetedLeds.All, color);
        }

        /// <summary>
        ///     Creates a <see cref="LightingCommand">command</see> to turn on/off
        ///     <paramref name="targetedLeds">targeted LEDs</paramref>
        ///     of a <see cref="LuxaforDevice">device</see> in a <paramref name="color">bright color</paramref>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">bright color</see>.</param>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="targetedLeds" /> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="color" /> is null.</exception>
        public static LightingCommand CreateSetColorCommand(TargetedLeds targetedLeds, BrightColor color) {
            SetColorCommandFactory factory = new SetColorCommandFactory(targetedLeds, color);
            LightingCommand        command = factory.Create();

            return command;
        }

        /// <summary>
        ///     Create a <see cref="LightingCommand">command</see> to fade <paramref name="targetedLeds">targeted LEDs</paramref>
        ///     of a
        ///     <see cref="LuxaforDevice">device</see> in a <paramref name="color">bright color</paramref>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">bright color</see>.</param>
        /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        public static LightingCommand CreateFadeColorCommand(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration) {
            FadeColorCommandFactory factory            = new FadeColorCommandFactory(targetedLeds, color, duration);
            LightingCommand         fadeToColorCommand = factory.Create();

            return fadeToColorCommand;
        }

        /// <summary>
        ///     Create a <see cref="LightingCommand">command</see> to activate the strobe effect on
        ///     <paramref name="targetedLeds">targeted LEDs</paramref> of a <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="color">The <see cref="BrightColor">bright color</see>.</param>
        /// <param name="speed">The <see cref="Speed">strobe speed.</see></param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="targetedLeds" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="color" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="speed" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        public static LightingCommand CreateStrobeCommand(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat) {
            StrobeCommandFactory factory = new StrobeCommandFactory(targetedLeds, color, speed, repeat);
            LightingCommand      command = factory.Create();

            return command;
        }

        /// <summary>
        ///     Create a <see cref="LightingCommand">command</see> to activate a wave effect of a
        ///     <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="wavePattern">The <see cref="WavePattern">predefined type</see> of the wave.</param>
        /// <param name="color">The <see cref="BrightColor">bright color</see>.</param>
        /// <param name="speed">The <see cref="Speed">strobe speed.</see></param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="wavePattern" />
        ///     is not a valid <see cref="WavePattern">wave type</see>.
        /// </exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="color" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="speed" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        public static LightingCommand CreatePlayWavePatternCommand(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat) {
            PlayWavePatternCommandFactory factory = new PlayWavePatternCommandFactory(wavePattern, color, speed, repeat);
            LightingCommand               command = factory.Create();

            return command;
        }

        /// <summary>
        ///     Create a <see cref="LightingCommand">command</see> to play a built-in pattern of a
        ///     <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="builtInPattern">The <see cref="BuiltInPattern">predefined built-in pattern</see> to activate.</param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightingCommand">command</see>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="builtInPattern" />
        ///     is not a valid <see cref="BuiltInPattern">built-in pattern</see>.
        /// </exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        /// .
        public static LightingCommand CreatePlayBuiltInPatternCommand(BuiltInPattern builtInPattern, Repeat repeat) {
            PlayBuiltInPatternCommandFactory factory = new PlayBuiltInPatternCommandFactory(builtInPattern, repeat);
            LightingCommand                  command = factory.Create();

            return command;
        }

        #endregion

        #region Fields declarations

        private readonly byte[] _buffer;
        private readonly string _stringRepresentation;

        #endregion

        #region Constructors declarations

        internal LightingCommand(CommandCode code,    CommandMode mode,    BrightColor color,
                                 Option      option1, Option      option2, Option      option3,
                                 string      stringRepresentation) {
            Rgb rgb = color.ToRgb();
            _buffer               = new byte[] { 0, code.ToByte(), mode.ToByte(), rgb.Red, rgb.Green, rgb.Blue, option1.ToByte(), option2.ToByte(), option3.ToByte() };
            _stringRepresentation = stringRepresentation;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString() {
            return _stringRepresentation;
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
            return new object[] { new ListByValue<byte>(_buffer) };
        }

        internal byte[] ToBuffer() {
            byte[] buffer = new byte[_buffer.Length];
            _buffer.CopyTo(buffer, 0);

            return buffer;
        }

    }

}