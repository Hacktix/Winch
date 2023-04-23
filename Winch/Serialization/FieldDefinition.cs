using System;
namespace Winch.Serialization;

public readonly record struct FieldDefinition(object DefaultValue, Func<object, object> Parser);