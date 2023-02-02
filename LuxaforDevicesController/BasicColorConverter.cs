namespace LuxaforDevicesController {

    internal static class BasicColorConverter {

        #region Statics members declarations

        public static byte ToByte(BasicColor simpleColor) {
            return simpleColor switch {
                BasicColor.Red     => LuxaforProtocol.Color.R,
                BasicColor.Green   => LuxaforProtocol.Color.G,
                BasicColor.Blue    => LuxaforProtocol.Color.B,
                BasicColor.Cyan    => LuxaforProtocol.Color.C,
                BasicColor.Magenta => LuxaforProtocol.Color.M,
                BasicColor.Yellow  => LuxaforProtocol.Color.Y,
                BasicColor.White   => LuxaforProtocol.Color.W,
                BasicColor.Off     => LuxaforProtocol.Color.O,
                _                  => throw new ArgumentOutOfRangeException(nameof(simpleColor), simpleColor, null)
            };
        }

        #endregion

    }

}