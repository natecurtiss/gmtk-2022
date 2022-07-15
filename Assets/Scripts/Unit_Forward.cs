using UnityEngine;

namespace PieceCombat
{
    class Unit_Forward : Unit
    {
        Rigidbody _rigidbody;

        void Awake() => _rigidbody = GetComponent<Rigidbody>();

        protected override void Go()
        {
            var oldPos = _rigidbody.position;
            var newPos = oldPos + transform.right * Rules.UNIT_SPEED;
            _rigidbody.MovePosition(newPos);
        }
    }
}