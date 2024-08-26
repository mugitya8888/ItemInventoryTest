using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class PlayerDeath:MonoBehaviour
    {
        public GameObject gameOverUI;


        // Update is called once per frame
        void Update()
        {
            if (PlayerState.GetIsDeath()) {
                gameOverUI.SetActive(true);
                PlayerState.SetIsDeath(false);               

            }

        }
    }
}
