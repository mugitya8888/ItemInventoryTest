using System.Collections;
using System.Collections.Generic;
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
    }
}
