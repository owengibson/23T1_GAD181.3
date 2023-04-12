using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeadowMateys
{
    public class EventManager : MonoBehaviour
    {
        public delegate void VoidDelegate();
        public static VoidDelegate OnPickup;
    }
}