using Code.Mechanics.HealthMechanics;
using Plugins.DynamicBehaviour;

namespace Code.Objects.Player
{
    public class Player : DynamicObject, IDamageable
    {
        public void TakeDamage(int damage)
        {
            if(TryGetMechanics(out IHealthMechanics healthMechanics))
                healthMechanics.TakeDamage(damage);
        }
    }
}
