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
        static Spawner _instance;
        [SerializeField] Spawn[] _spawns;
        [SerializeField] UnityEvent _onWaveFinished;
        [SerializeField] UnityEvent _onSpawn;
        [SerializeField] float _tileLength = 6f;
        [SerializeField] Vector3 _spawnOffset;

        List<Spawn> _remaining;
        readonly List<Transform> _spawnPoints = new();

        public static int Enemies
        {
            get => _enemies;
            set
            {
                _enemies = value;
                if (_enemies <= 0 && _instance._remaining.Count == 0)
                    _instance.LastKilled();
            }
        }
        static int _enemies;

        void Awake()
        {
            _instance = this;
            _enemies = 0;
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
                        Instantiate(_remaining[0].Enemy, spawnPoint.position + _spawnOffset, Quaternion.identity);
                        _remaining.RemoveAt(0);
                        _onSpawn.Invoke();
                        Enemies++;
                    }
                    else
                        continue;
                }
                else
                {
                    Instantiate(_remaining[0].Enemy, spawnPoint.position + _spawnOffset, Quaternion.identity);
                    _remaining.RemoveAt(0);
                    _onSpawn.Invoke();
                    Enemies++;
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
