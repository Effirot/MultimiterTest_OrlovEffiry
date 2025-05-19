using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedScreenTorque : MonoBehaviour
{


    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
    private void OnValidate()
    {
        LateUpdate();
    }
    private void OnDrawGizmos()
    {
        LateUpdate();
    }
}
