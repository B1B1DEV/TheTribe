using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemDisplayer : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        SceneManager.instance.DisplayLastTotemInMenu(this.transform);
	}
	
}
