using DG.Tweening;
using UnityEngine;

namespace PieceCombat
{
    class Pop : MonoBehaviour
    {
        [SerializeField] float _up = 1.25f;
        [SerializeField] float _duration = 0.5f;
        Vector3 _initial;

        void Awake() => _initial = transform.localScale;

        public void Do() =>
            DOTween.Sequence()
                .Append(transform.DOScale(new Vector3(_up, _up, _up), _duration))
                .Append(transform.DOScale(_initial, _duration));
    }
}
