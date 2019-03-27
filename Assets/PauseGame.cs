using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    public bool pause;

    void Start()
    {
        pause = false;
    }

        public void onPause()
        {
            pause = !pause;
            if(!pause)
            {
                Time.timeScale = 1;
            }
            else if (pause)
            {
                Time.timeScale = 0;
            }
        }

      }
  
