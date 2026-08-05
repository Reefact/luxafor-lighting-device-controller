#region Usings declarations

using Reefact.LuxaforLightingDeviceController.Hid;

#endregion

namespace Reefact.LuxaforLightingDeviceController.UnitTests.Fakes;

/// <summary>
///     An in-memory <see cref="IHidDeviceRegistry">HID registry</see>: it plays the role of the USB ports of the
///     machine, without any hardware.
/// </summary>
internal sealed class FakeHidDeviceRegistry : IHidDeviceRegistry {

    #region Statics members declarations

    public static FakeHidDeviceRegistry Empty() {
        return new FakeHidDeviceRegistry();
    }

    public static FakeHidDeviceRegistry Containing(params FakeHidDeviceHandle[] devices) {
        FakeHidDeviceRegistry registry = new();
        registry._devices.AddRange(devices);

        return registry;
    }

    #endregion

    #region Fields declarations

    private readonly List<FakeHidDeviceHandle> _devices = new();

    #endregion

    /// <summary>Gets the (vendor id, product id) couples the library asked for.</summary>
    public List<(int VendorId, int ProductId)> EnumerationRequests { get; } = new();

    /// <inheritdoc />
    public IEnumerable<IHidDeviceHandle> Enumerate(int vendorId, int productId) {
        EnumerationRequests.Add((vendorId, productId));

        return _devices.Where(device => device.VendorId == vendorId && device.ProductId == productId);
    }

    /// <inheritdoc />
    public IHidDeviceHandle? GetDevice(string devicePath) {
        return _devices.FirstOrDefault(device => device.DevicePath == devicePath);
    }

}
