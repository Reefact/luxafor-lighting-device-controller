namespace LuxaforDevicesController {

    internal static class BuiltInPatternConverter {

        #region Statics members declarations

        public static byte ToByte(BuiltInPattern builtInPattern) {
            return builtInPattern switch {
                BuiltInPattern.Off          => LuxaforProtocol.BuildInPatternId._0,
                BuiltInPattern.TrafficLight => LuxaforProtocol.BuildInPatternId._1,
                BuiltInPattern.Random_1     => LuxaforProtocol.BuildInPatternId._2,
                BuiltInPattern.Random_2     => LuxaforProtocol.BuildInPatternId._3,
                BuiltInPattern.Random_3     => LuxaforProtocol.BuildInPatternId._4,
                BuiltInPattern.Police       => LuxaforProtocol.BuildInPatternId._5,
                BuiltInPattern.Random_4     => LuxaforProtocol.BuildInPatternId._6,
                BuiltInPattern.Random_5     => LuxaforProtocol.BuildInPatternId._7,
                BuiltInPattern.Rainbow      => LuxaforProtocol.BuildInPatternId._8,
                _                           => throw new ArgumentOutOfRangeException(nameof(builtInPattern), builtInPattern, null)
            };
        }

        #endregion

    }

}