using System;
using UnityEngine;

namespace UI
{
    public class AnimationFinished : StateMachineBehaviour
    {
        public event Action Finished;
        private bool _fired;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _fired = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_fired || stateInfo.normalizedTime < 1f) return;
            _fired = true;
            Finished?.Invoke();
        }
    }
}