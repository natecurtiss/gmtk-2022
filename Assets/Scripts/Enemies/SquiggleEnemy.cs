namespace PieceCombat.Enemies
{
    class SquiggleEnemy : Enemy
    {
        void FixedUpdate()
        {
            if (!CanMove)
                return;
        }
    }
}