using UnityEngine;

namespace PieceCombat.Enemies
{
    class DiagonalEnemy : Enemy
    {
        Rigidbody _rigidbody;
        int _facing = 1;

        [SerializeField] float _leftWall = 17f;
        [SerializeField] float _rightWall = -17f;
        [SerializeField] float _snap = 3f;
        
        void Awake() => _rigidbody = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            if (!CanMove)
                return;
            var oldPos = _rigidbody.position;
            var dir = -transform.right + transform.forward;
            dir.Normalize();
            dir.x *= Rules.UNIT_SPEED * Time.fixedDeltaTime;
            dir.z *= _facing * (Rules.UNIT_SPEED * Time.fixedDeltaTime);
            var newPos = oldPos + dir;
            _rigidbody.MovePosition(newPos);
            if (_rigidbody.position.z >= _leftWall && _facing == 1)
            {
                var snapped = new Vector3(Snapping.Snap(newPos.x, _snap), newPos.y, Snapping.Snap(newPos.z, _snap));
                _rigidbody.MovePosition(snapped);
                _facing = -1;
            }
            else if (_rigidbody.position.z <= _rightWall && _facing == -1)
            {
                var snapped = new Vector3(Snapping.Snap(newPos.x, _snap), newPos.y, Snapping.Snap(newPos.z, _snap));
                _rigidbody.MovePosition(snapped);
                _facing = 1;
            }
        }
    }
}