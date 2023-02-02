#region Usings declarations

using System.Diagnostics.CodeAnalysis;

#endregion

namespace LuxaforDevicesController;

[SuppressMessage("ReSharper", "IdentifierTypo")]
internal static partial class LuxaforProtocol {

    #region Nested types declarations

    internal static class Color {

        #region Statics members declarations

        public static readonly byte R = Convert.ToByte('R');
        public static readonly byte G = Convert.ToByte('G');
        public static readonly byte B = Convert.ToByte('B');
        public static readonly byte C = Convert.ToByte('C');
        public static readonly byte M = Convert.ToByte('M');
        public static readonly byte Y = Convert.ToByte('Y');
        public static readonly byte W = Convert.ToByte('W');
        public static readonly byte O = Convert.ToByte('O');

        #endregion

    }

    #endregion

}