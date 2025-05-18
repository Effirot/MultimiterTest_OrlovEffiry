using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Effirot.Test.SelectionSource
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SelectionOutline : MonoBehaviour
    {
        [SerializeField]
        private Material outlineMaterial;
        [SerializeField, Range(0.1f, 30f)]
        private float smoothSpeed = 5;


        private MeshRenderer meshRenderer;
        private Material materialInstance;

        private bool isSelected;
        private float transparencyBlend = 0;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            materialInstance = Instantiate(outlineMaterial);
        }
        private void OnDestroy()
        {
            Destroy(materialInstance);
        }
        private void LateUpdate()
        {
            transparencyBlend = Mathf.Lerp(transparencyBlend, isSelected ? 1 : 0, smoothSpeed * Time.deltaTime);
            materialInstance.SetFloat("Transparency", transparencyBlend);
        }

        private void OnMouseOver()
        {
            if (!isSelected)
            {
                isSelected = true;

                meshRenderer.sharedMaterials = meshRenderer.sharedMaterials.Append(materialInstance).ToArray();
            }
        }
        private void OnMouseExit()
        {
            transparencyBlend = 0;

            isSelected = false;

            meshRenderer.sharedMaterials = meshRenderer.sharedMaterials.Where(mat => mat != materialInstance).ToArray();
        }
    }
}
