using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TestUI
{
    public class Menu:MonoBehaviour
    {
        //�Q�[���I��
        public void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }

        public void LoadStage1()
        {
            PlayerState.InitializingPlayerState();
            EventFlag.InitializingEventFlag();
            SceneManager.LoadScene(1);
        }
    }
}
