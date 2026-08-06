#region Usings declarations

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Reefact.LuxaforLightingDeviceController.Protocol;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    /// <summary>
    ///     Represents the LEDs targeted by a <see cref="LightingCommand">lighting command</see>. Targeted LEDs are turned
    ///     on/off or animated simultaneously by the device.
    /// </summary>
    /// <remarks>
    ///     Targeting combinations are limited by the <see cref="ILuxaforDevice">device</see>. Consider multiple sequential
    ///     commands for other combinations, but in this case the on/off or animation will also be sequential, which can cause
    ///     a visual ripple effect.
    /// </remarks>
    [DebuggerDisplay("{ToString()}")]
    public sealed class TargetedLeds : IEquatable<TargetedLeds>, IEnumerable<LedIndex> {

        #region Statics members declarations

        // Declared first: the targeted LEDs below register themselves in this dictionary as they are created.
        private static readonly Dictionary<byte, TargetedLeds> LedsByLuxCode = new Dictionary<byte, TargetedLeds>();

        /// <summary>The LED n° 1.</summary>
        public static readonly TargetedLeds Led_1 = Create(LuxaforProtocol.Lux.Led_1, "LED n° 1", LedIndex._1);
        /// <summary>The LED n° 2.</summary>
        public static readonly TargetedLeds Led_2 = Create(LuxaforProtocol.Lux.Led_2, "LED n° 2", LedIndex._2);
        /// <summary>The LED n° 3.</summary>
        public static readonly TargetedLeds Led_3 = Create(LuxaforProtocol.Lux.Led_3, "LED n° 3", LedIndex._3);
        /// <summary>The LED n° 4.</summary>
        public static readonly TargetedLeds Led_4 = Create(LuxaforProtocol.Lux.Led_4, "LED n° 4", LedIndex._4);
        /// <summary>The LED n° 5.</summary>
        public static readonly TargetedLeds Led_5 = Create(LuxaforProtocol.Lux.Led_5, "LED n° 5", LedIndex._5);
        /// <summary>The LED n° 6.</summary>
        public static readonly TargetedLeds Led_6 = Create(LuxaforProtocol.Lux.Led_6, "LED n° 6", LedIndex._6);
        /// <summary>The tab side LEDs (e.g. LEDs n° 1, 2 and 3)</summary>
        public static readonly TargetedLeds TabSide = Create(LuxaforProtocol.Lux.TabSide, "tab side LEDs", LedIndex._1, LedIndex._2, LedIndex._3);
        /// <summary>The back side LEDs (e.g. LEDs n° 4, 5 and 6)</summary>
        public static readonly TargetedLeds BackSide = Create(LuxaforProtocol.Lux.BackSide, "back side LEDs", LedIndex._4, LedIndex._5, LedIndex._6);
        /// <summary>All LEDs  (e.g. LEDs n° 1, 2, 3, 4, 5 and 6).</summary>
        public static readonly TargetedLeds All = Create(LuxaforProtocol.Lux.All, "all LEDs", LedIndex.GetAll());

        /// <summary>
        ///     Gets the <see cref="TargetedLeds">LEDs targeted</see> by a
        ///     <see cref="LightingCommand">lighting command</see> from a lux code.
        /// </summary>
        /// <param name="luxCode">The <see cref="byte" /> value</param>
        /// <returns>A <see cref="TargetedLeds">targeted LEDs</see>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Argument <paramref name="luxCode" /> is not a known lux code.</exception>
        public static TargetedLeds FromLuxCode(byte luxCode) {
            if (!LedsByLuxCode.TryGetValue(luxCode, out TargetedLeds leds)) { throw new ArgumentOutOfRangeException(nameof(luxCode), luxCode, $"LED {luxCode} does not exist."); }

            return leds;
        }

        private static TargetedLeds Create(byte luxCode, string stringRepresentation, params LedIndex[] targetedLeds) {
            TargetedLeds leds = new TargetedLeds(luxCode, stringRepresentation, targetedLeds);
            LedsByLuxCode.Add(luxCode, leds);

            return leds;
        }

        #endregion

        /// <summary>Define a <see cref="LedIndex">LED index</see> as a <see cref="TargetedLeds">targeted LED</see>.</summary>
        /// <param name="index">The <see cref="LedIndex">LED index</see>.</param>
        /// <returns>The <see cref="TargetedLeds">targeted LEDs</see> matching the <paramref name="index" />.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Argument <paramref name="index" /> is not a known LED index.</exception>
        public static implicit operator TargetedLeds(LedIndex index) {
            return FromLedIndex(index);
        }

        /// <summary>Define a <see cref="LedIndex">LED index</see> as a <see cref="TargetedLeds">targeted LED</see>.</summary>
        /// <param name="index">The <see cref="LedIndex">LED index</see>.</param>
        /// <returns>The <see cref="TargetedLeds">targeted LEDs</see> matching the <paramref name="index" />.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Argument <paramref name="index" /> is not a known LED index.</exception>
        public static TargetedLeds FromLedIndex(LedIndex index) {
            if (index == LedIndex._1) { return Led_1; }
            if (index == LedIndex._2) { return Led_2; }
            if (index == LedIndex._3) { return Led_3; }
            if (index == LedIndex._4) { return Led_4; }
            if (index == LedIndex._5) { return Led_5; }
            if (index == LedIndex._6) { return Led_6; }

            throw new ArgumentOutOfRangeException(nameof(index), index, "LED index is unknown.");
        }

        #region Fields declarations

        private readonly byte                          _luxCode;
        private readonly string                        _stringRepresentation;
        private readonly LedIndex[]                    _targetedLeds;

        #endregion

        #region Constructors declarations

        private TargetedLeds(byte luxCode, string stringRepresentation, params LedIndex[] targetedLeds) {
            _luxCode              = luxCode;
            _stringRepresentation = stringRepresentation;
            _targetedLeds         = targetedLeds;
        }

        #endregion

        /// <summary>Gets if the current instance targets a single LED.</summary>
        public bool TargetsSingleLed => _targetedLeds.Length == 1;
        /// <summary>Gets if the current instance targets several LEDs.</summary>
        public bool TargetsSeveralLeds => _targetedLeds.Length > 1;

        /// <inheritdoc />
        public IEnumerator<LedIndex> GetEnumerator() {
            return ((IEnumerable<LedIndex>)_targetedLeds).GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString() {
            return _stringRepresentation;
        }

        /// <inheritdoc />
        public bool Equals(TargetedLeds? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return _luxCode == other._luxCode;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return Equals(obj as TargetedLeds);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _luxCode.GetHashCode();
        }

        /// <summary>Indicates whether two <see cref="TargetedLeds">targeted LEDs</see> are equal.</summary>
        /// <param name="left">The first <see cref="TargetedLeds">targeted LEDs</see> to compare.</param>
        /// <param name="right">The second <see cref="TargetedLeds">targeted LEDs</see> to compare.</param>
        /// <returns>true if both values are equal, otherwise false.</returns>
        public static bool operator ==(TargetedLeds? left, TargetedLeds? right) {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>Indicates whether two <see cref="TargetedLeds">targeted LEDs</see> are different.</summary>
        /// <param name="left">The first <see cref="TargetedLeds">targeted LEDs</see> to compare.</param>
        /// <param name="right">The second <see cref="TargetedLeds">targeted LEDs</see> to compare.</param>
        /// <returns>true if both values are different, otherwise false.</returns>
        public static bool operator !=(TargetedLeds? left, TargetedLeds? right) {
            return !(left == right);
        }

        internal byte ToLuxCode() {
            return _luxCode;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}
