

using UnityEngine;
using UnityEngine.Events;

namespace Effirot.Test
{
    public abstract class MultimeterState : MonoBehaviour
    {
        public const float R = 1000f;
        public const float P = 400f;

        [SerializeField]
        public bool selected = false;
        [SerializeField]
        public UnityEvent onSelected = new();
        [SerializeField]
        public UnityEvent onDeselected = new();

        public abstract float value { get; }



    }
}