using UnityEngine;

namespace PieceCombat
{
    class PlaySound : MonoBehaviour
    {
        [SerializeField] AudioClip[] _sounds;
        [SerializeField] float _volume = 1f;
        [SerializeField] bool _isUI;

        public void Do()
        {
            if (_sounds.Length == 0)
                return;
            SoundManager.Instance.PlaySound(_sounds[Random.Range(0, _sounds.Length)], transform.position, _volume, _isUI);
        }
    }
}