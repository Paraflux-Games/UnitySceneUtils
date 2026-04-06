using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;

public class SubsceneRunner : MonoBehaviour
{
    [SerializeField]
    private SampleSceneLoader loader;

    async void Start()
    {
        Debug.Log($"Found parameters: MyInt: {loader.sceneParams.MyInt}, MyInt2 {loader.sceneParams.MyInt2}");

        Debug.Log($"Waiting for {loader.sceneParams.LifetimeInSeconds} seconds...");
        await UniTask.WaitForSeconds(loader.sceneParams.LifetimeInSeconds);
        Debug.Log($"Sub Scene ended");

        var outcome = new SampleSceneOutcome();
        outcome.SomeInt = 5;
        outcome.SomeString = "Epic strings are epic";
        
        if (loader.sceneParams.Callback != null)
            loader.sceneParams.Callback.Invoke(outcome);
    }
}