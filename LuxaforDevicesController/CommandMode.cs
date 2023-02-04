﻿#region Usings declarations

using System.ComponentModel;
using System.Diagnostics;

using LuxaforDevicesController.Converters;

using Value;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
internal sealed class CommandMode : ValueType<CommandMode> {

    #region Statics members declarations

    public static CommandMode From(BasicColor basicColor) {
        if (!Enum.IsDefined(typeof(BasicColor), basicColor)) { throw new InvalidEnumArgumentException(nameof(basicColor), (int)basicColor, typeof(BasicColor)); }

        var basicColorAsByte = BasicColorConverter.ToByte(basicColor);

        return new CommandMode(basicColorAsByte);
    }

    public static CommandMode From(TargetedLeds targetedLeds) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }

        return new CommandMode(targetedLeds.ToLuxCode());
    }

    public static CommandMode From(WaveType waveType) {
        if (!Enum.IsDefined(typeof(WaveType), waveType)) { throw new InvalidEnumArgumentException(nameof(waveType), (int)waveType, typeof(WaveType)); }

        var waveTypeAsByte = WaveTypeConverter.ToByte(waveType);

        return new CommandMode(waveTypeAsByte);
    }

    public static CommandMode From(BuiltInPattern builtInPattern) {
        if (!Enum.IsDefined(typeof(BuiltInPattern), builtInPattern)) { throw new InvalidEnumArgumentException(nameof(builtInPattern), (int)builtInPattern, typeof(BuiltInPattern)); }

        var builtInPatternAsByte = BuiltInPatternConverter.ToByte(builtInPattern);

        return new CommandMode(builtInPatternAsByte);
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

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { _value };
    }

    internal byte ToByte() {
        return _value;
    }

}