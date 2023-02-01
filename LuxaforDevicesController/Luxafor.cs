#region Usings declarations

#endregion

namespace LuxaforDevicesController;

public static class Luxafor {

    private const int VendorId  = 1240;
    private const int ProductId = 62322;

    #region Statics members declarations

    public static IEnumerable<LuxaforDevice> GetDevices() {
        IHidEnumerator             hidEnumerator  = new HidEnumerator();
        IEnumerable<LuxaforDevice> luxaforDevices = hidEnumerator.Enumerate(VendorId, ProductId).Select(t => new LuxaforDevice(t));

        return luxaforDevices;
    }

    public static LuxaforDevice GetDevice(string devicePath) {
        if (devicePath is null) { throw new ArgumentNullException(nameof(devicePath)); }

        IHidEnumerator hidEnumerator = new HidEnumerator();
        IHidDevice     target        = hidEnumerator.GetDevice(devicePath);
        LuxaforDevice  luxaforDevice = new(target);

        return luxaforDevice;
    }

    #endregion

}