using UnityEngine;

namespace PieceCombat.Enemies
{
    class LEnemy : Enemy
    {
        float _distance;
        EnemyDir _direction = EnemyDir.Forward;
        Rigidbody _rigidbody;
        Vector3 _forward;

        [SerializeField] float _tileLength = 6f;
        [SerializeField] float _leftWall = 17f;
        [SerializeField] float _rightWall = -17f;
        [SerializeField] float _snap = 3f;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _forward = -transform.forward;
        }

        void FixedUpdate()
        {
            if (!CanMove)
                return;
            var dir = -transform.right;
            if (_direction == EnemyDir.Forward)
                dir = _forward;
            var oldPos = _rigidbody.position;
            var newPos = oldPos + dir * (Rules.UNIT_SPEED * Time.fixedDeltaTime);
            _distance += Rules.UNIT_SPEED * Time.fixedDeltaTime;
            _rigidbody.MovePosition(newPos);
            
            if (_rigidbody.position.z >= _leftWall && _forward == transform.forward)
            {
                var snapped = new Vector3(Snapping.Snap(newPos.x, _snap), newPos.y, Snapping.Snap(newPos.z, _snap));
                _rigidbody.MovePosition(snapped);
                _forward *= -1;
            }
            else if (_rigidbody.position.z <= _rightWall && _forward == -transform.forward)
            {
                var snapped = new Vector3(Snapping.Snap(newPos.x, _snap), newPos.y, Snapping.Snap(newPos.z, _snap));
                _rigidbody.MovePosition(snapped);
                _forward *= -1;
            }

            _direction = _direction switch
            {
                EnemyDir.Forward when _distance >= _tileLength => EnemyDir.Right,
                EnemyDir.Right when _distance >= _tileLength * 2 => EnemyDir.Forward,
                _ => _direction
            };
        }
    }
}