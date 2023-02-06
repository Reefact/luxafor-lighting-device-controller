#region Usings declarations

using NFluent;

using Reefact.LuxaforLightingDeviceController;

using Xunit;

#endregion

namespace Reefact.LuxaforDevicesController.UnitTests {

    public class CommandMode_should {

        [Fact]
        public void have_a_byte_representation_in_accordance_with_the_Luxafor_when_targeting_leds() {
            Check.That(CommandMode.From(TargetedLeds.All).ToByte()).IsEqualTo(255);
            Check.That(CommandMode.From(TargetedLeds.BackSide).ToByte()).IsEqualTo(Convert.ToByte('A'));
            Check.That(CommandMode.From(TargetedLeds.TabSide).ToByte()).IsEqualTo(Convert.ToByte('B'));
            Check.That(CommandMode.From(TargetedLeds.Led_1).ToByte()).IsEqualTo(1);
            Check.That(CommandMode.From(TargetedLeds.Led_2).ToByte()).IsEqualTo(2);
            Check.That(CommandMode.From(TargetedLeds.Led_3).ToByte()).IsEqualTo(3);
            Check.That(CommandMode.From(TargetedLeds.Led_4).ToByte()).IsEqualTo(4);
            Check.That(CommandMode.From(TargetedLeds.Led_5).ToByte()).IsEqualTo(5);
            Check.That(CommandMode.From(TargetedLeds.Led_6).ToByte()).IsEqualTo(6);
        }

        [Fact]
        public void have_a_byte_representation_in_accordance_with_the_Luxafor_when_waving() {
            Check.That(CommandMode.From(WaveType.Wave_1).ToByte()).IsEqualTo(1);
            Check.That(CommandMode.From(WaveType.Wave_2).ToByte()).IsEqualTo(2);
            Check.That(CommandMode.From(WaveType.Wave_3).ToByte()).IsEqualTo(3);
            Check.That(CommandMode.From(WaveType.Wave_4).ToByte()).IsEqualTo(4);
            Check.That(CommandMode.From(WaveType.Wave_5).ToByte()).IsEqualTo(5);
        }

        [Fact]
        public void have_a_byte_representation_in_accordance_with_the_Luxafor_when_playing_pattern() {
            Check.That(CommandMode.From(BuiltInPattern.Off).ToByte()).IsEqualTo(0);
            Check.That(CommandMode.From(BuiltInPattern.TrafficLight).ToByte()).IsEqualTo(1);
            Check.That(CommandMode.From(BuiltInPattern.Pattern_1).ToByte()).IsEqualTo(2);
            Check.That(CommandMode.From(BuiltInPattern.Pattern_2).ToByte()).IsEqualTo(3);
            Check.That(CommandMode.From(BuiltInPattern.Pattern_3).ToByte()).IsEqualTo(4);
            Check.That(CommandMode.From(BuiltInPattern.Police).ToByte()).IsEqualTo(5);
            Check.That(CommandMode.From(BuiltInPattern.Pattern_4).ToByte()).IsEqualTo(6);
            Check.That(CommandMode.From(BuiltInPattern.Pattern_5).ToByte()).IsEqualTo(7);
            Check.That(CommandMode.From(BuiltInPattern.Rainbow).ToByte()).IsEqualTo(8);
        }

    }

}