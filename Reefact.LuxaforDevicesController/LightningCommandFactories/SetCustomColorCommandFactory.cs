namespace Reefact.LuxaforDevicesController.LightningCommandFactories;

internal sealed class SetCustomColorCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly TargetedLeds _targetedLeds;
    private readonly LightColor   _lightColor;

    #endregion

    #region Constructors declarations

    public SetCustomColorCommandFactory(TargetedLeds targetedLeds, LightColor lightColor) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (lightColor is null) { throw new ArgumentNullException(nameof(lightColor)); }

        _targetedLeds = targetedLeds;
        _lightColor   = lightColor;
    }

    #endregion

    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_targetedLeds);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.SetColorWithoutFade, mode, _lightColor, Option.UnUsed, Option.UnUsed, Option.UnUsed, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_lightColor == LightColor.Off) { return Resources.LCF_TurnOffDevice; }

        return $"Set {_targetedLeds} color to {_lightColor}";
    }

}