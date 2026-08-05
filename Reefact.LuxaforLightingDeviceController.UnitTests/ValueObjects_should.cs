#region Usings declarations

using NFluent;

using Xunit;

#endregion

namespace Reefact.LuxaforLightingDeviceController.UnitTests;

/// <summary>
///     The types of the API are value objects: two instances carrying the same value are equal, whatever the way they
///     have been built. These tests pin that contract, which used to be inherited from the Value library.
/// </summary>
public class ValueObjects_should {

    [Fact]
    public void consider_two_bright_colors_with_the_same_components_as_equal() {
        // Setup
        BrightColor color        = new(15, 17, 168);
        BrightColor sameColor    = BrightColor.From("#0F11A8");
        BrightColor anotherColor = BrightColor.From(15, 17, 169);
        // Verify
        Check.That(color).IsEqualTo(sameColor);
        Check.That(color.GetHashCode()).IsEqualTo(sameColor.GetHashCode());
        Check.That(color == sameColor).IsTrue();
        Check.That(color != sameColor).IsFalse();
        Check.That(color).IsNotEqualTo(anotherColor);
        Check.That(color == anotherColor).IsFalse();
        Check.That(color != anotherColor).IsTrue();
    }

    [Fact]
    public void consider_the_predefined_bright_colors_as_equal_to_their_components() {
        Check.That(BrightColor.Red).IsEqualTo(new BrightColor(255, 0, 0));
        Check.That(BrightColor.Black).IsEqualTo(BrightColor.From("#000000"));
        Check.That(BrightColor.White).IsEqualTo(BrightColor.From("#FFFFFF"));
    }

    [Fact]
    public void handle_null_and_foreign_types_when_comparing_values() {
        // Setup
        BrightColor  color = BrightColor.Red;
        BrightColor? none  = null;
        // Verify
        Check.That(color.Equals("#FF0000")).IsFalse();
        Check.That(color == none).IsFalse();
        Check.That(none == color).IsFalse();
        Check.That(none != color).IsTrue();
        Check.That(none == null).IsTrue();
        Check.That(color.Equals(none)).IsFalse();
    }

    [Fact]
    public void consider_two_speeds_with_the_same_value_as_equal() {
        Check.That(Speed.FromByte(20)).IsEqualTo(Speed.FromByte(20));
        Check.That(Speed.FromByte(20).GetHashCode()).IsEqualTo(Speed.FromByte(20).GetHashCode());
        Check.That(Speed.FromByte(20)).IsNotEqualTo(Speed.FromByte(21));
    }

    [Fact]
    public void consider_two_repeats_with_the_same_value_as_equal() {
        Check.That(Repeat.Count(1)).IsEqualTo(Repeat.Once);
        Check.That(Repeat.Count(2)).IsEqualTo(Repeat.Twice);
        Check.That(Repeat.Count(2)).IsNotEqualTo(Repeat.Once);
    }

    [Fact]
    public void consider_two_fade_durations_with_the_same_value_as_equal() {
        Check.That(FadeDuration.From(50)).IsEqualTo(FadeDuration.From(50));
        Check.That(FadeDuration.From(50)).IsNotEqualTo(FadeDuration.From(51));
    }

    [Fact]
    public void consider_two_led_indexes_with_the_same_value_as_equal() {
        Check.That(LedIndex.From(3)).IsEqualTo(LedIndex._3);
        Check.That(LedIndex.From(3) == LedIndex._3).IsTrue();
        Check.That(LedIndex.From(3) != LedIndex._4).IsTrue();
    }

    [Fact]
    public void consider_two_targeted_leds_with_the_same_lux_code_as_equal() {
        Check.That(TargetedLeds.FromLuxCode(1)).IsEqualTo(TargetedLeds.Led_1);
        Check.That(TargetedLeds.FromLuxCode(255)).IsEqualTo(TargetedLeds.All);
        Check.That(TargetedLeds.Led_1).IsNotEqualTo(TargetedLeds.Led_2);
    }

    [Fact]
    public void consider_two_lighting_commands_producing_the_same_buffer_as_equal() {
        // Setup
        LightingCommand turnOff       = LightingCommand.CreateTurnOffCommand();
        LightingCommand setBlack      = LightingCommand.CreateSetColorCommand(TargetedLeds.All, BrightColor.Black);
        LightingCommand setTabSideRed = LightingCommand.CreateSetColorCommand(TargetedLeds.TabSide, BrightColor.Red);
        // Verify
        Check.That(turnOff).IsEqualTo(setBlack);
        Check.That(turnOff.GetHashCode()).IsEqualTo(setBlack.GetHashCode());
        Check.That(turnOff == setBlack).IsTrue();
        Check.That(turnOff).IsNotEqualTo(setTabSideRed);
        Check.That(turnOff != setTabSideRed).IsTrue();
    }

    [Fact]
    public void give_each_value_object_an_expressive_string_representation() {
        Check.That(BrightColor.From(15, 17, 168).ToString()).IsEqualTo("#0F11A8");
        Check.That(Speed.FromByte(20).ToString()).IsEqualTo("20");
        Check.That(FadeDuration.From(50).ToString()).IsEqualTo("50");
        Check.That(LedIndex._4.ToString()).IsEqualTo("4");
        Check.That(TargetedLeds.TabSide.ToString()).IsEqualTo("tab side LEDs");
        Check.That(Repeat.Count(0).ToString()).IsEqualTo("none");
        Check.That(Repeat.Once.ToString()).IsEqualTo("once");
        Check.That(Repeat.Twice.ToString()).IsEqualTo("twice");
        Check.That(Repeat.Count(5).ToString()).IsEqualTo("5 times");
        Check.That(LightingCommand.CreateSetColorCommand(TargetedLeds.TabSide, BrightColor.Red).ToString()).IsEqualTo("Set tab side LEDs color to #FF0000");
    }

    [Fact]
    public void be_usable_as_dictionary_keys() {
        // Setup
        Dictionary<BrightColor, string> names = new() { { BrightColor.From(255, 0, 0), "red" } };
        // Verify
        Check.That(names[BrightColor.Red]).IsEqualTo("red");
    }

}
