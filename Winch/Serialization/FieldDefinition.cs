using System;
namespace Winch.Serialization;

// This is the sole reason I have included the ExternalInitDummy file in this project.
public readonly record struct FieldDefinition(object DefaultValue, Func<object, object> Parser);