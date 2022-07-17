using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace PieceCombat
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
		public AudioMixer MasterAudio;
		public AudioSource MusicSource;
		public AudioSource SFXSource;
        public AudioClip[] Sound;
        public AudioClip[] Music;

        private void Awake() {
			if(Instance != null) {
				Destroy(this);
                return;
			}

			DontDestroyOnLoad(this);
			Instance = this;

        }


        public void PlaySound(string _name, float _volume = 1) {
            if (SFXSource) {
                AudioClip _clip = null;
                foreach (AudioClip clip in Sound) {
                    if (clip.name == _name) {
                        _clip = clip;
                    }
                }

                if (_clip != null) {
                    SFXSource.clip = _clip;
                    SFXSource.volume = _volume;
                    SFXSource.Play();
                }
            }
        }

        public void StopSound() {
            if (SFXSource) {
                SFXSource.Stop();
            }
        }

        public void PlayMusic(string _name) {
            if (MusicSource) {
                MusicSource.Stop();
                AudioClip _clip = null;
                foreach (AudioClip clip in Music) {
                    if (clip.name == _name) {
                        _clip = clip;
                    }
                }

                if (_clip != null) {
                    MusicSource.clip = _clip;
                    MusicSource.Play();
                }
            }
        }


    }
}
