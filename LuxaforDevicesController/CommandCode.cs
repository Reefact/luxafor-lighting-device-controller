#region Usings declarations

using System.Diagnostics;

#endregion

namespace LuxaforDevicesController;

[DebuggerDisplay("{ToString()}")]
public sealed class CommandCode : ValueType<CommandCode> {

    #region Statics members declarations

    public static readonly CommandCode BasicColor       = new(0, "set basic color");
    public static readonly CommandCode ColorWithoutFade = new(1, "set color without fade");
    public static readonly CommandCode ColorWithFade    = new(2, "set color with fade");
    public static readonly CommandCode Strobe           = new(3, "activate strobe");
    public static readonly CommandCode Wave             = new(4, "activate wave");
    public static readonly CommandCode BuiltInPatterns  = new(6, "activate built-in pattern");

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