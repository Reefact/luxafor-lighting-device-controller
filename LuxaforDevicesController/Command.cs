#region Usings declarations

using System.ComponentModel;
using System.Diagnostics;

using Value;

#endregion

namespace LuxaforDevicesController {

    [DebuggerDisplay("{ToString()}")]
    public sealed class Command : ValueType<Command> {

        #region Statics members declarations

        public static Command CreateSetBasicColorCommand(BasicColor basicColor) {
            if (!Enum.IsDefined(typeof(BasicColor), basicColor)) { throw new InvalidEnumArgumentException(nameof(basicColor), (int)basicColor, typeof(BasicColor)); }

            CommandMode mode = CommandMode.From(basicColor);

            return new Command(CommandCode.BasicColor, mode, Color.Any, Option.Any, Option.Any, Option.Any, TimeSpan.Zero);
        }

        public static Command CreateSetColorWithoutFadeCommand(TargetedLeds targetedLeds, Color color) {
            if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
            if (color is null) { throw new ArgumentNullException(nameof(color)); }

            CommandMode mode = CommandMode.From(targetedLeds);

            return new Command(CommandCode.ColorWithoutFade, mode, color, Option.Any, Option.Any, Option.Any, TimeSpan.Zero);
        }

        public static Command CreateSetColorWithFadeCommand(TargetedLeds targetedLeds, Color color, ChangingTime changingTime) {
            if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
            if (color is null) { throw new ArgumentNullException(nameof(color)); }
            if (changingTime is null) { throw new ArgumentNullException(nameof(changingTime)); }

            CommandMode mode    = CommandMode.From(targetedLeds);
            Option      option1 = Option.From(changingTime);

            return new Command(CommandCode.ColorWithFade, mode, color, option1, Option.Any, Option.Any, TimeSpan.Zero);
        }

        public static Command CreateActivateStrobeCommand(TargetedLeds targetedLeds, Color color, Speed speed, Repeat repeat) {
            if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
            if (color is null) { throw new ArgumentNullException(nameof(color)); }
            if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
            if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

            CommandMode mode              = CommandMode.From(targetedLeds);
            Option      option1           = Option.From(speed);
            Option      option3           = Option.From(repeat);
            TimeSpan    estimatedDuration = TimeSpan.FromSeconds(speed.ToByte() * 0.065 * repeat.ToByte());

            return new Command(CommandCode.Strobe, mode, color, option1, Option.Any, option3, estimatedDuration);
        }

        public static Command CreateActivateWaveCommand(WaveType waveType, Color color, Speed speed, Repeat repeat) {
            if (!Enum.IsDefined(typeof(WaveType), waveType)) { throw new InvalidEnumArgumentException(nameof(waveType), (int)waveType, typeof(WaveType)); }
            if (color is null) { throw new ArgumentNullException(nameof(color)); }
            if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
            if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

            CommandMode mode    = CommandMode.From(waveType);
            Option      option2 = Option.From(repeat);
            Option      option3 = Option.From(speed);

            return new Command(CommandCode.Wave, mode, color, Option.Any, option2, option3, TimeSpan.Zero);
        }

        public static Command CreateActivateBuiltInPatternCommand(BuiltInPattern pattern, Repeat repeat) {
            if (!Enum.IsDefined(typeof(BuiltInPattern), pattern)) { throw new InvalidEnumArgumentException(nameof(pattern), (int)pattern, typeof(BuiltInPattern)); }
            if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

            CommandMode mode  = CommandMode.From(pattern);
            Color       color = new(repeat.ToByte(), 0, 0);

            return new Command(CommandCode.BuiltInPatterns, mode, color, Option.Any, Option.Any, Option.Any, TimeSpan.Zero);
        }

        #endregion

        #region Constructors declarations

        private Command(CommandCode code,    CommandMode mode,    Color  color,
                        Option      option1, Option      option2, Option option3,
                        TimeSpan    estimatedDuration) {
            Code              = code;
            Mode              = mode;
            Color             = color;
            Option1           = option1;
            Option2           = option2;
            Option3           = option3;
            EstimatedDuration = estimatedDuration;
        }

        #endregion

        public CommandCode Code              { get; }
        public CommandMode Mode              { get; }
        public Color       Color             { get; }
        public Option      Option1           { get; }
        public Option      Option2           { get; }
        public Option      Option3           { get; }
        public TimeSpan    EstimatedDuration { get; }

        /// <inheritdoc />
        public override string ToString() {
            if (Code == CommandCode.BasicColor) { return ToSimpleColorString(); }
            if (Code == CommandCode.ColorWithoutFade) { return ToStaticsColorWithoutFaceString(); }
            if (Code == CommandCode.ColorWithFade) { return ToChangeColorWithFadeString(); }
            if (Code == CommandCode.Strobe) { return ToStrobString(); }
            if (Code == CommandCode.Wave) { return ToWaveString(); }
            if (Code == CommandCode.BuiltInPatterns) { return ToBuiltInPatternsString(); }

            return "???";
        }

        public byte[] ToBuffer() {
            return new byte[] { 0, Code.ToByte(), Mode.ToByte(), Color.Red, Color.Green, Color.Blue, Option1.ToByte(), Option2.ToByte(), Option3.ToByte() };
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
            return new object[] { Code, Mode, Color, Option1, Option2, Option3 };
        }

        private string ToBuiltInPatternsString() {
            return string.Empty;
        }

        private string ToWaveString() {
            return string.Empty;
        }

        private string ToStrobString() {
            return string.Empty;
        }

        private string ToChangeColorWithFadeString() {
            return string.Empty;
        }

        private string ToStaticsColorWithoutFaceString() {
            return string.Empty;
        }

        private string ToSimpleColorString() {
            return string.Empty;
        }

    }

}