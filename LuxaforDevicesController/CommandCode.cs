#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class CommandCode : ValueType<CommandCode> {

    #region Statics members declarations

    public static readonly CommandCode SetBasicColor           = new(LuxaforProtocol.CommandCode.SimpleColourCommand, "set basic color");
    public static readonly CommandCode SetColorWithoutFade     = new(LuxaforProtocol.CommandCode.StaticsColourWithoutFade, "set color without fade");
    public static readonly CommandCode SetColorWithFade        = new(LuxaforProtocol.CommandCode.ChangeColourWithFade, "set color with fade");
    public static readonly CommandCode ActivateStrobe          = new(LuxaforProtocol.CommandCode.Strob, "activate strobe");
    public static readonly CommandCode ActivateWave            = new(LuxaforProtocol.CommandCode.Wave, "activate wave");
    public static readonly CommandCode ActivateBuiltInPatterns = new(LuxaforProtocol.CommandCode.BuildInPatterns, "activate built-in pattern");

    #endregion

    #region Fields declarations

    private readonly byte   _value;
    private readonly string _stringRepresentation;

    #endregion

    #region Constructors declarations

    private CommandCode(byte value, string stringRepresentation) {
        _value                = value;
        _stringRepresentation = stringRepresentation;
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() {
        return _stringRepresentation;
    }

    public byte ToByte() {
        return _value;
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        return new object[] { _value };
    }

}