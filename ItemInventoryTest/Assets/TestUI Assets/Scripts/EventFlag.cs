using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public static class EventFlag
    {
        private static bool hasKey;

        public static void SetHasKey(bool flag)
        {
            hasKey = flag;
        }

        public static bool GetHasKey()
        {
            return hasKey;
        }
    }
}
