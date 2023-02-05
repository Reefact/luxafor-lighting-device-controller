#region Usings declarations

using System.ComponentModel;

#endregion

namespace Reefact.LuxaforDevicesController;

/// <summary>
///     Provides a set of predefined basic colors.
/// </summary>
public enum BasicColor {

    /// <summary>
    ///     <see cref="Off" /> (could be considered as the basic <i>black</i> color) turn
    ///     <see cref="TargetedLeds">targeted LED(s)</see> off.
    /// </summary>
    [Description("#off")] Off,
    /// <summary>Red</summary>
    [Description("red")] Red,
    /// <summary>Green</summary>
    [Description("green")] Green,
    /// <summary>Blue</summary>
    [Description("blue")] Blue,
    /// <summary>Cyan</summary>
    [Description("cyan")] Cyan,
    /// <summary>Magenta</summary>
    [Description("magenta")] Magenta,
    /// <summary>Yellow</summary>
    [Description("yellow")] Yellow,
    /// <summary>White</summary>
    [Description("white")] White

}