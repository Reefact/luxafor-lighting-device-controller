namespace LuxaforDevicesController.LightningCommandFactories;

internal sealed class FadeColorCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly TargetedLeds _targetedLeds;
    private readonly CustomColor  _customColor;
    private readonly FadeDuration _duration;

    #endregion

    #region Constructors declarations

    public FadeColorCommandFactory(TargetedLeds targetedLeds, CustomColor customColor, FadeDuration duration) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (customColor is null) { throw new ArgumentNullException(nameof(customColor)); }
        if (duration is null) { throw new ArgumentNullException(nameof(duration)); }

        _targetedLeds = targetedLeds;
        _customColor  = customColor;
        _duration     = duration;
    }

    #endregion

    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_targetedLeds);
        Option           option1              = Option.From(_duration);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.SetColorWithFade, mode, _customColor, option1, Option.UnUsed, Option.UnUsed, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_customColor == CustomColor.Off) { return Resources.LCF_TurnOffDevice; }

        return $"Fade {_targetedLeds} color to {_customColor} over a duration od {_duration} units";
    }

}