#region Usings declarations

using System;
using System.Collections.Generic;
using System.Linq;

using HidLibrary;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Hid {

    /// <summary>
    ///     The <see cref="IHidDeviceRegistry">registry</see> backed by
    ///     <a href="https://github.com/mikeobrien/HidLibrary">HidLibrary</a>, e.g. by the real USB ports of the machine.
    /// </summary>
    internal sealed class HidLibraryDeviceRegistry : IHidDeviceRegistry {

        #region Statics members declarations

        public static readonly HidLibraryDeviceRegistry Instance = new HidLibraryDeviceRegistry();

        #endregion

        #region Fields declarations

        private readonly IHidEnumerator _hidEnumerator;

        #endregion

        #region Constructors declarations

        public HidLibraryDeviceRegistry() : this(new HidEnumerator()) { }

        public HidLibraryDeviceRegistry(IHidEnumerator hidEnumerator) {
            if (hidEnumerator is null) { throw new ArgumentNullException(nameof(hidEnumerator)); }

            _hidEnumerator = hidEnumerator;
        }

        #endregion

        /// <inheritdoc />
        public IEnumerable<IHidDeviceHandle> Enumerate(int vendorId, int productId) {
            return _hidEnumerator.Enumerate(vendorId, productId)
                                 .Select(device => (IHidDeviceHandle)new HidLibraryDeviceHandle(device));
        }

        /// <inheritdoc />
        public IHidDeviceHandle? GetDevice(string devicePath) {
            IHidDevice device = _hidEnumerator.GetDevice(devicePath);

            return device is null ? null : new HidLibraryDeviceHandle(device);
        }

    }

}
