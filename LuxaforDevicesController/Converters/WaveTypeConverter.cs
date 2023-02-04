#region Usings declarations

using LuxaforDevicesController.Protocol;

#endregion

namespace LuxaforDevicesController.Converters {

    internal static class WaveTypeConverter {

        #region Statics members declarations

        public static byte ToByte(WaveType waveType) {
            return waveType switch {
                WaveType.Wave_1 => LuxaforProtocol.WaveType._1,
                WaveType.Wave_2 => LuxaforProtocol.WaveType._2,
                WaveType.Wave_3 => LuxaforProtocol.WaveType._3,
                WaveType.Wave_4 => LuxaforProtocol.WaveType._4,
                WaveType.Wave_5 => LuxaforProtocol.WaveType._5,
                _               => throw new ArgumentOutOfRangeException(nameof(waveType), waveType, null)
            };
        }

        #endregion

    }

}