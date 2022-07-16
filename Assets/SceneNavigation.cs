using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PieceCombat
{
    public class SceneNavigation : MonoBehaviour
    {
        [SerializeField] bool NavToSceneOnSpaceBar = false;
        [SerializeField] string spaceBarSceneName;

        // Update is called once per frame
        void Update() {
            if (!NavToSceneOnSpaceBar) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(spaceBarSceneName);
            }
        }
    }
}
