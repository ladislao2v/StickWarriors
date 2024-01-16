using System;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Views.Animation
{
    [Serializable]
    public class AttackAnimation : ObjectPart, IView
    {
        private static int Attack = Animator.StringToHash(nameof(Attack));
        
        [SerializeField] private Animator _animator;

        [Button]
        public void OnAttack()
        {
            _animator.SetTrigger(Attack);
        }
    }
}