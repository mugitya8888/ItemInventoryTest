using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public static class EventFlag
    {
        private static bool hasKey = false;
        private static bool hasHandgun = false;
        private static bool playedDilde = false;
               

        public static void InitializingEventFlag()
        {
            hasKey = false;
            hasHandgun = false;
            playedDilde = false;
        }
        public static void SetHasKey(bool flag)
        {
            hasKey = flag;
        }

        public static bool GetHasKey()
        {
            return hasKey;
        }

        public static void SetHasHandgun(bool flag)
        {
            hasHandgun = flag;
        }

        public static bool GetHasHandgun()
        {
            return hasHandgun;
        }

        public static void SetPlayedDilde(bool flag)
        {
            playedDilde = flag;
        }

        public static bool GetPlayedDilde()
        {
            return playedDilde;
        }
    }
}
