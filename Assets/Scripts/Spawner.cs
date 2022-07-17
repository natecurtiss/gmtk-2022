using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using PieceCombat.Enemies;
using PieceCombat.Units;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PieceCombat
{
    class Spawner : MonoBehaviour
    {
        [SerializeField] Spawn[] _spawns;
        [SerializeField] UnityEvent _onWaveFinished;
        [SerializeField] float _tileLength = 6f;
        [SerializeField] Vector3 _spawnOffset;

        List<Spawn> _remaining;
        readonly List<Transform> _spawnPoints = new();
        Enemy _last;

        void Awake()
        {
            _remaining = _spawns.ToList();
            for (var i = 0; i < transform.childCount; i++) 
                _spawnPoints.Add(transform.GetChild(i));
        }

        IEnumerator Start()
        {
            for (var _ = 0; _ < _spawns.Length; _++)
            {
                if (_remaining.Count == 0)
                {
                    // _onWaveFinished.Invoke();
                }
                yield return new WaitForSeconds(_remaining[0].Delay);
                TrySpawn();
            }
        }

        void TrySpawn()
        {
            while (true)
            {
                var random = Random.Range(0, _spawnPoints.Count);
                var spawnPoint = _spawnPoints[random];
                if (Physics.SphereCast(spawnPoint.position, _tileLength, Vector3.up, out var hit))
                {
                    if (!hit.collider.TryGetComponent<Unit>(out _) && !hit.collider.TryGetComponent<Enemy>(out _))
                    {
                        _last = Instantiate(_remaining[0].Enemy, spawnPoint.position + _spawnOffset, Quaternion.identity);
                        _remaining.RemoveAt(0);
                        if (_remaining.Count == 0) 
                            _last.Spawner = this;
                    }
                    else
                        continue;
                }
                else
                {
                    _last = Instantiate(_remaining[0].Enemy, spawnPoint.position + _spawnOffset, Quaternion.identity);
                    _remaining.RemoveAt(0);
                    if (_remaining.Count == 0) 
                        _last.Spawner = this;
                }
                break;
            }
        }

        public void LastKilled() => _onWaveFinished.Invoke();
    }

    [Serializable]
    struct Spawn
    {
        [field: SerializeField]
        public float Delay { get; private set; }
        
        [field: SerializeField]
        public Enemy Enemy { get; private set; }
    }
}
