using System.Collections.Generic;

namespace Winch.Serialization.POI.Conversation;

// ReSharper disable HeapView.BoxingAllocation

public class ExplosivePoiConverter : ConversationPoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "animator", new( null, null) },
        { "ExplodeVibration", new( null, null) },
        { "id", new( null, null) },
        { "impulseSource", new( null, null) },
    };
    
    public ExplosivePoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
