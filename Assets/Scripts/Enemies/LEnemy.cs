using UnityEngine;

namespace PieceCombat.Enemies
{
    class LEnemy : Enemy
    {
        void FixedUpdate()
        {
            if (!CanMove)
                return;
        }
    }
}