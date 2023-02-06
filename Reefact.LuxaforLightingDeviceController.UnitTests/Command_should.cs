#region Usings declarations

using NFluent;

using Reefact.LuxaforLightingDeviceController;

using Xunit;

#endregion

namespace Reefact.LuxaforDevicesController.UnitTests {

    public class Command_should {

        [Fact]
        public void create_a_command_buffer_for_setting_all_device_leds_to_primary_color() {
            // Setup
            var setGreenColor = LightingCommand.CreateSetColorCommand(BrightColor.Green);
            // Exercise
            byte[] commandBuffer = setGreenColor.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 1, 255, 0, 255, 0, 0, 0, 0);
        }

        [Fact]
        public void create_a_command_buffer_for_setting_color_without_fade() {
            // Setup
            var setTabSideRed = LightingCommand.CreateSetColorCommand(TargetedLeds.TabSide, new BrightColor(255, 0, 0));
            // Exercise
            byte[] commandBuffer = setTabSideRed.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 1, 66, 255, 0, 0, 0, 0, 0);
        }

        [Fact]
        public void create_a_command_buffer_for_setting_color_with_fade() {
            // Setup
            var fade = LightingCommand.CreateFadeColorCommand(TargetedLeds.Led_3, new BrightColor(0, 0, 130), FadeDuration.From(50));
            // Exercise
            byte[] commandBuffer = fade.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 2, 3, 0, 0, 130, 50, 0, 0);
        }

        [Fact]
        public void create_a_command_buffer_for_flashing() {
            // Setup
            var strobe = LightingCommand.CreateStrobeCommand(TargetedLeds.All, new BrightColor(255, 255, 255), Speed.FromByte(25), Repeat.Count(10));
            // Exercise
            byte[] commandBuffer = strobe.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 3, 255, 255, 255, 255, 25, 0, 10);
        }

        [Fact]
        public void create_a_command_buffer_for_waving() {
            // Setup
            var wave = LightingCommand.CreatePlayWavePatternCommand(WavePattern.Wave_3, new BrightColor(200, 0, 200), Speed.FromByte(80), Repeat.Count(5));
            // Exercise
            byte[] commandBuffer = wave.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 4, 3, 200, 0, 200, 0, 5, 80);
        }

        [Fact]
        public void create_a_command_buffer_for_built_in_pattern() {
            // Setup
            var pattern = LightingCommand.CreatePlayBuiltInPatternCommand(BuiltInPattern.Police, Repeat.Count(33));
            // Exercise
            byte[] commandBuffer = pattern.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 6, 5, 33, 0, 0, 0, 0, 0);
        }

    }

}