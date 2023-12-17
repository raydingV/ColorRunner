using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managers;
using UnityEngine;

namespace StateMachine
{
    public abstract class AnimationStateMachine
    {
        protected Animator ? _animator { get; set; }
        
        public void SetContext(ref Animator animator)
        {
            _animator = animator;
        }

        public abstract void ChangeAnimationState();
    }
}
