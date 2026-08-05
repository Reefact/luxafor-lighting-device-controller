#region Usings declarations

using NFluent;

using Reefact.LuxaforLightingDeviceController.UnitTests.Fakes;

using Xunit;

#endregion

namespace Reefact.LuxaforLightingDeviceController.UnitTests;

/// <summary>
///     Covers the device lookup rules behind <see cref="Luxafor" />. <see cref="Luxafor" /> itself is a one liner over
///     <see cref="LuxaforDeviceLocator" /> bound to the real USB ports, which cannot be exercised without hardware.
/// </summary>
public class Luxafor_should {

    [Fact]
    public void find_no_device_when_none_is_plugged() {
        // Setup
        FakeHidDeviceRegistry registry = FakeHidDeviceRegistry.Empty();
        // Exercise
        IEnumerable<LuxaforDevice> devices = LuxaforDeviceLocator.GetDevices(registry);
        // Verify
        Check.That(devices).IsEmpty();
    }

    [Fact]
    public void find_no_device_when_only_devices_of_other_brands_are_plugged() {
        // Setup
        FakeHidDeviceRegistry registry = FakeHidDeviceRegistry.Containing(FakeHidDeviceHandle.AnotherBrandDevice());
        // Exercise
        IEnumerable<LuxaforDevice> devices = LuxaforDeviceLocator.GetDevices(registry);
        // Verify
        Check.That(devices).IsEmpty();
    }

    [Fact]
    public void find_every_luxafor_device_plugged() {
        // Setup
        FakeHidDeviceRegistry registry = FakeHidDeviceRegistry.Containing(
            FakeHidDeviceHandle.ALuxaforDevice("hid#luxafor-1"),
            FakeHidDeviceHandle.AnotherBrandDevice(),
            FakeHidDeviceHandle.ALuxaforDevice("hid#luxafor-2"));
        // Exercise
        LuxaforDevice[] devices = LuxaforDeviceLocator.GetDevices(registry).ToArray();
        // Verify
        Check.That(devices.Select(device => device.Path)).ContainsExactly("hid#luxafor-1", "hid#luxafor-2");
    }

    [Fact]
    public void look_for_the_luxafor_vendor_and_product_identifiers() {
        // Setup
        FakeHidDeviceRegistry registry = FakeHidDeviceRegistry.Empty();
        // Exercise
        LuxaforDevice[] devices = LuxaforDeviceLocator.GetDevices(registry).ToArray();
        // Verify
        Check.That(devices).IsEmpty();
        Check.That(registry.EnumerationRequests).ContainsExactly((1240, 62322));
    }

    [Fact]
    public void refuse_a_null_device_path() {
        Check.ThatCode(() => LuxaforDeviceLocator.GetDevice(FakeHidDeviceRegistry.Empty(), null!))
             .Throws<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void refuse_a_blank_device_path(string devicePath) {
        Check.ThatCode(() => LuxaforDeviceLocator.GetDevice(FakeHidDeviceRegistry.Empty(), devicePath))
             .Throws<ArgumentException>();
    }

    [Fact]
    public void refuse_a_device_path_that_matches_no_hid_device() {
        // Setup
        FakeHidDeviceRegistry registry = FakeHidDeviceRegistry.Containing(FakeHidDeviceHandle.ALuxaforDevice("hid#luxafor-1"));
        // Exercise
        LuxaforDeviceNotFoundException exception = Assert.Throws<LuxaforDeviceNotFoundException>(() => LuxaforDeviceLocator.GetDevice(registry, "hid#unknown-path"));
        // Verify
        Check.That(exception.DevicePath).IsEqualTo("hid#unknown-path");
    }

    [Fact]
    public void refuse_a_device_path_that_matches_a_device_of_another_brand() {
        // Setup
        FakeHidDeviceHandle   otherDevice = FakeHidDeviceHandle.AnotherBrandDevice("hid#not-a-luxafor");
        FakeHidDeviceRegistry registry    = FakeHidDeviceRegistry.Containing(otherDevice);
        // Exercise
        Check.ThatCode(() => LuxaforDeviceLocator.GetDevice(registry, "hid#not-a-luxafor"))
             .Throws<LuxaforDeviceNotFoundException>();
        // Verify: the rejected device is released instead of being leaked.
        Check.That(otherDevice.DisposeCount).IsEqualTo(1);
    }

    [Fact]
    public void refuse_a_device_that_is_not_connected_anymore() {
        // Setup
        FakeHidDeviceHandle unpluggedDevice = FakeHidDeviceHandle.ALuxaforDevice("hid#luxafor-1");
        unpluggedDevice.IsConnected = false;
        FakeHidDeviceRegistry registry = FakeHidDeviceRegistry.Containing(unpluggedDevice);
        // Exercise
        Check.ThatCode(() => LuxaforDeviceLocator.GetDevice(registry, "hid#luxafor-1"))
             .Throws<LuxaforDeviceNotFoundException>();
        // Verify
        Check.That(unpluggedDevice.DisposeCount).IsEqualTo(1);
    }

    [Fact]
    public void open_the_luxafor_device_located_at_the_requested_path() {
        // Setup
        FakeHidDeviceHandle   luxafor  = FakeHidDeviceHandle.ALuxaforDevice("hid#luxafor-1");
        FakeHidDeviceRegistry registry = FakeHidDeviceRegistry.Containing(FakeHidDeviceHandle.AnotherBrandDevice(), luxafor);
        // Exercise
        using LuxaforDevice device = LuxaforDeviceLocator.GetDevice(registry, "hid#luxafor-1");
        // Verify
        Check.That(device.Path).IsEqualTo("hid#luxafor-1");
        Check.That(luxafor.DisposeCount).IsEqualTo(0);
        Check.That(device.SetColor(BrightColor.Red)).IsTrue();
        Check.That(luxafor.WrittenBuffers).HasSize(1);
    }

    [Fact]
    public void refuse_to_work_without_a_hid_registry() {
        Check.ThatCode(() => LuxaforDeviceLocator.GetDevices(null!)).Throws<ArgumentNullException>();
        Check.ThatCode(() => LuxaforDeviceLocator.GetDevice(null!, "hid#luxafor-1")).Throws<ArgumentNullException>();
    }

}
