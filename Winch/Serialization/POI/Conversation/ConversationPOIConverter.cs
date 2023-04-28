using System.Collections.Generic;
// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI.Conversation;

public class ConversationPoiConverter : PoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "conversationNodeName", new( null, null) },
        { "enabledByOtherNodeVisit", new( false, null) },
        { "enableNodeNames", new( null, null) },
        { "interactCollider", new( null, null) },
        { "isDueRefresh", new( false, null) },
        { "isOneTimeOnly", new( false, null) },
        { "otherNodeNames", new( null, null) },
        { "releaseCameraOnComplete", new( true, null) },
        { "shouldDisableOnOtherNodeVisit", new( false, null) },
        { "vCam", new( null, null) },
    };
    
    public ConversationPoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
