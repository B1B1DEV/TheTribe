﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public void GoBackToMenuScreen()
    {
        SceneManager.instance.LoadMenuScene();
    }
}
