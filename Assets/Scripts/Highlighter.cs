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
            
            Dice.OnRollFrontUnit += _transition.Stop;
            Dice.OnRollAnywhereUnit += _transition.Stop;
            Placer.OnPlace += _transition.Stop;
            
            if (_type == UnitType.Anywhere)
            {
                Dice.OnRollAnywhereUnit += _transition.Do;
            }
            else
            {
                Dice.OnRollFrontUnit += _transition.Do;
                Dice.OnRollAnywhereUnit += _transition.Do;
            }
        }

        void OnDestroy()
        {
            Dice.OnRollFrontUnit -= _transition.Stop;
            Dice.OnRollAnywhereUnit -= _transition.Stop;
            Placer.OnPlace -= _transition.Stop;
            
            if (_type == UnitType.Anywhere)
            {
                Dice.OnRollAnywhereUnit -= _transition.Do;
            }
            else
            {
                Dice.OnRollFrontUnit -= _transition.Do;
                Dice.OnRollAnywhereUnit -= _transition.Do;
            }
        }
    }
}