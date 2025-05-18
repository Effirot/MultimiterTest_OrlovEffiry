using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effirot.Test
{
    public class DCVoltageMultimiterState : MultimeterState
    {
        public override float value => P / Mathf.Sqrt(P / R);
    }
}