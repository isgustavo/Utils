using UnityEngine;

namespace odt.util
{
    public interface IVirtualInput
    {
        float GetAxis(Axis axis);
    }

    public enum Axis
    {
        NONE,
        HORIZONTAL,
        VERTICAL,
        MOUSE_X,
        MOUSE_Y
    }

    public class CustomInput
    {
        private IVirtualInput virtualInput;

        private static CustomInput input;

        public static CustomInput InputBy
        {
            get
            {
                if(input == null)
                {
                    input = new CustomInput();
                }
                return input;
            }
        }

        private CustomInput()
        {
            virtualInput = GameObject.FindGameObjectWithTag("VirtualInput").GetComponent<IVirtualInput>();
        }

        public float GetAxis(Axis axis)
        {
            switch (axis)
            {
                case Axis.HORIZONTAL:
                    float horizontal = Input.GetAxis("Horizontal");
                    if (Mathf.Abs(horizontal) < 0.01f)
                    {
                        horizontal = virtualInput.GetAxis(axis);
                    }
                    return horizontal;
                case Axis.VERTICAL:
                    float vertical = Input.GetAxis("Vertical");
                    if (Mathf.Abs(vertical) < 0.01f)
                    {
                        vertical = virtualInput.GetAxis(axis);
                    }
                    return vertical;
                case Axis.MOUSE_X:
                    float mouseX = Input.GetAxis("Mouse X");
                    if (Mathf.Abs(mouseX) < 0.01f)
                    {
                        mouseX = virtualInput.GetAxis(axis);
                    }
                    return mouseX;
                case Axis.MOUSE_Y:
                    float mouseY = Input.GetAxis("Mouse Y");
                    if (Mathf.Abs(mouseY) < 0.01f)
                    {
                        mouseY = virtualInput.GetAxis(axis);
                    }
                    return mouseY;
                default:
                    return 0;
            }
        }
    }
}