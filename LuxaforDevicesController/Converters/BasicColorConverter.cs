#region Usings declarations

using LuxaforDevicesController.Protocol;

#endregion

namespace LuxaforDevicesController.Converters {

    internal static class BasicColorConverter {

        #region Statics members declarations

        public static byte ToByte(BasicColor simpleColor) {
            return simpleColor switch {
                BasicColor.Red     => LuxaforProtocol.BasicColor.Red,
                BasicColor.Green   => LuxaforProtocol.BasicColor.Green,
                BasicColor.Blue    => LuxaforProtocol.BasicColor.Blue,
                BasicColor.Cyan    => LuxaforProtocol.BasicColor.Cyan,
                BasicColor.Magenta => LuxaforProtocol.BasicColor.Magenta,
                BasicColor.Yellow  => LuxaforProtocol.BasicColor.Yellow,
                BasicColor.White   => LuxaforProtocol.BasicColor.White,
                BasicColor.Off     => LuxaforProtocol.BasicColor.Off,
                _                  => throw new ArgumentOutOfRangeException(nameof(simpleColor), simpleColor, null)
            };
        }

        #endregion

    }

}