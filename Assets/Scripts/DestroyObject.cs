using UnityEngine;

namespace PieceCombat
{
    class DestroyObject : MonoBehaviour
    {
        [SerializeField] float _delay;
        
        public void Do() => Destroy(gameObject);

        public void DoWait() => Destroy(gameObject, _delay);
    }
}
