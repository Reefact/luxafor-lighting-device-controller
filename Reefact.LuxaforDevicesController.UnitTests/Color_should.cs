#region Usings declarations

using NFluent;

#endregion

namespace Reefact.LuxaforDevicesController.UnitTests;

public class Color_should {

    [Theory]
    [InlineData("#0F11A8", 15, 17, 168)]
    [InlineData("#C4C4C4", 196, 196, 196)]
    [InlineData("#FF0099", 255, 0, 153)]
    public void be_created_from_its_hexadecimal_representation(string hexadecimalValue, byte expectedRedComponent, byte expectedGreenComponent, byte expectedBlueComponent) {
        // Exercise
        LightColor color = LightColor.From(hexadecimalValue);
        // Verify
        var rgb = color.ToRgb();
        Check.That(rgb.Red).IsEqualTo(expectedRedComponent);
        Check.That(rgb.Green).IsEqualTo(expectedGreenComponent);
        Check.That(rgb.Blue).IsEqualTo(expectedBlueComponent);
    }

    [Theory]
    [InlineData(15, 17, 168, "#0F11A8")]
    [InlineData(196, 196, 196, "#C4C4C4")]
    [InlineData(255, 0, 153, "#FF0099")]
    public void have_an_expressive_string_representation(byte redComponent, byte greenComponent, byte blueComponent, string expectedRepresentation) {
        // Setup
        LightColor color = new(redComponent, greenComponent, blueComponent);
        // Exercise
        var representation = color.ToString();
        // Verify
        Check.That(representation).IsEqualTo(expectedRepresentation);
    }

}