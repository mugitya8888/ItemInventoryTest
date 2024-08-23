using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class EnemyState:MonoBehaviour
    {
        [SerializeField]
        private int attackPower = 35;
        [SerializeField]
        private int enemyHP = 30;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) {

                PlayerState.DecreaseHP(attackPower);

            }
        }

        public void DecleaseEnemyHP(int damage)
        {
            enemyHP -= damage;
            Debug.Log("enemyHP : " + enemyHP);
            if (enemyHP <= 0) {
                Destroy(gameObject);
            }            
        }
    }
}
