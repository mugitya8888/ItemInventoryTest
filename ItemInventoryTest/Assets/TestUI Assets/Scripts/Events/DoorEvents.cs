using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class DoorEvents:MonoBehaviour
    {
        public static DoorEvents instance;

        private void Awake()
        {
            instance = this;
        }

        public event Action<int> onGimmickDoorOpen;

        public void EventGimmickDoorOpen(int id)
        {
            if (onGimmickDoorOpen != null) {
                onGimmickDoorOpen(id);
            }
        }

        
    }
}
