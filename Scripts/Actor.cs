using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace odt.util
{
    public class Actor : MonoBehaviour
    {
        [SerializeField]
        protected Health health;

        protected StateMachine stateMachine;

        protected void TakeDamage (int amount)
        {
            health.ChangeHealth(-amount);
        }
    }
}

