using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effirot.Test
{
    public class AmperageMultimiterState : MultimeterState
    {
        public override float value => Mathf.Sqrt(P / R);
    }
}