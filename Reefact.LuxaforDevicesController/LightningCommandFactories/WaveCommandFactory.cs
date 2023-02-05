#region Usings declarations

using System.ComponentModel;

#endregion

namespace Reefact.LuxaforDevicesController.LightningCommandFactories;

internal sealed class WaveCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly WaveType    _waveType;
    private readonly BrightColor _color;
    private readonly Speed       _speed;
    private readonly Repeat      _repeat;

    #endregion

    #region Constructors declarations

    public WaveCommandFactory(WaveType waveType, BrightColor color, Speed speed, Repeat repeat) {
        if (color is null) { throw new ArgumentNullException(nameof(color)); }
        if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }
        if (!Enum.IsDefined(typeof(WaveType), waveType)) { throw new InvalidEnumArgumentException(nameof(waveType), (int)waveType, typeof(WaveType)); }

        _waveType = waveType;
        _color    = color;
        _speed    = speed;
        _repeat   = repeat;
    }

    #endregion

    /// <inheritdoc />
    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_waveType);
        Option           option2              = Option.From(_repeat);
        Option           option3              = Option.From(_speed);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.ActivateWave, mode, _color, Option.UnUsed, option2, option3, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_color == BrightColor.Black) { return Resources.LCF_TurnOffDevice; }

        return $"Play a wave pattern ({_waveType}) {_repeat} with a base color {_color} (speed={_speed})";
    }

}