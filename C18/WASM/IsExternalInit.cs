// Fix the error CS0518: Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported
// Allows to use C# 9 init-only properties/records
namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}
