using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using Hash128 = Unity.Entities.Hash128;

public class SubSceneLoader : ComponentSystem
{
    private SceneSystem sceneSys;

    protected override void OnCreate()
    {
        sceneSys = World.GetOrCreateSystem<SceneSystem>();
    }
    
    protected override void OnUpdate()
    {
        SubSceneMap map = GameObject.FindObjectOfType<SubSceneMap>();
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            ToggleScene(map.Spheres);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            ToggleScene(map.Cubes);
        }
    }

    void ToggleScene(in SubScene scene)
    {
        if (IsSceneLoaded(scene.SceneGUID))
        {
            sceneSys.UnloadScene(scene.SceneGUID);
        }
        else
        {
            sceneSys.LoadSceneAsync(scene.SceneGUID);
        }
    }
    
    bool IsSceneLoaded(in Hash128 sceneGUID)
    {
        var sceneEntity = sceneSys.GetSceneEntity(sceneGUID);
        return sceneSys.IsSceneLoaded(sceneEntity);
    }
}
