using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class GimmickDoor:MonoBehaviour
    {
        private float high = 3f;
        private float speed = 1f;
        private void Update()
        {
            if (EventFlag.GetPlayedDilde()) {
                DoorOpen();

            }
        }

        private void DoorOpen()
        {
            if (transform.position.y < high) {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else {
                EventFlag.SetPlayedDilde(false);
            }
        }

    }
}
