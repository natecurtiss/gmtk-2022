using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PieceCombat
{
    class Slide : MonoBehaviour
    {
        Slider _slider;
        [SerializeField] float _duration = 0.2f;

        void Awake() => _slider = GetComponent<Slider>();
        public void Finish() => _slider.DOValue(0f, _duration);
    }
}