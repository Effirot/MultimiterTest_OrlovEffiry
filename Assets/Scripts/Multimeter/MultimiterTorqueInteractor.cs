using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Effirot.Test
{
    public class MultimiterTorqueInteractor : MonoBehaviour
    {
        [SerializeField]
        private MultimeterState[] _multimeterStates;
        [SerializeField]
        private Transform _torquePoint;

        [Space]
        [SerializeField]
        public UnityEvent<MultimeterState> onStateChanged = new();


        public MultimeterState activeState => _multimeterStates[stateIndex];

        private int stateIndex = 0;

        private void Awake()
        {
            OnMouseScroll(0);
        }
        private void LateUpdate()
        {
            var relativeVector = activeState.transform.position - _torquePoint.position;
            _torquePoint.rotation = Quaternion.Lerp(_torquePoint.rotation, Quaternion.LookRotation(relativeVector), 15 * Time.deltaTime);
        }
        private void OnMouseScroll(float scroll)
        {
            var oldState = activeState;

            stateIndex += Mathf.RoundToInt(scroll);
            stateIndex = stateIndex % (_multimeterStates.Length);
            if (stateIndex < 0)
            {
                stateIndex = _multimeterStates.Length - 1;
            }

            oldState.selected = false;
            oldState.onDeselected.Invoke();

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

