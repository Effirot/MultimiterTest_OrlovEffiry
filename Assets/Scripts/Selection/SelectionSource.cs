using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Effirot.Test.SelectionSource
{
    public class SelectionSource : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        private static void OnLoad()
        {
            var selectionSourceObject = new GameObject($"[{nameof(SelectionSource)}]");
            DontDestroyOnLoad(selectionSourceObject);

            selectionSourceObject.AddComponent<SelectionSource>();
        }

        [SerializeField]
        private InputActionReference mouseClickAction = null;
        [SerializeField]
        private InputActionReference mousePositionAction = null;
        [SerializeField]
        private InputActionReference mouseScrollAction = null;

        private Collider selected = null;
        private Vector2 mousePosition = new Vector2();
        private List<RaycastResult> raycastResults = new();

        private void Start()
        {
            mouseClickAction.action.Enable();
            mouseClickAction.action.started += OnMouseClick_Event;
            mouseClickAction.action.performed += OnMouseClick_Event;
            mouseClickAction.action.canceled += OnMouseClick_Event;

            mousePositionAction.action.Enable();
            mousePositionAction.action.started += OnMousePosition_Event;
            mousePositionAction.action.performed += OnMousePosition_Event;
            mousePositionAction.action.canceled += OnMousePosition_Event;

            mouseScrollAction.action.Enable();
            mouseScrollAction.action.performed += OnMouseScroll_Event;
        }
        private void OnDestroy()
        {
            mouseClickAction.action.Disable();
            mouseClickAction.action.started -= OnMouseClick_Event;
            mouseClickAction.action.performed -= OnMouseClick_Event;
            mouseClickAction.action.canceled -= OnMouseClick_Event;

            mousePositionAction.action.Disable();
            mousePositionAction.action.started -= OnMousePosition_Event;
            mousePositionAction.action.performed -= OnMousePosition_Event;
            mousePositionAction.action.canceled -= OnMousePosition_Event;

            mouseScrollAction.action.Disable();
            mouseScrollAction.action.performed -= OnMouseScroll_Event;
        }
        private void FixedUpdate()
        {
            var isUIOvered = false;

            var eventSystem = EventSystem.current;
            if (eventSystem != null)
            {
                var data = new PointerEventData(eventSystem);
                data.position = mousePosition;
                eventSystem.RaycastAll(data, raycastResults);

                isUIOvered = raycastResults.Any();
            }

            var ray = Camera.main.ScreenPointToRay(mousePosition);

                
            if (!isUIOvered && Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, ~0))
            {
                if (hitInfo.collider != selected)
                {
                    selected = hitInfo.collider;
                    selected.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                if (selected != null)
                {
                    selected.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
                }

                selected = null;
            }
        }

        private void OnMouseClick_Event(CallbackContext context)
        {

        }
        private void OnMousePosition_Event(CallbackContext context)
        {
            mousePosition = context.ReadValue<Vector2>();
        }
        private void OnMouseScroll_Event(CallbackContext context)
        {
            if (selected != null)
            {
                selected.SendMessage("OnMouseScroll", context.ReadValue<Vector2>().y / 120f, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
