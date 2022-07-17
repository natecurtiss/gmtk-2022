using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class HomeBase : MonoBehaviour
    {
        [SerializeField] UnityEvent<float> _onHealthChange;
        [SerializeField] UnityEvent _onDie;
        [SerializeField] int _startHealth = 20;
        
        int _health;
        bool _isDead;

        void Awake()
        {
            _health = _startHealth;
            ExplodeAtTheEnd.OnHitHomeBase += Damage;
        }

        void OnDestroy() => ExplodeAtTheEnd.OnHitHomeBase -= Damage;

        public void Damage()
        {
            if (_isDead)
                return;
            _health -= 1;
            _health = Mathf.Max(0, _health);
            _onHealthChange.Invoke((float) _health / _startHealth);
            if (_health == 0)
            {
                _isDead = true;
                _onDie.Invoke();
                CameraShake.Instance.DoBig();
            }
            else
            {
                CameraShake.Instance.Do();
            }
        }
    }
}