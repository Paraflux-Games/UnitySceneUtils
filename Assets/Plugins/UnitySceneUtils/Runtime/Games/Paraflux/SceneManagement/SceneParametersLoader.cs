using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace Games.Paraflux.SceneManagement
{
    public interface ICallbackProvider<T>
    {
        public Action<T> Callback { get; set; }
    }

    public abstract class SceneParametersLoader<T, N> : MonoBehaviour
        where T : class, ICallbackProvider<N> where N : struct
    {
        private static T loadSceneRegister = null;
        public T sceneParams;

        [SerializeField]
        private UnityEvent OnLoadedFromOutside = new();

        public void Awake()
        {
            if (loadSceneRegister != null)
            {
                sceneParams = loadSceneRegister;
                OnLoadedFromOutside.Invoke();
            }

            loadSceneRegister = null; // the register has served its purpose, clear the state
        }

        public static async UniTask<Scene> LoadSceneWithParams(T sceneParams,
            string sceneName,
            Action<N> callback)
        {
            loadSceneRegister = sceneParams;
            sceneParams.Callback = callback;

            var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            await UniTask.WaitUntil(() => loadOperation.isDone && loadOperation.progress > 0.9f);

            Debug.Log("Finished loading!");
            return SceneManager.GetSceneByName(sceneName);
        }

        public void OnSceneEnded(N result)
        {
            if (sceneParams.Callback != null)
                sceneParams.Callback(result);

            sceneParams.Callback = null; // Protect against double calling;
        }
    }
}