using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat.Enemies
{
    abstract class Enemy : MonoBehaviour
    {
        [SerializeField] UnityEvent _onExplode;
        [SerializeField] int _damage = 1;
        protected bool CanMove { get; private set; } = true;
        bool _isBlocking;
        float _blockTimer;
        BlockerUnit _blocker;
        public SpawnPoint Tile { get; set; }

        void Update()
        {
            if (!_isBlocking)
            {
                if (!CanMove && _blocker != null && !_blocker.IsBlocking)
                    CanMove = true;
                return;
            }
            _blockTimer -= Time.deltaTime;
            _blocker.Damage(_blockTimer);
            if (_blockTimer <= 0f)
            {
                _isBlocking = false;
                CanMove = true;
                _blocker.Explode();
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.TryGetComponent<Unit>(out var unit))
            {
                if (!_isBlocking && unit is BlockerUnit blockerUnit)
                {
                    CanMove = false;
                    if (blockerUnit.IsBlocking)
                        return;
                    _isBlocking = true;
                    _blockTimer = Rules.BLOCK_TIME;
                    _blocker = blockerUnit;
                    _blocker.IsBlocking = true;
                }
                else
                {
                    if (unit is TrapUnit trapUnit)
                        trapUnit.Explode();
                    Explode();
                }
            }
        }

        void Explode()
        {
            _onExplode.Invoke();
            if (Tile != null) 
                Tile.IsOccupied = false;
            Destroy(gameObject);
        }

        public void UnOccupyTile()
        {
            if (Tile != null) 
                Tile.IsOccupied = false;
        }
    }
}
