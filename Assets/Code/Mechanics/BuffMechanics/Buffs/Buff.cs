using UnityEngine;

namespace Code.Mechanics.BuffMechanics.Buffs
{
    public abstract class Buff : MonoBehaviour
    {
        public bool IsTimed = true;
        public abstract void OnUpdate();
    }
}