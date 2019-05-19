using UnityEngine;

namespace odt.util
{
    public class VirtualCustomInputCanvas : MonoBehaviour, IVirtualInput
    {
        private VirtualJoystick virtualJoystick;

        private void Start()
        {
            virtualJoystick = GetComponentInChildren<VirtualJoystick>();
        }

        public float GetAxis(Axis axis)
        {
            switch (axis)
            {
                case Axis.HORIZONTAL:
                    return virtualJoystick.Horizontal;
                case Axis.VERTICAL:
                    return virtualJoystick.Vertical;
                default:
                    return 0;
            }
        }
    }
}