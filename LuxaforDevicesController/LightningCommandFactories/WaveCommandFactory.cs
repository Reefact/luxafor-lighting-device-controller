#region Usings declarations

using System.ComponentModel;

#endregion

namespace LuxaforDevicesController.LightningCommandFactories;

internal sealed class WaveCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly WaveType    _waveType;
    private readonly CustomColor _customColor;
    private readonly Speed       _speed;
    private readonly Repeat      _repeat;

    #endregion

    #region Constructors declarations

    public WaveCommandFactory(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat) {
        if (customColor is null) { throw new ArgumentNullException(nameof(customColor)); }
        if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }
        if (!Enum.IsDefined(typeof(WaveType), waveType)) { throw new InvalidEnumArgumentException(nameof(waveType), (int)waveType, typeof(WaveType)); }

        _waveType    = waveType;
        _customColor = customColor;
        _speed       = speed;
        _repeat      = repeat;
    }

    #endregion

    /// <inheritdoc />
    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_waveType);
        Option           option2              = Option.From(_repeat);
        Option           option3              = Option.From(_speed);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.ActivateWave, mode, _customColor, Option.UnUsed, option2, option3, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_customColor == CustomColor.Off) { return Resources.LCF_TurnOffDevice; }

        return $"Play a wave pattern ({_waveType}) {_repeat} with a base color {_customColor} (speed={_speed})";
    }

}