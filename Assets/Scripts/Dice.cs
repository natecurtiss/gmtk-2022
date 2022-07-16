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
        bool _isPlaced;
        float _timer;

        void Awake() => _waitForCooldown = new(_cooldown);

        void Update()
        {
            if (_isCooledDown)
                return;
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = 0f;
                _isCooledDown = true;
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
            _timer = _cooldown;

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
            yield return _waitForCooldown;
            _isCooledDown = true;
        }
    }
}
