using UnityEngine;
using UnityEngine.SceneManagement;

namespace PieceCombat
{
    public class SceneNavigation : MonoBehaviour
    {
        [SerializeField] bool navToSceneOnSpaceBar = false;
        [SerializeField] bool transitionBeforeScene = false;
        [SerializeField] RectTransform transition;
        [SerializeField] Vector2 transitionTarget;
        Vector2 transitionStart;
        [SerializeField] float transitionDuration;
        [SerializeField] string spaceBarSceneName;

        bool transitioning = false;
        float time;

        void Update() {
            if (!navToSceneOnSpaceBar) {
                return;
            }

            if (!Input.GetKeyDown(KeyCode.Space) && !transitioning) {
                return;
            } else {
				if (!transitionBeforeScene) {
					SceneManager.LoadScene(spaceBarSceneName);
                    return;
				}

				if (!transitioning) {
                    transitionStart = transition.anchoredPosition;
                    transitioning = true;
                }
            }

			if (transitioning) {
                time += Time.deltaTime;
                float _timeRatio = time / transitionDuration;
                Debug.Log("Is transitioning at " + _timeRatio);
                Vector2 _lerpValue = Vector2.Lerp(transitionStart, transitionTarget, _timeRatio);
                Debug.Log("Is transitioning at " + _timeRatio + ", " + _lerpValue);
                transition.anchoredPosition = _lerpValue;
			}

            if (time < transitionDuration) {
                Debug.Log("Elapsed time is less than duration");
                return;
            } else {
                Debug.Log("Setting Target and changing scene");
                transitioning = false;
                transition.anchoredPosition = transitionTarget;
            }

            SceneManager.LoadScene(spaceBarSceneName);
        }
    }
}
