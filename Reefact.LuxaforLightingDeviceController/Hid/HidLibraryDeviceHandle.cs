#region Usings declarations

using System;

using HidLibrary;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Hid {

    /// <summary>
    ///     Adapts a <a href="https://github.com/mikeobrien/HidLibrary">HidLibrary</a> device to
    ///     <see cref="IHidDeviceHandle" />.
    /// </summary>
    internal sealed class HidLibraryDeviceHandle : IHidDeviceHandle {

        #region Fields declarations

        private readonly IHidDevice _device;

        #endregion

        #region Constructors declarations

        public HidLibraryDeviceHandle(IHidDevice device) {
            if (device is null) { throw new ArgumentNullException(nameof(device)); }

            _device = device;
        }

        #endregion

        /// <inheritdoc />
        public string DevicePath => _device.DevicePath;

        /// <inheritdoc />
        public string Description => _device.Description;

        /// <inheritdoc />
        public int VendorId => _device.Attributes.VendorId;

        /// <inheritdoc />
        public int ProductId => _device.Attributes.ProductId;

        /// <inheritdoc />
        public bool IsConnected => _device.IsConnected;

        /// <inheritdoc />
        public bool Write(byte[] data) {
            return _device.Write(data);
        }

        /// <inheritdoc />
        public void Dispose() {
            _device.Dispose();
        }

    }

}
