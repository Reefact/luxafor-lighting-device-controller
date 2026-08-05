#region Usings declarations

using System.Reflection;
using System.Text;

using NFluent;

using Xunit;

#endregion

namespace Reefact.LuxaforLightingDeviceController.UnitTests;

/// <summary>
///     The public API of the library is its contract: any change to the approved list below is a change for the
///     consumers, and a removal or a signature change is a breaking change. This test makes them visible in the diff.
///     When a change is intended, update <c>PublicApi.approved.txt</c> in the same commit.
/// </summary>
public class PublicApi_should {

    #region Statics members declarations

    private static readonly string[] LineSeparators = { "\r\n", "\n" };

    private static string DescribePublicApi() {
        Assembly      assembly = typeof(Luxafor).Assembly;
        StringBuilder builder  = new();

        foreach (Type type in assembly.GetExportedTypes().OrderBy(Describe, StringComparer.Ordinal)) {
            builder.AppendLine(Describe(type));

            IEnumerable<string> members = type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                              .Where(IsVisibleOutsideTheAssembly)
                                              .Select(member => $"    {member}")
                                              .OrderBy(member => member, StringComparer.Ordinal);
            foreach (string member in members) {
                builder.AppendLine(member);
            }
        }

        return builder.ToString();
    }

    private static string Describe(Type type) {
        string kind = type.IsEnum      ? "enum" :
                      type.IsInterface ? "interface" :
                      type.IsValueType ? "struct" : "class";

        List<string> parents = new();
        if (type.BaseType is not null && type.BaseType != typeof(object) && type.BaseType != typeof(ValueType) && type.BaseType != typeof(Enum)) {
            parents.Add(FormatTypeName(type.BaseType));
        }
        // Only the contracts the type declares itself: the ones inherited from the base type (ISerializable on
        // Exception, IConvertible on Enum, ...) differ from one target framework to another.
        if (!type.IsEnum) {
            Type[] inheritedContracts = type.BaseType?.GetInterfaces() ?? Array.Empty<Type>();
            parents.AddRange(type.GetInterfaces()
                                 .Where(contract => !inheritedContracts.Contains(contract))
                                 .Select(FormatTypeName)
                                 .OrderBy(name => name, StringComparer.Ordinal));
        }
        string inheritance = parents.Count == 0 ? string.Empty : $" : {string.Join(", ", parents)}";

        return $"{kind} {type.FullName}{inheritance}";
    }

    /// <summary>Formats a type name without its assembly qualification, which carries the (moving) version.</summary>
    private static string FormatTypeName(Type type) {
        if (!type.IsGenericType) { return type.FullName ?? type.Name; }

        string name      = type.GetGenericTypeDefinition().FullName ?? type.Name;
        string arguments = string.Join(", ", type.GetGenericArguments().Select(FormatTypeName));

        return $"{name.Split('`')[0]}<{arguments}>";
    }

    private static bool IsVisibleOutsideTheAssembly(MemberInfo member) {
        switch (member) {
            case MethodBase method:      return method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly;
            case FieldInfo field:        return field.IsPublic || field.IsFamily || field.IsFamilyOrAssembly;
            case PropertyInfo property:  return property.GetMethod is not null && IsVisibleOutsideTheAssembly(property.GetMethod);
            case EventInfo eventInfo:    return eventInfo.AddMethod is not null && IsVisibleOutsideTheAssembly(eventInfo.AddMethod);
            case Type nestedType:        return nestedType.IsNestedPublic || nestedType.IsNestedFamily;
            default:                     return false;
        }
    }

    /// <summary>
    ///     Normalizes the line endings, so that the comparison holds whatever the git checkout settings and the
    ///     target framework are. <c>string.Replace(string, string, StringComparison)</c> would not do: it does not
    ///     exist on .NET Framework.
    /// </summary>
    private static string NormalizeLineEndings(string text) {
        return string.Join("\n", text.Split(LineSeparators, StringSplitOptions.None));
    }

    #endregion

    [Fact]
    public void not_change_without_updating_the_approved_api() {
        // Setup
        string approvedFilePath = Path.Combine(AppContext.BaseDirectory, "PublicApi.approved.txt");
        string approvedApi      = NormalizeLineEndings(File.ReadAllText(approvedFilePath));
        // Exercise
        string actualApi = NormalizeLineEndings(DescribePublicApi());
        // Verify
        Check.WithCustomMessage($"The public API changed. Review the change (is it a breaking change?) then update '{approvedFilePath}'.")
             .That(actualApi).IsEqualTo(approvedApi);
    }

}
