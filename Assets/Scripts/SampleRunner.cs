using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SampleRunner : MonoBehaviour
{
    [SerializeField]
    private Button LoadSubSceneBtn;

    [SerializeField]
    private string SubsceneName = "SubSceneExample";

    void OnEnable()
    {
        LoadSubSceneBtn.onClick.AddListener(OnLoadSubscene);
    }

    void OnDisable()
    {
        LoadSubSceneBtn.onClick.RemoveAllListeners();
    }

    private Scene loadedSubScene;

    async void OnLoadSubscene()
    {
        LoadSubSceneBtn.gameObject.SetActive(false);

        var startParams = new SceneStartParametersSample();
        startParams.MyInt = 13;
        startParams.MyInt2 = 37;
        startParams.LifetimeInSeconds = 5f;

        loadedSubScene = await SampleSceneLoader.LoadSceneWithParams(startParams, SubsceneName, OnSceneEnded);
        gameObject.SetActive(false);

        SceneManager.SetActiveScene(loadedSubScene);
    }

    async void OnSceneEnded(SampleSceneOutcome outcome)
    {
        var operation = SceneManager.UnloadSceneAsync(loadedSubScene.name);
        await UniTask.WaitUntil(() => operation.isDone);

        LoadSubSceneBtn.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }
}