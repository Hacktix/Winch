namespace Winch.Core.API.Events.Scene;

public class SceneEvents
{
    public InstanceOnlyHook<GameSceneInitializer> GameSceneInitializerStart = new();
}