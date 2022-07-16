using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class HomeBase : MonoBehaviour
    {
        [SerializeField] UnityEvent<int> _onHealthChange;
        [SerializeField] UnityEvent _onDie;
        [SerializeField] int _startHealth = 20;
        
        int _health;
        bool _isDead;

        void Awake() => _health = _startHealth;

        public void Damage(int damage)
        {
            if (_isDead)
                return;
            _health -= damage;
            _health = Mathf.Max(0, _health);
            _onHealthChange.Invoke(_health);
            if (_health == 0)
            {
                _isDead = true;
                _onDie.Invoke();
            }
        }
    }
}