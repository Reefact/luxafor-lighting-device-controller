#region Usings declarations

using System.ComponentModel;
using System.Diagnostics;

using LuxaforDevicesController.LightningCommandFactories;

using Value;

#endregion

namespace LuxaforDevicesController {

    /// <summary>
    ///     Represents a lighting command to a Luxafor <see cref="LuxaforDevice">device</see>.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public sealed class LightningCommand : ValueType<LightningCommand> {

        #region Statics members declarations

        /// <summary>
        ///     Creates a <see cref="LightningCommand">command</see> to turn off a Luxafor <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        public static LightningCommand CreateTurnOffCommand() {
            return CreateSetColorCommand(BasicColor.Off);
        }

        /// <summary>
        ///     Creates a <see cref="LightningCommand">command</see> to turn off
        ///     <paramref name="targetedLeds">targeted LEDs</paramref> of a <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="targetedLeds" /> is null.</exception>
        public static LightningCommand CreateTurnOffCommand(TargetedLeds targetedLeds) {
            return CreateSetColorCommand(targetedLeds, BasicColor.Off);
        }

        /// <summary>
        ///     Creates a <see cref="LightningCommand">command</see> to turn on/off all LEDs of a
        ///     <see cref="LuxaforDevice">device</see>
        ///     in a <paramref name="basicColor">basic color</paramref>.
        /// </summary>
        /// <param name="basicColor">The <see cref="BasicColor">basic color</see>.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="basicColor" />
        ///     is not a valid <see cref="BasicColor">basic color</see>.
        /// </exception>
        public static LightningCommand CreateSetColorCommand(BasicColor basicColor) {
            SetBasicColorCommandFactory factory = new(basicColor);
            LightningCommand            command = factory.Create();

            return command;
        }

        /// <summary>
        ///     Creates a <see cref="LightningCommand">command</see> to turn on/off all LEDs of a
        ///     <see cref="LuxaforDevice">device</see>
        ///     in a <paramref name="customColor">custom color</paramref>.
        /// </summary>
        /// <param name="customColor">The <see cref="CustomColor">custom color</see>.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="customColor" /> is null.</exception>
        public static LightningCommand CreateSetColorCommand(CustomColor customColor) {
            return CreateSetColorCommand(TargetedLeds.All, customColor);
        }

        /// <summary>
        ///     Creates a <see cref="LightningCommand">command</see> to turn on/off
        ///     <paramref name="targetedLeds">targeted LEDs</paramref>
        ///     of a <see cref="LuxaforDevice">device</see> in a <paramref name="customColor">custom color</paramref>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="customColor">The <see cref="CustomColor">custom color</see>.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="targetedLeds" /> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="customColor" /> is null.</exception>
        public static LightningCommand CreateSetColorCommand(TargetedLeds targetedLeds, CustomColor customColor) {
            SetCustomColorCommandFactory factory = new(targetedLeds, customColor);
            LightningCommand             command = factory.Create();

            return command;
        }

        /// <summary>
        ///     Creates a <see cref="LightningCommand">command</see> to turn on/off
        ///     <paramref name="targetedLeds">targeted LEDs</paramref>
        ///     of a <see cref="LuxaforDevice">device</see> in a <paramref name="basicColor">basic color</paramref>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="basicColor">The <see cref="BasicColor">basic color</see>.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="targetedLeds" /> is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="basicColor" />
        ///     is not a valid <see cref="BasicColor">basic color</see>.
        /// </exception>
        public static LightningCommand CreateSetColorCommand(TargetedLeds targetedLeds, BasicColor basicColor) {
            CustomColor color = CustomColor.From(basicColor);

            return CreateSetColorCommand(targetedLeds, color);
        }

        /// <summary>
        ///     Create a <see cref="LightningCommand">command</see> to fade <paramref name="targetedLeds">targeted LEDs</paramref>
        ///     of a
        ///     <see cref="LuxaforDevice">device</see> in a <paramref name="customColor">custom color</paramref>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="customColor">The <see cref="CustomColor">custom color</see>.</param>
        /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        public static LightningCommand CreateFadeColorCommand(TargetedLeds targetedLeds, CustomColor customColor, FadeDuration duration) {
            FadeColorCommandFactory factory            = new(targetedLeds, customColor, duration);
            LightningCommand        fadeToColorCommand = factory.Create();

            return fadeToColorCommand;
        }

        /// <summary>
        ///     Create a <see cref="LightningCommand">command</see> to fade <paramref name="targetedLeds">targeted LEDs</paramref>
        ///     of a
        ///     <see cref="LuxaforDevice">device</see> in a <paramref name="basicColor">basic color</paramref>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="basicColor">The <see cref="BasicColor">basic color</see>.</param>
        /// <param name="duration">The <see cref="FadeDuration">fade duration</see>.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        public static LightningCommand CreateFadeColorCommand(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration) {
            CustomColor customColor = CustomColor.From(basicColor);

            return CreateFadeColorCommand(targetedLeds, customColor, duration);
        }

        /// <summary>
        ///     Create a <see cref="LightningCommand">command</see> to activate the strobe effect on
        ///     <paramref name="targetedLeds">targeted LEDs</paramref> of a <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="customColor">The <see cref="CustomColor">custom color</see>.</param>
        /// <param name="speed">The <see cref="Speed">strobe speed.</see></param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="targetedLeds" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="customColor" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="speed" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        public static LightningCommand CreateStrobeCommand(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat) {
            StrobeCommandFactory factory = new(targetedLeds, customColor, speed, repeat);
            LightningCommand     command = factory.Create();

            return command;
        }

        /// <summary>
        ///     Create a <see cref="LightningCommand">command</see> to activate the strobe effect on
        ///     <paramref name="targetedLeds">targeted LEDs</paramref> of a <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="targetedLeds">The <see cref="TargetedLeds">targeted LEDs</see>.</param>
        /// <param name="basicColor">The <see cref="BasicColor">basic color</see>.</param>
        /// <param name="speed">The <see cref="Speed">strobe speed.</see></param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="targetedLeds" /> is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="basicColor" />
        ///     is not a valid <see cref="BasicColor">basic color</see>.
        /// </exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="speed" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        public static LightningCommand CreateStrobeCommand(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat) {
            CustomColor customColor = CustomColor.From(basicColor);

            return CreateStrobeCommand(targetedLeds, customColor, speed, repeat);
        }

        /// <summary>
        ///     Create a <see cref="LightningCommand">command</see> to activate a wave effect of a
        ///     <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="waveType">The <see cref="WaveType">predefined type</see> of the wave.</param>
        /// <param name="customColor">The <see cref="CustomColor">custom color</see>.</param>
        /// <param name="speed">The <see cref="Speed">strobe speed.</see></param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="waveType" />
        ///     is not a valid <see cref="WaveType">wave type</see>.
        /// </exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="customColor" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="speed" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        public static LightningCommand CreateWaveCommand(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat) {
            WaveCommandFactory factory = new(waveType, customColor, speed, repeat);
            LightningCommand   command = factory.Create();

            return command;
        }

        /// <summary>
        ///     Create a <see cref="LightningCommand">command</see> to activate a wave effect of a
        ///     <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="waveType">The <see cref="WaveType">predefined type</see> of the wave.</param>
        /// <param name="basicColor">The <see cref="BasicColor">basic color</see>.</param>
        /// <param name="speed">The <see cref="Speed">strobe speed.</see></param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="waveType" />
        ///     is not a valid <see cref="WaveType">wave type</see>.
        /// </exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="basicColor" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="speed" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        public static LightningCommand CreateWaveCommand(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat) {
            CustomColor customColor = CustomColor.From(basicColor);

            return CreateWaveCommand(waveType, customColor, speed, repeat);
        }

        /// <summary>
        ///     Create a <see cref="LightningCommand">command</see> to play a built-in pattern of a
        ///     <see cref="LuxaforDevice">device</see>.
        /// </summary>
        /// <param name="pattern">The <see cref="BuiltInPattern">predefined built-in pattern</see> to activate.</param>
        /// <param name="repeat">the number of <see cref="Repeat">repetitions</see> to be carried out.</param>
        /// <returns>A <see cref="LightningCommand">command</see>.</returns>
        /// <exception cref="InvalidEnumArgumentException">
        ///     <paramref name="pattern" />
        ///     is not a valid <see cref="BuiltInPattern">built-in pattern</see>.
        /// </exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="repeat" /> is null.</exception>
        /// .
        public static LightningCommand CreatePlayPatternCommand(BuiltInPattern pattern, Repeat repeat) {
            PlayPatternCommandFactory factory = new(pattern, repeat);
            LightningCommand          command = factory.Create();

            return command;
        }

        #endregion

        #region Fields declarations

        private readonly byte[] _buffer;
        private readonly string _stringRepresentation;

        #endregion

        #region Constructors declarations

        internal LightningCommand(CommandCode code,    CommandMode mode,    CustomColor color,
                                  Option      option1, Option      option2, Option      option3,
                                  string      stringRepresentation) {
            _buffer               = new byte[] { 0, code.ToByte(), mode.ToByte(), color.Red, color.Green, color.Blue, option1.ToByte(), option2.ToByte(), option3.ToByte() };
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
            var buffer = new byte[_buffer.Length];
            _buffer.CopyTo(buffer, 0);

            return buffer;
        }

    }

}