#region Usings declarations

using System.ComponentModel;

#endregion

namespace LuxaforDevicesController;

/// <summary>
///     Provides a set of predefined patterns of built-in lighting effects.
/// </summary>
public enum BuiltInPattern {

    /// <summary>
    ///     Turns all device's LEDs off.
    /// </summary>
    [Description("#off")] Off,
    /// <summary>
    ///     Plays a predefined lighting pattern.
    /// </summary>
    [Description("pattern 1")] Pattern_1,
    /// <summary>
    ///     Plays a predefined lighting pattern.
    /// </summary>
    [Description("pattern 2")] Pattern_2,
    /// <summary>
    ///     Plays a predefined lighting pattern.
    /// </summary>
    [Description("pattern 3")] Pattern_3,
    /// <summary>
    ///     Plays a predefined lighting pattern.
    /// </summary>
    [Description("pattern 4")] Pattern_4,
    /// <summary>
    ///     Plays a predefined lighting pattern.
    /// </summary>
    [Description("pattern 5")] Pattern_5,
    /// <summary>
    ///     Plays a predefined lighting pattern reminiscent of the colors of the rainbow.
    /// </summary>
    [Description("rainbow")] Rainbow,
    /// <summary>
    ///     Plays a <see cref="BasicColor.Red" />, <see cref="BasicColor.Yellow" />, <see cref="BasicColor.Green" /> sequence
    ///     in the style of traffic lights.
    /// </summary>
    [Description("traffic light")] TrafficLight,
    /// <summary>
    ///     Plays a <see cref="BasicColor.Red" /> and <see cref="BasicColor.Blue" /> strobe effect in the style of a police
    ///     flashing light.
    /// </summary>
    [Description("police")] Police

}