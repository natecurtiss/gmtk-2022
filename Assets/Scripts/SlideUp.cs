using DG.Tweening;
using UnityEngine;

namespace PieceCombat
{
    class SlideUp : MonoBehaviour
    {
        Vector3 _initial;
        [SerializeField] float _up = 0.5f;
        [SerializeField] float _duration = 0.2f;
        bool _isUp;

        void Awake() => _initial = transform.position;

        public void Do()
        {
            if (_isUp)
                return;
            _isUp = true;
            transform.DOMoveY(_initial.y + _up, _duration);
        }

        public void Stop()
        {
            if (!_isUp)
                return;
            Debug.Log("stop");
            _isUp = false;
            transform.DOMoveY(_initial.y, _duration);
        }
    }
}
