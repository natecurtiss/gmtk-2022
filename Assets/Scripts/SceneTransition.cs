using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PieceCombat
{
    class SceneTransition : MonoBehaviour
    {
        [SerializeField] Animator animator;
        public void Change()
        {
            animator.Play("WaveComplete");
            /*var next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);*/
        }

        public void Lose() {
            animator.Play("BlackSwipeIn");
            StartCoroutine(LoadLose());
            /*var next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);*/
        }

        IEnumerator LoadLose()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Lose");
        }

        public void Win() {
            animator.Play("WhiteFadeIn");
            /*var next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);*/
        }
    }
}
