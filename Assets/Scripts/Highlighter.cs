using PieceCombat.Units;
using UnityEngine;

namespace PieceCombat
{
    class Highlighter : MonoBehaviour
    {
        [SerializeField] UnitType _type = UnitType.Anywhere;
        SlideUp _transition;

        void Awake()
        {
            _transition = GetComponent<SlideUp>();
            if (_type == UnitType.Anywhere)
            {
                Dice.OnRollAnywhereUnit += _transition.Do;
            }
            else
            {
                Dice.OnRollFrontUnit += _transition.Do;
                Dice.OnRollAnywhereUnit += _transition.Do;
            }
            Placer.OnPlace += _transition.Stop;
        }

        void OnDestroy()
        {
            if (_type == UnitType.Anywhere)
            {
                Dice.OnRollAnywhereUnit -= _transition.Do;
            }
            else
            {
                Dice.OnRollFrontUnit -= _transition.Do;
                Dice.OnRollAnywhereUnit -= _transition.Do;
            }
            Placer.OnPlace -= _transition.Stop;
        }
    }
}