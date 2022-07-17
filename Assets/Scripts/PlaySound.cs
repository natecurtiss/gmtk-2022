using UnityEngine;

namespace PieceCombat
{
    class PlaySound : MonoBehaviour
    {
        [SerializeField] AudioClip _sound;
        [SerializeField] float _volume = 1f;

        public void Do() => SoundManager.Instance.PlaySound(_sound, transform.position, _volume);
    }
}