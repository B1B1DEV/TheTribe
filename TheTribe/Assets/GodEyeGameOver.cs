using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GodEyeGameOver : MonoBehaviour {
	public Sprite eyeDownLeft;
	public Sprite eyeDown;
	public Sprite eyeIdle;
	public Sprite eyeLeft;
	public Sprite eyeLove;
	public Sprite eyeAngry;
	public Sprite eyeClosed;
	SpriteRenderer eyeRenderer;
	private void Start()
	{
		eyeRenderer = GetComponent<SpriteRenderer>();

	}


	void Update () {
		//Debug.Log(Input.mousePosition);
		if(Input.mousePosition.y > (Screen.height - 200))
		{
			
			if (Input.mousePosition.x < (Screen.width / 2) - 100)
			{
				eyeRenderer.sprite = eyeLeft;
				eyeRenderer.flipX = false;
			}
			else if (Input.mousePosition.x > (Screen.width / 2) + 100)
			{
				eyeRenderer.sprite = eyeLeft;
				eyeRenderer.flipX = true;
			}
			else
			{
				if(Input.GetMouseButton(0))
				{
					eyeRenderer.sprite = eyeClosed;
				}
				else if (Input.GetMouseButton(1))
				{
					eyeRenderer.sprite = eyeClosed;
				}
				else
				{
					eyeRenderer.sprite = eyeIdle;
				}
			}
		}
		else
		{
			if(Input.mousePosition.x < (Screen.width/2) - 100)
			{
				eyeRenderer.flipX = false;
				eyeRenderer.sprite = eyeDownLeft;
			}
			else if (Input.mousePosition.x > (Screen.width / 2) + 100)
			{
				eyeRenderer.flipX = true ;
				eyeRenderer.sprite = eyeDownLeft;
			}
			else
			{
				eyeRenderer.sprite = eyeDown;
			}
		}
	}


}
