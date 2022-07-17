using UnityEngine;

namespace PieceCombat
{
    class UnParent : MonoBehaviour
    {
        public void Do() => transform.parent = null;
    }
}