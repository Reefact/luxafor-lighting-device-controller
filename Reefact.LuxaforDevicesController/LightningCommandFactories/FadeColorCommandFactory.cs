namespace Reefact.LuxaforDevicesController.LightningCommandFactories;

internal sealed class FadeColorCommandFactory : LightningCommandFactory {

    #region Fields declarations

    private readonly TargetedLeds _targetedLeds;
    private readonly LightColor   _lightColor;
    private readonly FadeDuration _duration;

    #endregion

    #region Constructors declarations

    public FadeColorCommandFactory(TargetedLeds targetedLeds, LightColor lightColor, FadeDuration duration) {
        if (targetedLeds is null) { throw new ArgumentNullException(nameof(targetedLeds)); }
        if (lightColor is null) { throw new ArgumentNullException(nameof(lightColor)); }
        if (duration is null) { throw new ArgumentNullException(nameof(duration)); }

        _targetedLeds = targetedLeds;
        _lightColor   = lightColor;
        _duration     = duration;
    }

    #endregion

    public LightningCommand Create() {
        CommandMode      mode                 = CommandMode.From(_targetedLeds);
        Option           option1              = Option.From(_duration);
        string           stringRepresentation = CreateStringRepresentation();
        LightningCommand command              = new(CommandCode.SetColorWithFade, mode, _lightColor, option1, Option.UnUsed, Option.UnUsed, stringRepresentation);

        return command;
    }

    private string CreateStringRepresentation() {
        if (_lightColor == LightColor.Off) { return Resources.LCF_TurnOffDevice; }

        return $"Fade {_targetedLeds} color to {_lightColor} over a duration od {_duration} units";
    }

}