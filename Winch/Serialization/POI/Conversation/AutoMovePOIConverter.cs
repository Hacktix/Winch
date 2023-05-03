using System.Collections.Generic;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI.Conversation;

public class AutoMovePoiConverter : ConversationPoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "autoMoveDestination", new( null, null) },
        { "includeRotation", new( false, null) },
    };
    
    public AutoMovePoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
