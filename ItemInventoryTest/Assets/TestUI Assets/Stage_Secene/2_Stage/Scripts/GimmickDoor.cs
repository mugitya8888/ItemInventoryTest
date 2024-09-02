using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class GimmickDoor:MonoBehaviour
    {    
        public int ID;

        private void Start()
        {
            DoorEvents.instance.onGimmickDoorOpen += GimmickDoorOpen;
        }
        

        private void GimmickDoorOpen(int id)
        {
            Debug.Log("GimickDoorOpen()" + id);
            if (id == ID) {
                LeanTween.moveLocalY(gameObject, 4f, 1f).setEaseOutQuad();
                EventFlag.SetPlayedDilde(false);
            }

        }
    }
}
