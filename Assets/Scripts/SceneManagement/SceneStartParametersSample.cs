using System;
using Games.Paraflux.SceneManagement;

namespace SceneManagement
{
    [Serializable]
    public class SceneStartParametersSample : ICallbackProvider<SampleSceneOutcome>
    {
        public int MyInt;
        public int MyInt2;
        public float LifetimeInSeconds { get; set; }
        public Action<SampleSceneOutcome> Callback { get; set; }
    }
}