using UnityEngine;

namespace odt.util
{
    public class VirtualCustomInputCanvas : MonoBehaviour, IVirtualInput
    {
        private VirtualJoystick virtualJoystick;
        private VirtualDrag virtualDrag;

        private void Start()
        {
            virtualJoystick = GetComponentInChildren<VirtualJoystick>();
            virtualDrag = GetComponentInChildren<VirtualDrag>();
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
    }
}