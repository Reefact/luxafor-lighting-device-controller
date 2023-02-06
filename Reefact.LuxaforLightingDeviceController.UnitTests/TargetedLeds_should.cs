#region Usings declarations

using NFluent;

using Reefact.LuxaforLightingDeviceController;

using Xunit;

#endregion

namespace Reefact.LuxaforDevicesController.UnitTests {

    public class TargetedLeds_should {

        [Fact]
        public void have_a_byte_representation_in_accordance_with_the_Luxafor_protocol() {
            Check.That(TargetedLeds.Led_1.ToLuxCode()).IsEqualTo(1);
            Check.That(TargetedLeds.Led_2.ToLuxCode()).IsEqualTo(2);
            Check.That(TargetedLeds.Led_3.ToLuxCode()).IsEqualTo(3);
            Check.That(TargetedLeds.Led_4.ToLuxCode()).IsEqualTo(4);
            Check.That(TargetedLeds.Led_5.ToLuxCode()).IsEqualTo(5);
            Check.That(TargetedLeds.Led_6.ToLuxCode()).IsEqualTo(6);
            Check.That(TargetedLeds.BackSide.ToLuxCode()).IsEqualTo(Convert.ToByte('A'));
            Check.That(TargetedLeds.TabSide.ToLuxCode()).IsEqualTo(Convert.ToByte('B'));
            Check.That(TargetedLeds.All.ToLuxCode()).IsEqualTo(255);
        }

    }

}