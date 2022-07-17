using UnityEngine;
using UnityEngine.SceneManagement;

namespace PieceCombat
{
    class SceneTransition : MonoBehaviour
    {
        // TODO: Make this transition
        public void Change()
        {
            var next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);
        }
    }
}
