﻿namespace Reefact.LuxaforDevicesController.LightningCommandFactories;

internal sealed class StrobeCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly TargetedLeds _targetedLeds;
    private readonly BrightColor  _color;
    private readonly Speed        _speed;
    private readonly Repeat       _repeat;

    #endregion

    #region Constructors declarations

    public StrobeCommandFactory(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (color is null) { throw new ArgumentNullException(nameof(color)); }
        if (speed is null) { throw new ArgumentNullException(nameof(speed)); }
        if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }

        _targetedLeds = targetedLeds;
        _color        = color;
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
        LightningCommand command              = new(CommandCode.ActivateStrobe, mode, _color, option1, Option.UnUsed, option3, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_color == BrightColor.Black) { return Resources.LCF_TurnOffDevice; }

        return $"Strobe {_targetedLeds} {_repeat} with color {_color} (speed={_speed})";
    }

}