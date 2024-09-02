using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public static class EventFlag
    {
        private static bool hasKey;
        private static bool hasHandgun;
        private static bool playedDilde;

        private static void Start()
        {
            InitializingEventFlag();
        }

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
