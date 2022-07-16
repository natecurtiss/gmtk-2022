using PieceCombat.Enemies;
using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class SpawnPoint : MonoBehaviour
    {
        [SerializeField] UnityEvent _onHover;
        [SerializeField] UnityEvent _onPlace;
        [SerializeField] Vector3 _spawnOffset;
        [field: SerializeField] public UnitType[] Allowed { get; private set; }
        public bool IsOccupied { get; private set; }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Unit>(out _) || other.TryGetComponent<Enemy>(out _))
                IsOccupied = true;
        }
        
        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Unit>(out _) || other.TryGetComponent<Enemy>(out _))
                IsOccupied = false;
        }

        public void Hover(Unit unit)
        {
            Debug.Log($"{gameObject.name} // {unit} // Hover!");
            _onHover.Invoke();
        }

        public void Place(Unit unit)
        {
            _onPlace.Invoke();
            Instantiate(unit, transform.position + _spawnOffset, Quaternion.identity);
        }
    }
}