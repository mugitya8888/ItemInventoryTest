using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestUI
{
    public class MenuController:MonoBehaviour
    {
        private Selectable[] selectables;
        private int currentIndex = 0;
        // Start is called before the first frame update
        void Start()
        {
            selectables = GetComponentsInChildren<Selectable>(true);

            if (selectables.Length > 0) {

                selectables[currentIndex].Select();
            }

        }

        public void Navigate(Vector2 direction)
        {
            if (selectables == null || selectables.Length == 0) {
                Debug.Log("—v‘f–³‚µ");
                return;
            }
            SetCurrentIndex(direction);
            OverIndexReset();
            selectables[currentIndex].Select();

        }

        private void SetCurrentIndex(Vector2 direction)
        {
            if (direction.y > 0) // ãŒü‚«‚Ì“ü—Í
                  {
                currentIndex = (currentIndex == 0) ? selectables.Length - 1 : currentIndex - 1;
            }
            else if (direction.y < 0) // ‰ºŒü‚«‚Ì“ü—Í
            {
                currentIndex = (currentIndex == selectables.Length - 1) ? 0 : currentIndex + 1;
            }
            else if (direction.x > 0) // ‰EŒü‚«‚Ì“ü—Í
            {
                currentIndex = (currentIndex == selectables.Length - 1) ? 0 : currentIndex + 1;
            }
            else if (direction.x < 0) // ¶Œü‚«‚Ì“ü—Í
            {
                currentIndex = (currentIndex == 0) ? selectables.Length - 1 : currentIndex - 1;
            }

            
        }

        private void OverIndexReset()
        {
            if (currentIndex < 0) {
                currentIndex = selectables.Length - 1;
            }
            else if (currentIndex >= selectables.Length) {
                currentIndex = 0;
            }
        }
    }
}
