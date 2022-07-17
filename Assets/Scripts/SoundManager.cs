using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PieceCombat
{
    class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        [SerializeField] int _maxAudioSources = 20;
        readonly Queue<AudioSource> _audioSources = new();

        void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Init();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Init()
        {
            for (var i = 0; i < _maxAudioSources; i++)
            {
                var go = new GameObject($"AudioSource_{i}");
                go.transform.SetParent(transform);
                var aud = go.AddComponent<AudioSource>();
                aud.playOnAwake = false;
                aud.loop = false;
                aud.spatialBlend = 1f;
                _audioSources.Enqueue(aud);
            }
        }

        public void PlaySound(AudioClip sound, Vector3 position, float volume = 1f)
        {
            if (_audioSources.Count == 0)
                return;
            var aud = _audioSources.Dequeue();
            aud.clip = sound;
            aud.pitch = Random.Range(0.9f, 1.1f);
            aud.volume = volume;
            aud.transform.position = position;
            aud.Play();
            StartCoroutine(ResetAudioSource(aud, sound.length));
        }

        IEnumerator ResetAudioSource(AudioSource aud, float delay)
        {
            yield return new WaitForSeconds(delay);
            _audioSources.Enqueue(aud);
        }
    }
}