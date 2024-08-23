using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class Bullet:MonoBehaviour
    {
        public int speed = 10;
        public int despawnTime = 10;
        public int bulletDamage;
        private EnemyState enemy;


        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Destroy(gameObject, despawnTime);
        }

        private void OnCollisionEnter(Collision co)
        {
            if (co.gameObject.CompareTag("Enemy")) {
                enemy = co.gameObject.GetComponent<EnemyState>();
                enemy.DecleaseEnemyHP(bulletDamage);
            }

            Destroy(gameObject);

        }

    }
}
