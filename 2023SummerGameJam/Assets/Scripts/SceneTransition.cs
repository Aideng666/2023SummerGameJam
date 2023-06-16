using UnityEngine;

namespace EasyTransition
{
    public class SceneTransition : MonoBehaviour
    {
        public TransitionSettings transition;
        public float startDelay;

        public void LoadScene(string _sceneName)
        {
            TransitionManager.Instance().Transition(_sceneName, transition, startDelay);
        }
    }
}
