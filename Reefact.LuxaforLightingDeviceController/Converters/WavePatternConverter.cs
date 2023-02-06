#region Usings declarations

using System;

using Reefact.LuxaforLightingDeviceController.Protocol;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Converters {

    internal static class WavePatternConverter {

        #region Statics members declarations

        public static byte ToByte(WavePattern wavePattern) {
            switch (wavePattern) {
                case WavePattern.Wave_1: return LuxaforProtocol.WaveType._1;
                case WavePattern.Wave_2: return LuxaforProtocol.WaveType._2;
                case WavePattern.Wave_3: return LuxaforProtocol.WaveType._3;
                case WavePattern.Wave_4: return LuxaforProtocol.WaveType._4;
                case WavePattern.Wave_5: return LuxaforProtocol.WaveType._5;
                default:              throw new ArgumentOutOfRangeException(nameof(wavePattern), wavePattern, null);
            }
        }

        #endregion

    }

}