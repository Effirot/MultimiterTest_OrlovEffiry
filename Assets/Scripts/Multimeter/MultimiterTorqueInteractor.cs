using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Effirot.Test
{
    public class MultimiterTorqueInteractor : MonoBehaviour
    {
        [SerializeField]
        private MultimeterState[] multimeterStates;
        [SerializeField]
        private bool isLooped;
        [SerializeField]
        private Transform torquePoint;

        [Space]
        [SerializeField]
        public UnityEvent<MultimeterState> onStateChanged = new();


        public MultimeterState activeState => multimeterStates[stateIndex];

        private int stateIndex = 0;

        private void Awake()
        {
            OnMouseScroll(0);
        }
        private void LateUpdate()
        {
            var relativeVector = activeState.transform.position - torquePoint.position;
            torquePoint.rotation = Quaternion.Lerp(torquePoint.rotation, Quaternion.LookRotation(relativeVector), 15 * Time.deltaTime);
        }
        private void OnMouseScroll(float scroll)
        {
            var oldState = activeState;

            stateIndex += Mathf.RoundToInt(scroll);
            stateIndex = stateIndex % (multimeterStates.Length);
            if (stateIndex < 0)
            {
                stateIndex = multimeterStates.Length - 1;
            }

            oldState.selected = false;

            var newState = activeState;
            newState.selected = true;
            newState.onSelected.Invoke();

            if (oldState != newState)
            {
                onStateChanged.Invoke(newState);
            }
        }
    }
}

