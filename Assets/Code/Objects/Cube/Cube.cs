
using Plugins.DynamicBehaviour;
using UnityEngine;

namespace Code.Objects.Cube
{
    public class Cube : DynamicObject
    {
        [SerializeField] private int _damage = 5;
        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.collider.name);
            
            if(collision.collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }
    }
}