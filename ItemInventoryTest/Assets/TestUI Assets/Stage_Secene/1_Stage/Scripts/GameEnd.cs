using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestUI
{
    public class GameEnd:MonoBehaviour
    {
        private GameObject gameOverUI;
        private GameObject gameClearUI;

        public void Start()
        {
            PlayerState.InitializingPlayerState();
            EventFlag.InitializingEventFlag();
            gameOverUI = transform.GetChild(0).gameObject;
            gameClearUI = transform.GetChild(1).gameObject;
        }
        public void LoadHome()
        {
            PlayerState.InitializingPlayerState();
            EventFlag.InitializingEventFlag();
            gameClearUI.SetActive(false);
            SceneManager.LoadScene(0);
        }

        public void LoadStage1()
        {
            PlayerState.InitializingPlayerState();
            EventFlag.InitializingEventFlag();
            gameOverUI.SetActive(false);
            SceneManager.LoadScene(1);

        }

        public void SetActiveGameoverUI(bool isActive)
        {
            gameOverUI.SetActive(isActive);
        }

        public void SetActiveGameclearUI(bool isActive)
        {
            gameClearUI.SetActive(isActive);
        }
    }
}
