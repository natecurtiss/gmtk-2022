using System;
using DG.Tweening;
using UnityEngine;

namespace PieceCombat
{
    class DiceLook : MonoBehaviour
    {
        [SerializeField] Vector3[] _rotations = new Vector3[6];
        [SerializeField] float _duration = 0.5f;

        public void Look(int unit) => transform.DORotate(_rotations[unit - 1], _duration);
    }
}