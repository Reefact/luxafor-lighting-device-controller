#region Usings declarations

using System.Collections.Generic;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Hid {

    /// <summary>
    ///     Gives access to the HID devices plugged into the machine.
    /// </summary>
    /// <remarks>
    ///     This abstraction is deliberately internal: it isolates the library from
    ///     <a href="https://github.com/mikeobrien/HidLibrary">HidLibrary</a> and makes the device lookup testable
    ///     without any hardware, but it is not part of the public API.
    /// </remarks>
    internal interface IHidDeviceRegistry {

        /// <summary>Enumerates the connected devices matching a vendor and a product identifier.</summary>
        /// <param name="vendorId">The USB vendor identifier.</param>
        /// <param name="productId">The USB product identifier.</param>
        /// <returns>An enumeration of <see cref="IHidDeviceHandle">device handles</see>.</returns>
        IEnumerable<IHidDeviceHandle> Enumerate(int vendorId, int productId);

        /// <summary>Gets the device located at the specified path.</summary>
        /// <param name="devicePath">The operating system path of the device.</param>
        /// <returns>The <see cref="IHidDeviceHandle">device handle</see>, or null when no device matches the path.</returns>
        IHidDeviceHandle? GetDevice(string devicePath);

    }

}
