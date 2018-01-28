using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemDisplayer : MonoBehaviour
{
    public string totemOrGod;

	// Use this for initialization
	void Start ()
    {
        SceneManager.instance.DisplayTotemOrGod(this.transform, totemOrGod);
	}

    public void UnDisplayTotem()
    {
        this.gameObject.SetActive(false);
    }
	
}
