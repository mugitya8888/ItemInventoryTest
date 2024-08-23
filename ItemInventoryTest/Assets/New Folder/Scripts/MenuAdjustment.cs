using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAdjustment:MonoBehaviour
{
    public GameObject MenuObject;
    private bool isMenuState = false;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) {

            if (isMenuState == false) {
                SwithShowMenu(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else if (isMenuState == true) {
                SwithShowMenu(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
       
    }

    private void SwithShowMenu(bool isShow)
    {
        MenuObject.SetActive(isShow);
        isMenuState = isShow;

        //�}�E�X�J�[�\����\���ɂ��A�ʒu�Œ����
        Cursor.visible = isShow;

    }


}
