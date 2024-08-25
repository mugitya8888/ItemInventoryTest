using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestUI
{
    public class EnemyMove:MonoBehaviour
    {
        private bool isLookPlayer;
        //private Rigidbody enemyRb;
        private NavMeshAgent enemyAgent;
        //[SerializeField]
        //private float enemySpeed = 2.0f;
        public GameObject player;

        private void Start()
        {
            //enemyRb = GetComponent<Rigidbody>();
            enemyAgent = GetComponent<NavMeshAgent>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) {
                isLookPlayer = true;
                Debug.Log("isLookPlayer : " + isLookPlayer);
            }
        }

        private void FixedUpdate()
        {
            if (isLookPlayer) {

                enemyAgent.destination = player.transform.position;
                //Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                //enemyRb.AddForce(lookDirection * enemySpeed);

            }
        }
    }
}
