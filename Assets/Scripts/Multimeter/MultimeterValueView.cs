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
        private MultimiterTorqueInteractor _multimiterTorque;
        [SerializeField]
        private string _stringFormat = "000.00";

        private TMP_Text _label;
        private float _smoothedValue = 0;

        private void Awake()
        {
            _label = GetComponent<TMP_Text>();
        }
        private void Update()
        {
            _smoothedValue = Mathf.Lerp(_smoothedValue, _multimiterTorque.activeState.value, 10 * Time.deltaTime);

            _label.text = _smoothedValue.ToString(_stringFormat);
        }
    }
}
