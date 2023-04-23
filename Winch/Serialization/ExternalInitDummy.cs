// ReSharper disable once CheckNamespace

namespace System.Runtime.CompilerServices;

// This is necessary because we are technically compiling .NET 5 code against older .NET framework versions.
// Refer to https://developercommunity.visualstudio.com/t/error-cs0518-predefined-type-systemruntimecompiler/1244809 for more information.
internal static class IsExternalInit {}