using UnityEngine;

namespace PieceCombat
{
    class PlaySound : MonoBehaviour
    {
        [SerializeField] AudioClip _sound;
        [SerializeField] float _volume = 1f;

        public void Do() => AudioManager.Instance.PlaySound(_sound, transform.position, _volume);
    }
}