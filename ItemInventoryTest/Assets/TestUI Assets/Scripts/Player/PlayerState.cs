using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestUI
{
    public class PlayerState:MonoBehaviour
    {
        [SerializeField]
        private static int HP = 100;
        [SerializeField]
        public Slider hpBar;

        private void Update()
        {
            hpBar.value = HP;
        }

        public static void DecreaseHP(int damage)
        {
            HP -= damage;
        }

        public static void IncreaseHP(int healing)
        {
            HP += healing;
        }

        public static int GetHP()
        {
            return HP;
        }
    }
}
