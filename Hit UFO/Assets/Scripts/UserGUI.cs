﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bq;

namespace bq
{

    public class UserGUI : MonoBehaviour
    {
        private UserAction action;
        public int status = 2;
        public float Score = 0;
        GUIStyle style;
        GUIStyle buttonStyle;

        void Start()
        {
            action = Director.GetInstance().CurrentScenceController as UserAction;

            style = new GUIStyle();
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;
            buttonStyle = new GUIStyle("button");
            buttonStyle.fontSize = 30;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 pos = Input.mousePosition;
                action.hit(pos);
            }
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(50, 120, 50, 25), "Restart"))
            {
                this.status = 1;
                this.Score = 0;
                 action.restart();
            }
            GUI.Label(new Rect(50, 85, 100, 50), "Score: " + this.Score);
            if (status == 2)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Hit Disk!", style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Start"))
                {
                    this.status = 1;
                    this.Score = 0;
                    action.restart();
                }
            }
            else if (status == 0)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Time's up!", style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart"))
                {
                    this.status = 1;
                    this.Score = 0;
                    action.restart();
                }
            }
        }
    }
}