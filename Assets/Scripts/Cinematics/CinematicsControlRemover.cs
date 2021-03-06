using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start() 
        {
            {
                GetComponent<PlayableDirector>().played += DisableControl;
                GetComponent<PlayableDirector>().stopped += EnableControl;
                player = GameObject.FindWithTag("Player");
            }
        }

        void DisableControl(PlayableDirector pd)
        {
                player.GetComponent<ActionScheduler>().CancelCurrentAction();
                player.GetComponent<PlayerControl>().enabled = false;

        }

        void EnableControl(PlayableDirector pd)
        {
           player.GetComponent<PlayerControl>().enabled = true;
        }

    //     public event Action<float> onFinish;

    //     private void Start() 
    //     {
    //         Invoke("OnFinish", 3f);    
    //     }

    //     void OnFinish()
    //     {
    //         onFinish(4.3f);
    //     }
     }
}

