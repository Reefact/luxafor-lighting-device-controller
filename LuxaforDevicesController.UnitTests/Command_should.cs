#region Usings declarations

using NFluent;

#endregion

namespace LuxaforDevicesController.UnitTests {

    public class Command_should {

        #region Statics members declarations

        private static readonly byte G = Convert.ToByte('G');

        #endregion

        [Fact]
        public void create_a_command_buffer_for_setting_basic_color() {
            // Setup
            var setGreenColor = Command.CreateSetBasicColorCommand(BasicColor.Green);
            // Exercise
            byte[] commandBuffer = setGreenColor.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 0, G, 0, 0, 0, 0, 0, 0);
        }

        [Fact]
        public void create_a_command_buffer_for_setting_color_without_fade() {
            // Setup
            var setTabSideRed = Command.CreateSetColorWithoutFadeCommand(TargetedLeds.TabSide, new Color(255, 0, 0));
            // Exercise
            byte[] commandBuffer = setTabSideRed.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 1, 42, 255, 0, 0, 0, 0, 0);
        }

        [Fact]
        public void create_a_command_buffer_for_setting_color_with_fade() {
            // Setup
            var fade = Command.CreateSetColorWithFadeCommand(TargetedLeds.Led_3, new Color(0, 0, 130), ChangingTime.From(50));
            // Exercise
            byte[] commandBuffer = fade.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 2, 3, 0, 0, 130, 50, 0, 0);
        }

        [Fact]
        public void create_a_command_buffer_for_flashing() {
            // Setup
            var strobe = Command.CreateActivateStrobeCommand(TargetedLeds.All, new Color(255, 255, 255), Speed.FromByte(25), Repeat.Count(10));
            // Exercise
            byte[] commandBuffer = strobe.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 3, 255, 255, 255, 255, 25, 0, 10);
        }

        [Fact]
        public void create_a_command_buffer_for_waving() {
            // Setup
            var wave = Command.CreateActivateWaveCommand(WaveType.Wave_3, new Color(200, 0, 200), Speed.FromByte(80), Repeat.Count(5));
            // Exercise
            byte[] commandBuffer = wave.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 4, 3, 200, 0, 200, 0, 5, 80);
        }

        [Fact]
        public void create_a_command_buffer_for_built_in_pattern() {
            // Setup
            var pattern = Command.CreateActivateBuiltInPatternCommand(BuiltInPattern.Police, Repeat.Count(33));
            // Exercise
            byte[] commandBuffer = pattern.ToBuffer();
            // Verify
            Check.That(commandBuffer).ContainsExactly(0, 6, 5, 33, 0, 0, 0, 0, 0);
        }

        // TODO: integrate productivity

    }

}