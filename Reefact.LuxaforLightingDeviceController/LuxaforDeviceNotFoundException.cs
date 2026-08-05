#region Usings declarations

using System;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     The exception thrown when no compatible Luxafor <see cref="LuxaforDevice">device</see> can be opened at the
    ///     requested device path.
    /// </summary>
    public class LuxaforDeviceNotFoundException : Exception {

        #region Constructors declarations

        /// <summary>Instantiates a new <see cref="LuxaforDeviceNotFoundException" />.</summary>
        public LuxaforDeviceNotFoundException() { }

        /// <summary>Instantiates a new <see cref="LuxaforDeviceNotFoundException" />.</summary>
        /// <param name="message">The message that describes the error.</param>
        public LuxaforDeviceNotFoundException(string message) : base(message) { }

        /// <summary>Instantiates a new <see cref="LuxaforDeviceNotFoundException" />.</summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public LuxaforDeviceNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        internal LuxaforDeviceNotFoundException(string message, string devicePath) : base(message) {
            DevicePath = devicePath;
        }

        #endregion

        /// <summary>Gets the device path that could not be opened, when known.</summary>
        public string? DevicePath { get; }

    }

}
