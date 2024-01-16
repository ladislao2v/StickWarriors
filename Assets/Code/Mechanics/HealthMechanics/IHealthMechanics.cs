using System;

namespace Code.Mechanics.HealthMechanics
{
    public interface IHealthMechanics
    {
        event Action Died;
        event Action<int> HealthChanged; 
        void TakeDamage(int damage);
        void Hill(int hillPoints);
    }
}