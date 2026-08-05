#region Usings declarations

using System;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Hid {

    /// <summary>
    ///     The minimal view of a HID device required by the library.
    /// </summary>
    /// <remarks>
    ///     This abstraction is deliberately internal: it isolates the library from
    ///     <a href="https://github.com/mikeobrien/HidLibrary">HidLibrary</a> and makes the device logic testable
    ///     without any hardware, but it is not part of the public API.
    /// </remarks>
    internal interface IHidDeviceHandle : IDisposable {

        /// <summary>Gets the operating system path of the device.</summary>
        string DevicePath { get; }

        /// <summary>Gets the description of the device.</summary>
        string Description { get; }

        /// <summary>Gets the USB vendor identifier of the device.</summary>
        int VendorId { get; }

        /// <summary>Gets the USB product identifier of the device.</summary>
        int ProductId { get; }

        /// <summary>Gets whether the device is currently connected.</summary>
        bool IsConnected { get; }

        /// <summary>Writes a raw report to the device.</summary>
        /// <param name="data">The bytes to write.</param>
        /// <returns>true if the write succeeded, otherwise false.</returns>
        bool Write(byte[] data);

    }

}
