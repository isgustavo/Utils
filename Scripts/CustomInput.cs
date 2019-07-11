using UnityEngine;

namespace odt.util
{
    public interface IVirtualInput
    {
        float GetAxis(Axis axis);

        bool GetButtonDown(Buttons button);
    }

    public enum Axis
    {
        NONE,
        HORIZONTAL,
        VERTICAL,
        MOUSE_X,
        MOUSE_Y
    }

    public enum Buttons
    {
        NONE,
        BUTTON_A
    }

    public class CustomInput
    {
        private IVirtualInput virtualInput;
        private bool hasTouchInput;

        private static CustomInput input;

        public static CustomInput Instance
        {
            get
            {
                if (input == null)
                {
                    input = new CustomInput();
                }
                return input;
            }
        }

        private CustomInput()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("VirtualInput");
            if(gameObject != null)
            {
                virtualInput = gameObject.GetComponent<IVirtualInput>();
                hasTouchInput = true;
            } else
            {
                Debug.LogWarning("Virtual Input component not found");
                hasTouchInput = false;
            }
        }

        public bool HasHorizontalOrVerticalInput()
        {
            return Mathf.Abs(GetAxis(Axis.HORIZONTAL)) + Mathf.Abs(GetAxis(Axis.VERTICAL)) > 0f;
        }

        public float GetAxis(Axis axis)
        {
            switch (axis)
            {
                case Axis.HORIZONTAL:
                    float horizontal = Input.GetAxis("Horizontal");
                    if (hasTouchInput && Mathf.Abs(horizontal) < 0.01f)
                    {
                        horizontal = virtualInput.GetAxis(axis);
                    }
                    return horizontal;
                case Axis.VERTICAL:
                    float vertical = Input.GetAxis("Vertical");
                    if (hasTouchInput && Mathf.Abs(vertical) < 0.01f)
                    {
                        vertical = virtualInput.GetAxis(axis);
                    }
                    return vertical;
                case Axis.MOUSE_X:
                    float mouseX = Input.GetAxis("Mouse X");
                    if (hasTouchInput && Mathf.Abs(mouseX) < 0.01f)
                    {
                        mouseX = virtualInput.GetAxis(axis);
                    }
                    return mouseX;
                case Axis.MOUSE_Y:
                    float mouseY = Input.GetAxis("Mouse Y");
                    if (hasTouchInput && Mathf.Abs(mouseY) < 0.01f)
                    {
                        mouseY = virtualInput.GetAxis(axis);
                    }
                    return mouseY;
                default:
                    return 0;
            }
        }

        public bool GetButton(Buttons button)
        {
            switch (button)
            {
                case Buttons.BUTTON_A:
                    bool b = Input.GetButtonDown("Fire1");
                    if (!b)
                    {
                        b = virtualInput.GetButtonDown(button);
                    }
                    return b;
                default:
                    return false;
            }
        }
    }
}