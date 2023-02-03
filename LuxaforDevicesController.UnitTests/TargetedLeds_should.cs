#region Usings declarations

using NFluent;

#endregion

namespace LuxaforDevicesController.UnitTests {

    public class TargetedLeds_should {

        [Fact]
        public void have_a_byte_representation_in_accordance_with_the_Luxafor_protocol() {
            Check.That(TargetedLeds.Led_1.ToByte()).IsEqualTo(1);
            Check.That(TargetedLeds.Led_2.ToByte()).IsEqualTo(2);
            Check.That(TargetedLeds.Led_3.ToByte()).IsEqualTo(3);
            Check.That(TargetedLeds.Led_4.ToByte()).IsEqualTo(4);
            Check.That(TargetedLeds.Led_5.ToByte()).IsEqualTo(5);
            Check.That(TargetedLeds.Led_6.ToByte()).IsEqualTo(6);
            Check.That(TargetedLeds.BackSide.ToByte()).IsEqualTo(Convert.ToByte('A'));
            Check.That(TargetedLeds.TabSide.ToByte()).IsEqualTo(Convert.ToByte('B'));
            Check.That(TargetedLeds.All.ToByte()).IsEqualTo(255);
        }

    }

}