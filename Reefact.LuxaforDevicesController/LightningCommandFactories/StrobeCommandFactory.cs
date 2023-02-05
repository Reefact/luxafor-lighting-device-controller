namespace Reefact.LuxaforDevicesController.LightningCommandFactories;

internal sealed class StrobeCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly TargetedLeds _targetedLeds;
    private readonly LightColor   _lightColor;
    private readonly Speed        _speed;
    private readonly Repeat       _repeat;

    #endregion

    #region Constructors declarations

    public StrobeCommandFactory(TargetedLeds targetedLeds, LightColor lightColor, Speed speed, Repeat repeat) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (lightColor is null) { throw new ArgumentNullException(nameof(lightColor)); }
        if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

        _targetedLeds = targetedLeds;
        _lightColor   = lightColor;
        _speed        = speed;
        _repeat       = repeat;
    }

    #endregion

    /// <inheritdoc />
    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_targetedLeds);
        Option           option1              = Option.From(_speed);
        Option           option3              = Option.From(_repeat);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.ActivateStrobe, mode, _lightColor, option1, Option.UnUsed, option3, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_lightColor == LightColor.Off) { return Resources.LCF_TurnOffDevice; }

        return $"Strobe {_targetedLeds} {_repeat} with color {_lightColor} (speed={_speed})";
    }

}