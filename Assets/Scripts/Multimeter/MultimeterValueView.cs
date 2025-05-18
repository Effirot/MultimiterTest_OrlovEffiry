using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Effirot.Test
{
    [RequireComponent(typeof(TMP_Text))]
    public class MultimeterValueView : MonoBehaviour
    {
        [SerializeField]
        private MultimiterTorqueInteractor multimiterTorque;
        [SerializeField]
        private string stringFormat = "000.00";

        private TMP_Text label;
        private float smoothedValue = 0;

        private void Awake()
        {
            label = GetComponent<TMP_Text>();
        }
        private void Update()
        {
            smoothedValue = Mathf.Lerp(smoothedValue, multimiterTorque.activeState.value, 10 * Time.deltaTime);

            label.text = smoothedValue.ToString(stringFormat);
        }
    }
}
