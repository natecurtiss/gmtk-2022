using DG.Tweening;
using UnityEngine;

namespace PieceCombat
{
    class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }
        
        [SerializeField] float _big = 0.3f;
        [SerializeField] float _strength = 0.1f;
        [SerializeField] float _duration = 0.2f;

        void Awake() => Instance = this;

        public void Do() => Camera.main.DOShakePosition(_strength, _duration);

        public void DoBig() => Camera.main.DOShakePosition(_big, _duration);
    }
}