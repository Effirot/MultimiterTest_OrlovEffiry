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
        private Material _outlineMaterial;
        [SerializeField, Range(0.1f, 30f)]
        private float _smoothSpeed = 5;


        private MeshRenderer _meshRenderer;
        private Material _materialInstance;

        private bool _isSelected;
        private float _transparencyBlend = 0;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _materialInstance = Instantiate(_outlineMaterial);
        }
        private void OnDestroy()
        {
            Destroy(_materialInstance);
        }
        private void LateUpdate()
        {
            _transparencyBlend = Mathf.Lerp(_transparencyBlend, _isSelected ? 1 : 0, _smoothSpeed * Time.deltaTime);
            _materialInstance.SetFloat("Transparency", _transparencyBlend);
        }

        private void OnMouseOver()
        {
            if (!_isSelected)
            {
                _isSelected = true;

                _meshRenderer.sharedMaterials = _meshRenderer.sharedMaterials.Append(_materialInstance).ToArray();
            }
        }
        private void OnMouseExit()
        {
            _transparencyBlend = 0;

            _isSelected = false;

            _meshRenderer.sharedMaterials = _meshRenderer.sharedMaterials.Where(mat => mat != _materialInstance).ToArray();
        }
    }
}
