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
        VERTICAL
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
                default:
                    return 0;
            }
        }
    }
}