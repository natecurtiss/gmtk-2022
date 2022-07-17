using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace PieceCombat {
    public class GenericButton : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler {

        [SerializeField] private Color selectedColor;
        private Color unselectedColor;
        private RectTransform rectTransform;
        private bool wobble;
        [SerializeField] private Image outline;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI text;

        private void Awake() {
            unselectedColor = outline.color;
            rectTransform = GetComponent<RectTransform>();
            SetHover(false);
        }

        public void OnPointerEnter(PointerEventData _eventData) {
            wobble = true;
            StartCoroutine(UIWobble(rectTransform, 1.15f, 10f, 5f));

            SetHover(wobble);
        }

        public void OnPointerExit(PointerEventData _eventData) {
            wobble = false;

            SetHover(wobble);
        }

        private void SetHover(bool _enabled) {
            if (AudioManager.Instance) {
                AudioManager.Instance.PlaySound(_enabled ? "select" : "deselect");
            }
            if (outline)
                outline.color = _enabled ? selectedColor : unselectedColor;
            if (icon)
                icon.color = _enabled ? selectedColor : unselectedColor;
            if (text)
                text.color = _enabled ? selectedColor : unselectedColor;
            if (outline)
                outline.enabled = _enabled;
        }

        private IEnumerator UIWobble(RectTransform _rectTransform, float _targetScaleValue, float _intensity, float _speed) {
            Vector3 _startingScale = _rectTransform.localScale;
            float _timeElapsed = 0f;
            float _duration = .1f;
            Vector3 _targetScale = new Vector3(_targetScaleValue, _targetScaleValue, _targetScaleValue);

            //Scale Up
            while (_rectTransform.localScale != _targetScale) {
                _rectTransform.localScale = Vector3.Lerp(_startingScale, _targetScale, _timeElapsed / _duration);
                _timeElapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            _duration = 1f / _speed;
            _timeElapsed = _duration / 2; //Start half way through the _duration.  This should be the starting rotation
            bool _isPinging = true;
            while (wobble) {
                Quaternion _rot = _rectTransform.rotation;
                float _rotZ = Mathf.Lerp(-_intensity, _intensity, _timeElapsed / _duration);
                _rot = Quaternion.Euler(new Vector3(_rot.eulerAngles.x, _rot.eulerAngles.y, _rotZ));
                _rectTransform.rotation = _rot;

                if (_isPinging) {
                    _timeElapsed += Time.unscaledDeltaTime;
                    if (_timeElapsed >= _duration) {
                        _isPinging = !_isPinging;
                    }
                } else {
                    _timeElapsed -= Time.unscaledDeltaTime;
                    if (_timeElapsed <= 0) {
                        _isPinging = !_isPinging;
                    }
                }
                yield return null;
            }

            //Scale back down
            _rectTransform.rotation = Quaternion.identity;
            Vector3 _endingScale = _rectTransform.localScale;
            _timeElapsed = 0f;
            _duration = .1f;
            while (_rectTransform.localScale != _startingScale) {
                _rectTransform.localScale = Vector3.Lerp(_endingScale, _startingScale, _timeElapsed / _duration);
                _timeElapsed += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        private void OnDisable() {
            SetHover(false);
            rectTransform.rotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;
        }
    }
}
