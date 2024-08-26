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
        private static bool isDeath;

        //static void Start()
        //{
        //    Debug.Log("PlayerState Start()");
        //    HP = 100;
        //    isDeath = false;
        //}

        private void Update()
        {
            hpBar.value = HP;
        }

        public static void InitializingPlayerState()
        {
            HP = 100;
            isDeath = false;
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

        public static void SetIsDeath(bool flag)
        {
            isDeath = flag;
        }

        public static bool GetIsDeath()
        {
            return isDeath;
        }
    }
}
