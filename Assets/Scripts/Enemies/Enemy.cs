using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Enemies
{
    abstract class Enemy : MonoBehaviour
    {
        [SerializeField] UnityEvent _onExplode;
        [SerializeField] int _damage = 1;

        void OnTriggerEnter(Collider col)
        {
            if (col.TryGetComponent<Unit>(out _))
            {
                Explode();
            }
            else if (col.TryGetComponent<HomeBase>(out var homeBase))
            {
                homeBase.Damage(_damage);
                Explode();
            }
        }

        void Explode()
        {
            _onExplode.Invoke();
            Destroy(gameObject);
        }
    }
}
