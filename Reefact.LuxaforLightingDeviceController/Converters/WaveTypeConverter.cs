#region Usings declarations

using System;

using Reefact.LuxaforLightingDeviceController.Protocol;

#endregion

namespace Reefact.LuxaforLightingDeviceController.Converters {

    internal static class WaveTypeConverter {

        #region Statics members declarations

        public static byte ToByte(WaveType waveType) {
            switch (waveType) {
                case WaveType.Wave_1: return LuxaforProtocol.WaveType._1;
                case WaveType.Wave_2: return LuxaforProtocol.WaveType._2;
                case WaveType.Wave_3: return LuxaforProtocol.WaveType._3;
                case WaveType.Wave_4: return LuxaforProtocol.WaveType._4;
                case WaveType.Wave_5: return LuxaforProtocol.WaveType._5;
                default:              throw new ArgumentOutOfRangeException(nameof(waveType), waveType, null);
            }
        }

        #endregion

    }

}