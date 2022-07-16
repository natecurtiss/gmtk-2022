using System;
using System.Collections;
using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PieceCombat
{
    class Dice : MonoBehaviour
    {
        public static event Action OnRollAnywhereUnit;   
        public static event Action OnRollFrontUnit;   
        
        [SerializeField] float _cooldown = 1f;
        [SerializeField] UnityEvent<int> _onRoll;
        [SerializeField] UnityEvent<float> _onCooldownTick;
        [SerializeField] UnityEvent _onFinishCooldown;
        
        WaitForSeconds _waitForCooldown;
        bool _isCooledDown = true;
        bool _hitDown;
        bool _isPlaced;
        float _timer;

        void Awake()
        {
            _waitForCooldown = new(_cooldown - 0.1f);
            _timer = _cooldown;
        }

        void Update()
        {
            if (_isCooledDown && _hitDown)
                return;
            _timer += Time.deltaTime;
            if (_timer >= _cooldown)
            {
                _timer = _cooldown;
                _isCooledDown = true;
                _hitDown = false;
                if (!_isPlaced)
                    _onFinishCooldown.Invoke();
            }
            _onCooldownTick.Invoke(_timer / _cooldown);
        }

        public void Roll()
        {
            if (!_isCooledDown)
                return;
            var result = Random.Range(1, 7);
            _onRoll.Invoke(result);
            _isCooledDown = false;
            _timer = 0f;

            var unit = AvailableUnits.Instance.Units[result - 1];
            if (unit is BlockerUnit or TrapUnit)
                OnRollAnywhereUnit?.Invoke();
            else
                OnRollFrontUnit?.Invoke();
            
            StartCoroutine(Cooldown());
        }

        public void Placed()
        {
            _isPlaced = false;
            if (_isCooledDown)
                _onFinishCooldown.Invoke();
        }

        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(0.1f);
            _hitDown = true;
            yield return _waitForCooldown;
            _isCooledDown = true;
        }
    }
}
