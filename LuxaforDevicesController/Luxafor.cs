#region Usings declarations

#endregion

#region Usings declarations

using HidLibrary;

#endregion

namespace LuxaforDevicesController;

/// <summary>
///     The entry point of the library to retrieve Luxafor <see cref="LuxaforDevice">devices</see> connected to the
///     machine's USB ports.
/// </summary>
public static class Luxafor {

    private const int VendorId  = 1240;
    private const int ProductId = 62322;

    #region Statics members declarations

    /// <summary>
    ///     Gets all Luxafor <see cref="LuxaforDevice">devices</see> connected to the USB
    ///     ports.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{LuxaforDevice}">enumeration</see> of devices.</returns>
    public static IEnumerable<LuxaforDevice> GetDevices() {
        IHidEnumerator             hidEnumerator  = new HidEnumerator();
        IEnumerable<LuxaforDevice> luxaforDevices = hidEnumerator.Enumerate(VendorId, ProductId).Select(t => new LuxaforDevice(t));

        return luxaforDevices;
    }

    /// <summary>
    ///     Get the specified Luxafor <see cref="LuxaforDevice">device</see>.
    /// </summary>
    /// <param name="devicePath">The path of the <see cref="LuxaforDevice">device</see> to retrieve.</param>
    /// <returns>The <see cref="LuxaforDevice">device</see>.</returns>
    /// <exception cref="ArgumentNullException">Argument <paramref name="devicePath" /> is null.</exception>
    public static LuxaforDevice GetDevice(string devicePath) {
        if (devicePath is null) { throw new ArgumentNullException(nameof(devicePath)); }

        IHidEnumerator hidEnumerator = new HidEnumerator();
        IHidDevice     target        = hidEnumerator.GetDevice(devicePath);
        LuxaforDevice  luxaforDevice = new(target);

        return luxaforDevice;
    }

    #endregion

}