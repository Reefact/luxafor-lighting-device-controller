#region Usings declarations

using System.ComponentModel;
using System.Diagnostics;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class CommandMode : ValueType<CommandMode> {

    #region Statics members declarations

    public static CommandMode From(BasicColor basicColor) {
        if (!Enum.IsDefined(typeof(BasicColor), basicColor)) { throw new InvalidEnumArgumentException(nameof(basicColor), (int)basicColor, typeof(BasicColor)); }

        byte basicColorAsByte = ToByte(basicColor);

        return new CommandMode(basicColorAsByte);
    }

    public static CommandMode From(TargetedLeds targetedLeds) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }

        return new CommandMode(targetedLeds.ToByte());
    }

    public static CommandMode From(WaveType waveType) {
        if (!Enum.IsDefined(typeof(WaveType), waveType)) { throw new InvalidEnumArgumentException(nameof(waveType), (int)waveType, typeof(WaveType)); }

        byte waveTypeAsByte = ToByte(waveType);

        return new CommandMode(waveTypeAsByte);
    }

    public static CommandMode From(BuiltInPattern builtInPattern) {
        if (!Enum.IsDefined(typeof(BuiltInPattern), builtInPattern)) { throw new InvalidEnumArgumentException(nameof(builtInPattern), (int)builtInPattern, typeof(BuiltInPattern)); }

        byte builtInPatternAsByte = ToByte(builtInPattern);

        return new CommandMode(builtInPatternAsByte);
    }

    private static byte ToByte(WaveType waveType) {
        return waveType switch {
            WaveType.Wave_1 => 1,
            WaveType.Wave_2 => 2,
            WaveType.Wave_3 => 3,
            WaveType.Wave_4 => 4,
            WaveType.Wave_5 => 5,
            _               => throw new ArgumentOutOfRangeException(nameof(waveType), waveType, null)
        };
    }

    private static byte ToByte(BasicColor simpleColor) {
        return simpleColor switch {
            BasicColor.Red     => Convert.ToByte('R'),
            BasicColor.Green   => Convert.ToByte('G'),
            BasicColor.Blue    => Convert.ToByte('B'),
            BasicColor.Cyan    => Convert.ToByte('C'),
            BasicColor.Magenta => Convert.ToByte('M'),
            BasicColor.Yellow  => Convert.ToByte('Y'),
            BasicColor.White   => Convert.ToByte('W'),
            BasicColor.Off     => Convert.ToByte('O'),
            _                  => throw new ArgumentOutOfRangeException(nameof(simpleColor), simpleColor, null)
        };
    }

    private static byte ToByte(BuiltInPattern builtInPattern) {
        return builtInPattern switch {
            BuiltInPattern.Off          => 0,
            BuiltInPattern.TrafficLight => 1,
            BuiltInPattern.Random_1     => 2,
            BuiltInPattern.Random_2     => 3,
            BuiltInPattern.Random_3     => 4,
            BuiltInPattern.Police       => 5,
            BuiltInPattern.Random_4     => 6,
            BuiltInPattern.Random_5     => 7,
            BuiltInPattern.Rainbow      => 8,
            _                           => throw new ArgumentOutOfRangeException(nameof(builtInPattern), builtInPattern, null)
        };
    }

    #endregion

    #region Fields declarations

    private readonly byte _value;

    #endregion

    #region Constructors declarations

    private CommandMode(byte value) {
        _value = value;
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() {
        return _value.ToString();
    }

    public byte ToByte() {
        return _value;
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { _value };
    }

}