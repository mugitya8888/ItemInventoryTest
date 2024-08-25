using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class Menu:MonoBehaviour
    {
        //ゲーム終了
        public void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
