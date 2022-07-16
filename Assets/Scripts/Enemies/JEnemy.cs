using UnityEngine;

namespace PieceCombat.Enemies
{
    class JEnemy : Enemy
    {
        float _distance;
        EnemyDir _direction = EnemyDir.Right;
        Rigidbody _rigidbody;
        Vector3 _forward;

        [SerializeField] float _tileLength = 6f;
        [SerializeField] float _leftWall = 15f;
        [SerializeField] float _rightWall = -15f;
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

            if (_direction == EnemyDir.Forward && _distance >= _tileLength * 2)
            {
                _direction = EnemyDir.Right;
                _distance = 0f;
            }
            else if (_direction == EnemyDir.Right && _distance >= _tileLength)
            {
                _direction = EnemyDir.Forward;
                _distance = 0f;
            }
        }
    }
}