using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TotemOnHover : MonoBehaviour
{
    
    public void OnMouseEnter()
    {
        Animator anim = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
        GetComponent<AudioSource>().Play();

        if(transform.name == "YesButton")
        {
            anim.SetBool("IsLookingConfirmButton", true);
            anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        else if(transform.name == "NopeButton")
        {
            anim.SetBool("IsLookingRefuseButton", true);
            anim.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void OnMouseExit()
    {
        Animator anim = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
       

        if (transform.name == "YesButton")
        {
            anim.SetBool("IsLookingConfirmButton", false);
        }

        else if (transform.name == "NopeButton")
        {
            anim.SetBool("IsLookingRefuseButton", false);
        }
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
