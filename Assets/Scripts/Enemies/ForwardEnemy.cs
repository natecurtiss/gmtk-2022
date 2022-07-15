using UnityEngine;

namespace PieceCombat.Enemies
{
    class ForwardEnemy : Enemy
    {
        Rigidbody _rigidbody;

        void Awake() => _rigidbody = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            var oldPos = _rigidbody.position;
            var newPos = oldPos + -transform.right * (Rules.UNIT_SPEED * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
        }
    }
}