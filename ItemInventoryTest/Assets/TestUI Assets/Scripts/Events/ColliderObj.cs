using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class ColliderObj:MonoBehaviour
    {
        public int doorID;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject);
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("enter");

                if (EventFlag.GetPlayedDilde() == true) {

                    DoorEvents.instance.EventGimmickDoorOpen(doorID);
                    EventFlag.SetPlayedDilde(false);
                }

                
            }
        }
    }
}
