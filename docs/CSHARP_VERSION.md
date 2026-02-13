# C# Language Version Requirements

## Current Status: C# 7.3

This project targets .NET Framework 4.7.2 and uses C# 7.3 syntax exclusively.

### C# 7.3 Features Used

The codebase uses the following C# 7.3 compatible features:

1. **String interpolation** (`$"text {variable}"`) - C# 6.0+
2. **Ternary operators** (`condition ? true : false`) - C# 1.0+
3. **Lambda expressions** - C# 3.0+
4. **LINQ** (`.Count()`, `.Find()`, etc.) - C# 3.0+
5. **Auto-implemented properties** - C# 3.0+
6. **Default parameter values** - C# 4.0+
7. **Expression-bodied members** (if any) - C# 6.0+
8. **Null-conditional operators** (`?.`, `??`) - C# 6.0+

### Features Converted from C# 8.0+

The following features were originally implemented using newer C# syntax but have been converted to C# 7.3:

1. **Switch expressions with relational patterns** (C# 9.0)
   - **Location**: `Program.cs` lines 142-147 (originally)
   - **Converted to**: Traditional if-else statements
   - **Commit**: Converted to C# 7.3 syntax
   
   ```csharp
   // Original C# 9.0 code:
   return address switch
   {
       < 0 => ValidationResult.Error("..."),
       > 30 => ValidationResult.Error("..."),
       _ => ValidationResult.Success()
   };
   
   // Converted to C# 7.3:
   if (address < 0)
   {
       return ValidationResult.Error("...");
   }
   else if (address > 30)
   {
       return ValidationResult.Error("...");
   }
   else
   {
       return ValidationResult.Success();
   }
   ```

### Project Configuration

The project file does NOT specify a `<LangVersion>` property, which means it defaults to the language version supported by the compiler for the target framework (.NET Framework 4.7.2 = C# 7.3).

### Future Considerations

If future development requires C# 8.0+ features that cannot be easily converted to C# 7.3 syntax, create a GitHub issue with the following template:

**Issue Title**: "Require C# [version] for [feature description]"

**Issue Body**:
```
## Feature Requirement

**Feature**: [Name of C# feature needed]
**C# Version**: [Required version]
**Reason**: [Why this feature is needed]

## Current Workaround

[Describe the C# 7.3 workaround being used, if any]

## Benefits of Using Newer Syntax

- [Benefit 1]
- [Benefit 2]

## Compatibility Impact

- Requires Visual Studio [version]+
- May require compiler updates
- Runtime compatibility: [analysis]

## Decision

- [ ] Use C# 7.3 workaround
- [ ] Upgrade to C# [version] with justification
```

### References

- [C# Language Versioning](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version)
- [C# 7.3 Features](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-3)
- [C# 8.0 Features](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8)
- [C# 9.0 Features](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9)
