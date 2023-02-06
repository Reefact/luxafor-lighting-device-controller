#region Usings declarations

using System.Diagnostics.CodeAnalysis;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Protocol {

    [SuppressMessage("ReSharper", "IdentifierTypo")]
    internal static partial class LuxaforProtocol {

        #region Nested types declarations

        internal static class CommandCode {

            public const byte StaticsColourWithoutFade = 1;
            public const byte ChangeColourWithFade     = 2;
            public const byte Strob                    = 3;
            public const byte Wave                     = 4;
            public const byte BuildInPatterns          = 6;

        }

        #endregion

    }

}