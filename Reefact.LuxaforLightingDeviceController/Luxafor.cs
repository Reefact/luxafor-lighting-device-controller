#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LuxaforLightingDeviceController.Hid;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     The entry point of the library to retrieve Luxafor <see cref="ILuxaforDevice">devices</see> connected to the
    ///     machine's USB ports.
    /// </summary>
    public static class Luxafor {

        #region Statics members declarations

        /// <summary>
        ///     Gets all Luxafor <see cref="ILuxaforDevice">devices</see> connected to the USB
        ///     ports.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{ILuxaforDevice}">enumeration</see> of devices.</returns>
        public static IEnumerable<ILuxaforDevice> GetDevices() {
            return LuxaforDeviceLocator.GetDevices(HidLibraryDeviceRegistry.Instance);
        }

        /// <summary>
        ///     Get the specified Luxafor <see cref="ILuxaforDevice">device</see>.
        /// </summary>
        /// <param name="devicePath">The path of the <see cref="ILuxaforDevice">device</see> to retrieve.</param>
        /// <returns>The <see cref="ILuxaforDevice">device</see>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="devicePath" /> is null.</exception>
        /// <exception cref="ArgumentException">Argument <paramref name="devicePath" /> is empty.</exception>
        /// <exception cref="LuxaforDeviceNotFoundException">
        ///     No device exists at <paramref name="devicePath" />, or the device found there is not a connected and supported
        ///     Luxafor device.
        /// </exception>
        public static ILuxaforDevice GetDevice(string devicePath) {
            return LuxaforDeviceLocator.GetDevice(HidLibraryDeviceRegistry.Instance, devicePath);
        }

        #endregion

    }

}
