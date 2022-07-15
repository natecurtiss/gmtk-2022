using PieceCombat.Units;
using UnityEngine;

namespace PieceCombat
{
    class Placer : MonoBehaviour
    {
        bool _isPlacing;
        Unit _unit;

        void Update()
        {
            if (!_isPlacing)
                return;
            if (Physics.Raycast(Mouse(), out var hit))
            {
                if (hit.collider.TryGetComponent<SpawnPoint>(out var spawn))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        spawn.Place(_unit);
                        _isPlacing = false;
                    }
                    else
                    {
                        spawn.Hover(_unit);
                    }
                }
            }
        }

        public void Begin(int diceRoll)
        {
            _unit = AvailableUnits.Instance.Units[diceRoll - 1];
            _isPlacing = true;
        }

        Ray Mouse()
        {
            var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
            return Camera.main.ScreenPointToRay(screenPos);
        }
    }
}