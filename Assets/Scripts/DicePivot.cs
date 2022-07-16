using DG.Tweening;
using UnityEngine;

namespace PieceCombat
{
    class DicePivot : MonoBehaviour
    {
        [SerializeField] Vector3 _spin = new(0, 0, 180f);
        [SerializeField] bool _shouldSpin = true;
        [SerializeField] float _stopDuration;
        Vector3 _vel;
        
        void Update()
        {
            if (_shouldSpin)
                transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, transform.eulerAngles + _spin, ref _vel, 1f);
        }

        public void Stop(int unit)
        {
            _shouldSpin = false;
            _vel = Vector3.zero;
            transform.DORotate(Vector3.zero, _stopDuration);
        }

        public void Begin() => _shouldSpin = true;
    }
}