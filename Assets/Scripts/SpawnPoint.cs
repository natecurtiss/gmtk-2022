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
            else if (other.TryGetComponent<Unit>(out _))
            {
                IsOccupied = true;
            }
        }
        
        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var e))
            {
                IsOccupied = false;
                if (e.Tile == this)
                    e.Tile = null;
            }
            else if (other.TryGetComponent<Unit>(out _))
            {
                IsOccupied = false;
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