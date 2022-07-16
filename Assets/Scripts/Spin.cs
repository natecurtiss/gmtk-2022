using UnityEngine;

namespace PieceCombat
{
    class Spin : MonoBehaviour
    {
        [SerializeField] Vector3 _spin = new(0, 0, 180f);
        Vector3 _vel;
        
        void Update() =>
            transform.eulerAngles =
                Vector3.SmoothDamp(transform.eulerAngles, transform.eulerAngles + _spin, ref _vel, 1f);
    }
}
