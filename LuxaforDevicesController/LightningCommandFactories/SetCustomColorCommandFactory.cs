namespace LuxaforDevicesController.LightningCommandFactories;

internal sealed class SetCustomColorCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly TargetedLeds _targetedLeds;
    private readonly CustomColor  _customColor;

    #endregion

    #region Constructors declarations

    public SetCustomColorCommandFactory(TargetedLeds targetedLeds, CustomColor customColor) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (customColor is null) { throw new ArgumentNullException(nameof(customColor)); }

        _targetedLeds = targetedLeds;
        _customColor  = customColor;
    }

    #endregion

    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_targetedLeds);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.SetColorWithoutFade, mode, _customColor, Option.UnUsed, Option.UnUsed, Option.UnUsed, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_customColor == CustomColor.Off) { return Resources.LCF_TurnOffDevice; }

        return $"Set {_targetedLeds} color to {_customColor}";
    }

}