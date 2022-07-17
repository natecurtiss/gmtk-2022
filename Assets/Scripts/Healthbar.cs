using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PieceCombat
{
    class Healthbar : MonoBehaviour
    {
        Slider _slider;
        [SerializeField] float _duration;

        void Awake() => _slider = GetComponent<Slider>();

        public void Change(float perc) => _slider.DOValue(perc, _duration);
    }
}