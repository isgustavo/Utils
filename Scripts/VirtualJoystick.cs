using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace odt.util
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private readonly float maxHandleDistance = 40;
        private readonly float sensitivity = 1f;

        private Image joystickImage;
        private Image handleImage;

        private Vector3 initialPoint;

        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        void Start()
        {
            joystickImage = transform.Find("VirtualButton").GetComponent<Image>();
            handleImage = joystickImage.transform.Find("Handle").GetComponent<Image>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            handleImage.transform.position = joystickImage.transform.position;
            initialPoint = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 dragPoint = eventData.position;
            Vector3 deltaPoint = dragPoint - initialPoint;

            float factor = Mathf.Min(dragPoint.magnitude / (maxHandleDistance), 1f);
            deltaPoint.Normalize();
            deltaPoint *= factor;
            deltaPoint.x *= sensitivity;
            deltaPoint.y *= sensitivity;

            Horizontal = deltaPoint.x;
            Vertical = deltaPoint.y;

            UpdateHandle(dragPoint);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            initialPoint = Vector3.zero;
            Horizontal = 0;
            Vertical = 0;

            UpdateHandle(Vector3.zero);
        }

        private void UpdateHandle(Vector3 dragPoint)
        {
            Vector3 handlePointerDelta;

            if (dragPoint == Vector3.zero)
                handlePointerDelta = dragPoint;
            else 
            {
                handlePointerDelta = dragPoint - initialPoint;
                if (handlePointerDelta.magnitude > maxHandleDistance)
                {
                    handlePointerDelta = handlePointerDelta.normalized * maxHandleDistance;
                }
            }

            handleImage.transform.localPosition = handlePointerDelta;
        }
    }

}
