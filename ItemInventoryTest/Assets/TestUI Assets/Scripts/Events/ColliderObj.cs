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
            if (other.gameObject.CompareTag("Player")) {                

                if (EventFlag.GetPlayedDilde() == true) {

                    DoorEvents.instance.EventGimmickDoorOpen(doorID);
                    
                }

                
            }
        }
    }
}
