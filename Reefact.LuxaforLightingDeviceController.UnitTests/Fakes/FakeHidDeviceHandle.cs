#region Usings declarations

using Reefact.LuxaforLightingDeviceController.Hid;

#endregion

namespace Reefact.LuxaforLightingDeviceController.UnitTests.Fakes;

/// <summary>
///     An in-memory <see cref="IHidDeviceHandle">HID device</see> that records what the library writes to it.
/// </summary>
internal sealed class FakeHidDeviceHandle : IHidDeviceHandle {

    #region Statics members declarations

    public static FakeHidDeviceHandle ALuxaforDevice(string devicePath = "hid#vid_04d8&pid_f372#luxafor-1") {
        return new FakeHidDeviceHandle(devicePath);
    }

    public static FakeHidDeviceHandle AFailingLuxaforDevice(string devicePath = "hid#vid_04d8&pid_f372#luxafor-1") {
        return new FakeHidDeviceHandle(devicePath) { WriteSucceeds = false };
    }

    public static FakeHidDeviceHandle AnotherBrandDevice(string devicePath = "hid#vid_dead&pid_beef#other-1") {
        return new FakeHidDeviceHandle(devicePath) { VendorId = 999, ProductId = 111 };
    }

    #endregion

    #region Constructors declarations

    private FakeHidDeviceHandle(string devicePath) {
        DevicePath = devicePath;
    }

    #endregion

    public string DevicePath  { get; }
    public string Description { get; set; } = "Luxafor (fake)";
    public int    VendorId    { get; set; } = LuxaforDeviceLocator.VendorId;
    public int    ProductId   { get; set; } = LuxaforDeviceLocator.ProductId;
    public bool   IsConnected { get; set; } = true;

    /// <summary>Gets or sets what <see cref="Write" /> returns, e.g. whether the device accepts the writes.</summary>
    public bool WriteSucceeds { get; set; } = true;

    /// <summary>Gets the buffers written to the device, in order.</summary>
    public List<byte[]> WrittenBuffers { get; } = new();

    /// <summary>Gets how many times the device has been disposed.</summary>
    public int DisposeCount { get; private set; }

    /// <inheritdoc />
    public bool Write(byte[] data) {
        WrittenBuffers.Add(data);

        return WriteSucceeds;
    }

    /// <inheritdoc />
    public void Dispose() {
        DisposeCount++;
    }

}
