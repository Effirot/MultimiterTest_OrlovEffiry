

using System.Linq;
using TMPro;
using UnityEngine;

namespace Effirot.Test
{
    [RequireComponent(typeof(TMP_Text))]
    public class AllStatesView : MonoBehaviour
    {
        [SerializeField]
        private MultimeterState[] _multimeterStates = new MultimeterState[0];
        [SerializeField]
        private string _stringFormat = "000.00";

        private TMP_Text label;

        private void Awake()
        {
            label = GetComponent<TMP_Text>();
        }
        private void Update()
        {
            label.text = string.Join("\n", _multimeterStates.Select(StateToString));
        }
        private string StateToString(MultimeterState state)
        {
            var result = state.name + " - " + (state.selected ? state.value.ToString(_stringFormat) : 0);

            if (state.selected)
            {
                result = "<u>" + result + "</u>";
            }

            return result;
        }
        private void OnValidate()
        {
            label ??= GetComponent<TMP_Text>();

            Update();
        }
    }
}