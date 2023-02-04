#region Usings declarations

using System.Diagnostics.CodeAnalysis;

#endregion

namespace LuxaforDevicesController.Protocol;

/// <summary>
///     Gathers all constants related to the protocol of Luxafor devices.
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
internal static partial class LuxaforProtocol {

    #region Nested types declarations

    internal static class BasicColor {

        #region Statics members declarations

        public static readonly byte Red     = Convert.ToByte('R');
        public static readonly byte Green   = Convert.ToByte('G');
        public static readonly byte Blue    = Convert.ToByte('B');
        public static readonly byte Cyan    = Convert.ToByte('C');
        public static readonly byte Magenta = Convert.ToByte('M');
        public static readonly byte Yellow  = Convert.ToByte('Y');
        public static readonly byte White   = Convert.ToByte('W');
        public static readonly byte Off     = Convert.ToByte('O');

        #endregion

    }

    #endregion

}