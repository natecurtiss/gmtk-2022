using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace PieceCombat
{
    class Shrink : MonoBehaviour
    {
        [SerializeField] float _duration;
        [SerializeField] UnityEvent _onFinish;

        public void Do() => transform.DOScale(0f, _duration).OnComplete(_onFinish.Invoke);
    }
}