using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class Dice : MonoBehaviour
    {
        [SerializeField] float _cooldown = 1f;
        [SerializeField] UnityEvent<int> _onRoll;
        WaitForSeconds _waitForCooldown;
        bool _isCooledDown = true;

        void Awake() => _waitForCooldown = new(_cooldown);

        public void Roll()
        {
            if (!_isCooledDown)
                return;
            var result = Random.Range(1, 7);
            _onRoll.Invoke(result);
            Debug.Log($"Dice Roll: {result}.");
            _isCooledDown = false;
            StartCoroutine(Cooldown());
        }

        IEnumerator Cooldown()
        {
            yield return _waitForCooldown;
            _isCooledDown = true;
        }
    }
}
