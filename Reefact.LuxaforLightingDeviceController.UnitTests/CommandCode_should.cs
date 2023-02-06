#region Usings declarations

using NFluent;

using Reefact.LuxaforLightingDeviceController;

using Xunit;

#endregion

namespace Reefact.LuxaforDevicesController.UnitTests {

    public class CommandCode_should {

        [Fact]
        public void have_a_byte_representation_in_accordance_with_the_Luxafor_protocol() {
            Check.That(CommandCode.SetColorWithoutFade.ToByte()).IsEqualTo(1);
            Check.That(CommandCode.SetColorWithFade.ToByte()).IsEqualTo(2);
            Check.That(CommandCode.ActivateStrobe.ToByte()).IsEqualTo(3);
            Check.That(CommandCode.ActivateWave.ToByte()).IsEqualTo(4);
            Check.That(CommandCode.ActivateBuiltInPatterns.ToByte()).IsEqualTo(6);
        }

    }

}