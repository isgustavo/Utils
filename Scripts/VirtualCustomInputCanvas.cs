using UnityEngine;

namespace odt.util
{
    public class VirtualCustomInputCanvas : MonoBehaviour, IVirtualInput
    {
        private VirtualJoystick virtualJoystick;
        private VirtualDrag virtualDrag;
        private VirtualButton virtualButtonA;

        private void Start()
        {
            virtualJoystick = GetComponentInChildren<VirtualJoystick>();
            virtualDrag = GetComponentInChildren<VirtualDrag>();
            virtualButtonA = GetComponentInChildren<VirtualButtonA>();
        }

        public float GetAxis(Axis axis)
        {
            switch (axis)
            {
                case Axis.HORIZONTAL:
                    return virtualJoystick.Horizontal;
                case Axis.VERTICAL:
                    return virtualJoystick.Vertical;
                case Axis.MOUSE_X:
                    return virtualDrag.Horizontal;
                case Axis.MOUSE_Y:
                    return virtualDrag.Vertical;
                default:
                    return 0;
            }
        }

        public bool GetButtonDown(Buttons button)
        {
            switch (button)
            {
                case Buttons.BUTTON_A:
                    return virtualButtonA.IsDown;
                default:
                    return false;
            }
        }
    }
}