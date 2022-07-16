using DG.Tweening;
using UnityEngine;

namespace PieceCombat
{
    class SlideUp : MonoBehaviour
    {
        Color _initial;
        Color _to;
        MeshRenderer _renderer;
        [SerializeField] float _alpha = 0.5f;
        [SerializeField] float _duration = 0.2f;
        bool _isFaded;

        void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _initial = _renderer.material.color;
            _to = new(_initial.r, _initial.g, _initial.b, _alpha);
        }

        public void Do()
        {
            if (_isFaded)
                return;
            _isFaded = true;
            _renderer.material.DOColor(_to, _duration);
        }

        public void Stop()
        {
            if (!_isFaded)
                return;
            _isFaded = false;
            _renderer.material.DOColor(_initial, _duration);
        }
    }
}
