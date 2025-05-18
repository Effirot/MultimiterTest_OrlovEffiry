using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effirot.Test
{
    public class ResistanceMultimiterState : MultimeterState
    {
        public override float value => R;
    }
}