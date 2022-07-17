using UnityEngine;
using UnityEngine.SceneManagement;

namespace PieceCombat
{
    public class SceneControl : MonoBehaviour
    {

		private void OnEnable() {
			var next = SceneManager.GetActiveScene().buildIndex + 1;
			SceneManager.LoadScene(next);
		}

	}
}
