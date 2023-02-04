#region Usings declarations

using LuxaforDevicesController.Protocol;

#endregion

namespace LuxaforDevicesController.Converters {

    internal static class BuiltInPatternConverter {

        #region Statics members declarations

        public static byte ToByte(BuiltInPattern builtInPattern) {
            return builtInPattern switch {
                BuiltInPattern.Off          => LuxaforProtocol.BuildInPatternId._0,
                BuiltInPattern.TrafficLight => LuxaforProtocol.BuildInPatternId._1,
                BuiltInPattern.Pattern_1    => LuxaforProtocol.BuildInPatternId._2,
                BuiltInPattern.Pattern_2    => LuxaforProtocol.BuildInPatternId._3,
                BuiltInPattern.Pattern_3    => LuxaforProtocol.BuildInPatternId._4,
                BuiltInPattern.Police       => LuxaforProtocol.BuildInPatternId._5,
                BuiltInPattern.Pattern_4    => LuxaforProtocol.BuildInPatternId._6,
                BuiltInPattern.Pattern_5    => LuxaforProtocol.BuildInPatternId._7,
                BuiltInPattern.Rainbow      => LuxaforProtocol.BuildInPatternId._8,
                _                           => throw new ArgumentOutOfRangeException(nameof(builtInPattern), builtInPattern, null)
            };
        }

        #endregion

    }

}