using System;

namespace Reefact.LuxaforLightingDeviceController.LightingCommandFactories {

internal sealed class SetColorCommandFactory : LightingCommandFactory {

    #region Fields declarations

    private readonly TargetedLeds _targetedLeds;
    private readonly BrightColor  _color;

    #endregion

    #region Constructors declarations

    public SetColorCommandFactory(TargetedLeds targetedLeds, BrightColor color) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (color is null) { throw new ArgumentNullException(nameof(color)); }

        _targetedLeds = targetedLeds;
        _color        = color;
    }

    #endregion

    public LightingCommand Create() {
        CommandMode      mode                 = CommandMode.From(_targetedLeds);
        string           stringRepresentation = CreateStringRepresentation();
        LightingCommand command              = new LightingCommand(CommandCode.SetColorWithoutFade, mode, _color, Option.UnUsed, Option.UnUsed, Option.UnUsed, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_color == BrightColor.Black) { return "Turn off the device"; }

        return $"Set {_targetedLeds} color to {_color}";
    }

}
}