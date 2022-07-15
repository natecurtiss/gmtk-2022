using PieceCombat.Units;
using UnityEngine;

namespace PieceCombat
{
    class AvailableUnits : MonoBehaviour
    {
        public static AvailableUnits Instance { get; private set; }

        [field: SerializeField, Tooltip("This has to be exactly 6 units!")] 
        public Unit[] Units { get; private set; } = new Unit[6];

        void Awake() => Instance = this;
    }
}