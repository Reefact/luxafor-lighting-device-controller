#region Usings declarations

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Reefact.LuxaforLightingDeviceController.Hid;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Locates the Luxafor <see cref="ILuxaforDevice">devices</see> exposed by a
    ///     <see cref="IHidDeviceRegistry">HID registry</see>.
    /// </summary>
    /// <remarks>
    ///     <see cref="Luxafor" /> is the (hardware bound) public facade of this locator; the locator itself takes the
    ///     registry as an argument so that the lookup rules can be tested without any device plugged in.
    /// </remarks>
    internal static class LuxaforDeviceLocator {

        #region Statics members declarations

        /// <summary>The USB vendor identifier of the supported Luxafor devices.</summary>
        internal const int VendorId = 1240;
        /// <summary>The USB product identifier of the supported Luxafor devices.</summary>
        internal const int ProductId = 62322;

        public static IEnumerable<ILuxaforDevice> GetDevices(IHidDeviceRegistry registry) {
            if (registry is null) { throw new ArgumentNullException(nameof(registry)); }

            return registry.Enumerate(VendorId, ProductId)
                           .Select(handle => (ILuxaforDevice)new HidLuxaforDevice(handle));
        }

        public static ILuxaforDevice GetDevice(IHidDeviceRegistry registry, string devicePath) {
            if (registry is null) { throw new ArgumentNullException(nameof(registry)); }
            if (devicePath is null) { throw new ArgumentNullException(nameof(devicePath)); }
            if (devicePath.Trim().Length == 0) { throw new ArgumentException("Device path cannot be empty.", nameof(devicePath)); }

            IHidDeviceHandle? handle = registry.GetDevice(devicePath);
            if (handle is null) { throw new LuxaforDeviceNotFoundException($"No HID device found at path '{devicePath}'.", devicePath); }

            try {
                EnsureIsAConnectedLuxaforDevice(handle, devicePath);
            } catch {
                handle.Dispose();

                throw;
            }

            return new HidLuxaforDevice(handle);
        }

        private static void EnsureIsAConnectedLuxaforDevice(IHidDeviceHandle handle, string devicePath) {
            if (handle.VendorId != VendorId || handle.ProductId != ProductId) {
                string message = string.Format(CultureInfo.InvariantCulture,
                                               "The HID device found at path '{0}' is not a supported Luxafor device (expected vendor id {1} and product id {2}, found vendor id {3} and product id {4}).",
                                               devicePath, VendorId, ProductId, handle.VendorId, handle.ProductId);

                throw new LuxaforDeviceNotFoundException(message, devicePath);
            }
            if (!handle.IsConnected) {
                throw new LuxaforDeviceNotFoundException($"The Luxafor device found at path '{devicePath}' is not connected anymore.", devicePath);
            }
        }

        #endregion

    }

}
