using System;
using System.Collections.Generic;
using Code.Mechanics.BuffMechanics.Buffs;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEditor.Build.Content;
using UnityEngine;

namespace Code.Mechanics.BuffMechanics
{
    [Serializable]
    public class BuffMechanics : ObjectPart, IMechanics, IUpdatable
    {
        private readonly List<Buff> _buffs = new List<Buff>();

        public void AddBuff(Buff buff)
        {
            _buffs.Add(buff);
        }

        public void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
        }

        public void Update()
        {
            foreach (var buff in _buffs)
                buff.OnUpdate();
        }
    }
}