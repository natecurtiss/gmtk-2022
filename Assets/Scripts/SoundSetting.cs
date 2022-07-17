using UnityEngine;
using UnityEngine.UI;


namespace PieceCombat {
    public class SoundSetting : MonoBehaviour {

        [SerializeField] Sprite soundOff;
        [SerializeField] Sprite soundLow;
        [SerializeField] Sprite soundMedium;
        [SerializeField] Sprite soundHigh;

        [SerializeField] Image soundIcon;
        [SerializeField] Slider soundSlider;

        [SerializeField] AudioSource audioSample;

        private float tempMasterVolume;

        private void Start() {
            SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100), false); //Changing this will also set the volume because of the OnValueChanged
        }

        public void ToggleSound() {
            float _value = PlayerPrefs.GetFloat("SavedMasterVolume", 100);

            if (_value > 1) {
                tempMasterVolume = soundSlider.value;
                SetVolume(0);
            } else {
                SetVolume(tempMasterVolume);
            }

        }

        public void SetVolume(float _value, bool playSound = true) {
            if (_value < 1) {
                _value = .001f;
            }

            if (_value > 66f) {
                soundIcon.sprite = soundHigh;
            } else if (_value > 33f) {
                soundIcon.sprite = soundMedium;
            } else if (_value > 1f) {
                soundIcon.sprite = soundLow;
            } else {
                soundIcon.sprite = soundOff;
            }

            if (playSound) {

                audioSample.Play();
            }

            RefreshSlider(_value);
            PlayerPrefs.SetFloat("SavedMasterVolume", _value);
            AudioManager.Instance.MasterAudio.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        }

        public void SetVolumeFromSlider(bool _playSound = false) {
            SetVolume(soundSlider.value, _playSound);
        }

        public void RefreshSlider(float _value) {
            soundSlider.value = _value;
        }
    }
}
