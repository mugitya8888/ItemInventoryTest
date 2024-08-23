using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class Door:MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.0f;
        [SerializeField]
        private float high = 3.0f;
        private void Update()
        {
            if (EventFlag.GetHasKey()) {
                DoorOpen();
            }

        }

        private void DoorOpen()
        {            
            if (transform.position.y < high) {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }
}
