using UnityEngine;

namespace PieceCombat
{
    public class SceneStart : MonoBehaviour
    {

        [SerializeField] string MusicName;

        void Start()
        {
            AudioManager.Instance.PlayMusic(MusicName);
        }

    }
}
