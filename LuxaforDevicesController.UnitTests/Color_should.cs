#region Usings declarations

using System.Drawing;

using NFluent;

#endregion

namespace LuxaforDevicesController.UnitTests;

public class Color_should {

    [Theory]
    [InlineData(33, 42, 120)]
    [InlineData(255, 0, 0)]
    [InlineData(95, 39, 241)]
    public void be_created_from_a_system_color(byte redComponent, byte greenComponent, byte blueComponent) {
        // Setup
        Color systemColor = Color.FromArgb(redComponent, greenComponent, blueComponent);
        // Exercise
        CustomColor color = CustomColor.From(systemColor);
        // Verify
        Check.That(color.Red).IsEqualTo(redComponent);
        Check.That(color.Green).IsEqualTo(greenComponent);
        Check.That(color.Blue).IsEqualTo(blueComponent);
    }

    [Theory]
    [InlineData("#0F11A8", 15, 17, 168)]
    [InlineData("#C4C4C4", 196, 196, 196)]
    [InlineData("#FF0099", 255, 0, 153)]
    public void be_created_from_its_hexadecimal_representation(string hexadecimalValue, byte expectedRedComponent, byte expectedGreenComponent, byte expectedBlueComponent) {
        // Exercise
        CustomColor color = CustomColor.From(hexadecimalValue);
        // Verify
        Check.That(color.Red).IsEqualTo(expectedRedComponent);
        Check.That(color.Green).IsEqualTo(expectedGreenComponent);
        Check.That(color.Blue).IsEqualTo(expectedBlueComponent);
    }

    [Theory]
    [InlineData(15, 17, 168, "#0F11A8")]
    [InlineData(196, 196, 196, "#C4C4C4")]
    [InlineData(255, 0, 153, "#FF0099")]
    public void have_an_expressive_string_representation(byte redComponent, byte greenComponent, byte blueComponent, string expectedRepresentation) {
        // Setup
        CustomColor color = new(redComponent, greenComponent, blueComponent);
        // Exercise
        var representation = color.ToString();
        // Verify
        Check.That(representation).IsEqualTo(expectedRepresentation);
    }

}