using PieceCombat.Enemies;
using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class SpawnPoint : MonoBehaviour
    {
        [SerializeField] UnityEvent _onHover;
        [SerializeField] UnityEvent _onStopHover;
        [SerializeField] UnityEvent _onPlace;
        [SerializeField] Vector3 _spawnOffset;
        [field: SerializeField] public UnitType[] Allowed { get; private set; }
        public bool IsOccupied { get; set; }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var e))
            {
                IsOccupied = true;
                e.Tile = this;
            }
            else if (other.TryGetComponent<Unit>(out var u))
            {
                if (u is BlockerUnit b)
                {
                    IsOccupied = true;
                    b.Tile = this;
                }
                else if (u is TrapUnit t)
                {
                    IsOccupied = true;
                    t.Tile = this;
                }
            }
        }
        
        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var e))
            {
                if (e.Tile == this)
                {
                    e.Tile = null;
                    IsOccupied = false;
                }
            }
            else if (other.TryGetComponent<Unit>(out var u))
            {
                if (u is BlockerUnit b)
                {
                    if (b.Tile == this)
                    {
                        b.Tile = null;
                        IsOccupied = false;
                    }
                }
                else if (u is TrapUnit t)
                {
                    if (t.Tile == this)
                    {
                        t.Tile = null;
                        IsOccupied = false;
                    }
                }
            }
        }

        public void Hover(Unit unit) => _onHover.Invoke();
        public void StopHover() => _onStopHover.Invoke();
        
        public void Place(Unit unit)
        {
            _onPlace.Invoke();
            Instantiate(unit, transform.position + _spawnOffset, Quaternion.identity);
        }
    }
}