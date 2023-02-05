#region Usings declarations

using System.ComponentModel;

#endregion

namespace Reefact.LuxaforDevicesController.LightningCommandFactories;

internal sealed class WaveCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly WaveType   _waveType;
    private readonly LightColor _lightColor;
    private readonly Speed      _speed;
    private readonly Repeat     _repeat;

    #endregion

    #region Constructors declarations

    public WaveCommandFactory(WaveType waveType, LightColor lightColor, Speed speed, Repeat repeat) {
        if (lightColor is null) { throw new ArgumentNullException(nameof(lightColor)); }
        if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }
        if (!Enum.IsDefined(typeof(WaveType), waveType)) { throw new InvalidEnumArgumentException(nameof(waveType), (int)waveType, typeof(WaveType)); }

        _waveType   = waveType;
        _lightColor = lightColor;
        _speed      = speed;
        _repeat     = repeat;
    }

    #endregion

    /// <inheritdoc />
    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_waveType);
        Option           option2              = Option.From(_repeat);
        Option           option3              = Option.From(_speed);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.ActivateWave, mode, _lightColor, Option.UnUsed, option2, option3, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_lightColor == LightColor.Off) { return Resources.LCF_TurnOffDevice; }

        return $"Play a wave pattern ({_waveType}) {_repeat} with a base color {_lightColor} (speed={_speed})";
    }

}