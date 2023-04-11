using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD181_3
{
    public class EventManager : MonoBehaviour
    {
        public delegate void VoidDelegate();
        public static VoidDelegate OnPickup;
    }
}