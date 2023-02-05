#region Usings declarations

using System.Diagnostics.CodeAnalysis;

#endregion

namespace Reefact.LuxaforDevicesController.Protocol;

[SuppressMessage("ReSharper", "IdentifierTypo")]
internal static partial class LuxaforProtocol {

    #region Nested types declarations

    internal static class Lux {

        public const byte Led_1 = 1;
        public const byte Led_2 = 2;
        public const byte Led_3 = 3;
        public const byte Led_4 = 4;
        public const byte Led_5 = 5;
        public const byte Led_6 = 6;
        public const byte All   = 255;

        #region Statics members declarations

        public static readonly byte BackSide = Convert.ToByte('A');
        public static readonly byte TabSide  = Convert.ToByte('B');

        #endregion

    }

    #endregion

}