#region Usings declarations

using System;

using Reefact.LuxaforLightingDeviceController.Protocol;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Converters {

    internal static class BuiltInPatternConverter {

        #region Statics members declarations

        public static byte ToByte(BuiltInPattern builtInPattern) {
            switch (builtInPattern) {
                case BuiltInPattern.Off:          return LuxaforProtocol.BuildInPatternId._0;
                case BuiltInPattern.TrafficLight: return LuxaforProtocol.BuildInPatternId._1;
                case BuiltInPattern.Pattern_1:    return LuxaforProtocol.BuildInPatternId._2;
                case BuiltInPattern.Pattern_2:    return LuxaforProtocol.BuildInPatternId._3;
                case BuiltInPattern.Pattern_3:    return LuxaforProtocol.BuildInPatternId._4;
                case BuiltInPattern.Police:       return LuxaforProtocol.BuildInPatternId._5;
                case BuiltInPattern.Pattern_4:    return LuxaforProtocol.BuildInPatternId._6;
                case BuiltInPattern.Pattern_5:    return LuxaforProtocol.BuildInPatternId._7;
                case BuiltInPattern.Rainbow:      return LuxaforProtocol.BuildInPatternId._8;
                default:                          throw new ArgumentOutOfRangeException(nameof(builtInPattern), builtInPattern, null);
            }
        }

        #endregion

    }

}