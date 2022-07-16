using System;
using System.Linq;
using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class Placer : MonoBehaviour
    {
        public static event Action OnPlace;
        
        [SerializeField] UnityEvent _onPlace;
        bool _isPlacing;
        Unit _unit;
        SpawnPoint _hover;

        void Update()
        {
            if (!_isPlacing)
            {
                if (_hover != null)
                    _hover.StopHover();
                _hover = null;
                return;
            }
            if (Physics.Raycast(Mouse(), out var hit))
            {
                if (hit.collider.TryGetComponent<SpawnPoint>(out var spawn))
                {
                    if (!spawn.Allowed.Contains(_unit.Type))
                    {
                        if (_hover != null)
                            _hover.StopHover();
                        _hover = null;
                        return;
                    }

                    if (spawn.IsOccupied)
                    {
                        if (_hover != null)
                            _hover.StopHover();
                        _hover = null;
                        return;
                    }
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        OnPlace?.Invoke();
                        spawn.Place(_unit);
                        _onPlace.Invoke();
                        _isPlacing = false;
                        if (_hover != null)
                            _hover.StopHover();
                        _hover = null;
                    }
                    else
                    {
                        if (_hover != spawn)
                        {
                            if (_hover != null)
                                _hover.StopHover();
                            spawn.Hover(_unit);
                            _hover = spawn;
                        }
                    }
                }
                else
                {
                    if (_hover != null)
                        _hover.StopHover();
                    _hover = null;
                }
            }
            else
            {
                if (_hover != null)
                    _hover.StopHover();
                _hover = null;
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