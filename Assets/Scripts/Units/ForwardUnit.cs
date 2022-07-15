using UnityEngine;

namespace PieceCombat.Units
{
    class ForwardUnit : Unit
    {
        Rigidbody _rigidbody;

        void Awake() => _rigidbody = GetComponent<Rigidbody>();

        protected override void Go()
        {
            var oldPos = _rigidbody.position;
            var newPos = oldPos + transform.right * (Rules.UNIT_SPEED * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
        }
    }
}