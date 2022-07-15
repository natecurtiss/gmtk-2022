using System.Collections;
using UnityEngine;

namespace PieceCombat
{
    class Dice : MonoBehaviour
    {
        [SerializeField] float _cooldown = 1f;
        WaitForSeconds _waitForCooldown;
        bool _isCooledDown = true;

        void Awake() => _waitForCooldown = new(_cooldown);

        public void Roll()
        {
            if (!_isCooledDown)
                return;
            var result = Random.Range(1, 7);
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
