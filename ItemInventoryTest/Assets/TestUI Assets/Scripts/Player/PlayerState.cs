using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestUI
{
    public class PlayerState:MonoBehaviour
    {
        private static int HP = 100;
        private static int Ecstasy = 0;
        private static bool isDilde = false;
        private Slider hpBar;
        private Slider ecstasyBar;
        private static bool isDeath = false;
        public GameEnd gameEnd;

        void Start()
        {
            hpBar = transform.GetChild(0).GetComponent<Slider>();
            ecstasyBar = transform.GetChild(1).GetComponent<Slider>();
            InitializingPlayerState();
        }

        private void Update()
        {
            hpBar.value = HP;
            ecstasyBar.value = Ecstasy;
            if (GetIsDeath()) {
                //gameOverUI.SetActive(true);
                gameEnd.SetActiveGameoverUI(true);
                SetIsDeath(false);
            }
        }

        public static void InitializingPlayerState()
        {
            HP = 100;
            Ecstasy = 0;
            isDeath = false;
            isDilde = false;
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

        public static void DecreaseEcstasy(int coolDown)
        {
            Ecstasy -= coolDown;
        }

        public static void IncreaseEcstasy(int sensitive)
        {
            Ecstasy += sensitive;
        }

        public static void SetIsDeath(bool flag)
        {
            isDeath = flag;
        }

        public static bool GetIsDeath()
        {
            return isDeath;
        }

        public static void SetIsDilde(bool flag)
        {
            isDilde = flag;
        }

        public static bool GetIsDilde()
        {
            return isDilde;
        }

    }
}
