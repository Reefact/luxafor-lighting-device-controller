#region Usings declarations

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Protocol {

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

            // 'A' (0x41) selects the tab side, e.g. the LEDs n° 1, 2 and 3, and 'B' (0x42) the back
            // side, e.g. the LEDs n° 4, 5 and 6. The library had these two inverted until 2.0.0;
            // the mapping below is the one of the Luxafor documentation and of the other drivers of
            // these devices, and it has been confirmed on a device. Do not swap them back.
            public static readonly byte TabSide  = Convert.ToByte('A');
            public static readonly byte BackSide = Convert.ToByte('B');

            #endregion

        }

        #endregion

    }

}