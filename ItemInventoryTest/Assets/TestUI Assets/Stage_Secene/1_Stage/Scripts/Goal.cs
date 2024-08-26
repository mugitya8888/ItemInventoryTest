using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class Goal:MonoBehaviour
    {
        public GameObject gameClearUI;
      
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) {

                gameClearUI.SetActive(true);

            }
        }
    }
}
