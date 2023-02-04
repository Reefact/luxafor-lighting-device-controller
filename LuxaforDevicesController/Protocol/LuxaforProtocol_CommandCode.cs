#region Usings declarations

using System.Diagnostics.CodeAnalysis;

#endregion

namespace LuxaforDevicesController.Protocol;

[SuppressMessage("ReSharper", "IdentifierTypo")]
internal static partial class LuxaforProtocol {

    #region Nested types declarations

    internal static class CommandCode {

        public const byte SimpleColourCommand      = 0;
        public const byte StaticsColourWithoutFade = 1;
        public const byte ChangeColourWithFade     = 2;
        public const byte Strob                    = 3;
        public const byte Wave                     = 4;
        public const byte BuildInPatterns          = 6;

        // Not use by the library.
        // ReSharper disable once UnusedMember.Global
        public const byte Productivity = 10;

    }

    #endregion

}