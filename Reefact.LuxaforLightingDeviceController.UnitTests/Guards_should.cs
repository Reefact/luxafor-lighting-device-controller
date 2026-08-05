#region Usings declarations

using System.ComponentModel;

using NFluent;

using Xunit;

#endregion

namespace Reefact.LuxaforLightingDeviceController.UnitTests;

/// <summary>
///     Pins the errors reported by the API when it is fed with invalid arguments.
/// </summary>
public class Guards_should {

    [Fact]
    public void reject_a_null_hexadecimal_color() {
        Check.ThatCode(() => BrightColor.From(null!)).Throws<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("0F11A8")]
    [InlineData("#0F11A")]
    [InlineData("#0F11A8A")]
    [InlineData("#0G11A8")]
    public void reject_an_invalid_hexadecimal_color(string hexadecimalValue) {
        Check.ThatCode(() => BrightColor.From(hexadecimalValue)).Throws<FormatException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(7)]
    [InlineData(255)]
    public void reject_an_out_of_range_led_index(byte value) {
        Check.ThatCode(() => LedIndex.From(value)).Throws<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void reject_an_unknown_lux_code() {
        Check.ThatCode(() => TargetedLeds.FromLuxCode(42)).Throws<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void reject_null_arguments_when_creating_a_lighting_command() {
        Check.ThatCode(() => LightingCommand.CreateSetColorCommand(null!)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreateSetColorCommand(null!, BrightColor.Red)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreateSetColorCommand(TargetedLeds.All, null!)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreateTurnOffCommand(null!)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreateFadeColorCommand(TargetedLeds.All, BrightColor.Red, null!)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Red, null!, Repeat.Once)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Red, Speed.FromByte(1), null!)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreatePlayWavePatternCommand(WavePattern.Wave_1, null!, Speed.FromByte(1), Repeat.Once)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LightingCommand.CreatePlayBuiltInPatternCommand(BuiltInPattern.Police, null!)).Throws<ArgumentNullException>();
    }

    [Fact]
    public void reject_an_undefined_pattern() {
        Check.ThatCode(() => LightingCommand.CreatePlayWavePatternCommand((WavePattern)42, BrightColor.Red, Speed.FromByte(1), Repeat.Once))
             .Throws<InvalidEnumArgumentException>();
        Check.ThatCode(() => LightingCommand.CreatePlayBuiltInPatternCommand((BuiltInPattern)42, Repeat.Once))
             .Throws<InvalidEnumArgumentException>();
    }

}
