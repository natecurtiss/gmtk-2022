using UnityEngine;
namespace PieceCombat {
    public class Pause : MonoBehaviour {
        public static Pause Instance;
        private bool isPaused = false;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private SoundSetting soundSetting;

        [SerializeField] private bool lowPassAudioOnPause;
        [SerializeField] private float lowPassValueOnPause;
        [SerializeField] private float lowPassValueOffPause;
        [SerializeField] private string pauseMusic;
        [SerializeField] private string gameMusic;

        private void Awake() {
            pauseMenu.SetActive(false);

            if (Instance != null) {
                Destroy(this);
                return;
            }

            DontDestroyOnLoad(this);
            Instance = this;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
        }

        public void TogglePause() {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;

            if (isPaused) {
                soundSetting.RefreshSlider(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
                if (lowPassAudioOnPause) {
                    AudioManager.Instance.MasterAudio.SetFloat("MasterLowpass", lowPassValueOnPause);
                } else {
                    AudioManager.Instance.PlayMusic(pauseMusic);
                }
            } else {
                if (lowPassAudioOnPause) {
                    AudioManager.Instance.MasterAudio.SetFloat("MasterLowpass", lowPassValueOffPause);
                } else {
                    AudioManager.Instance.PlayMusic(gameMusic);
                }
            }
        }


        public void QuitApp() {
            Application.Quit();
        }


    }
}
