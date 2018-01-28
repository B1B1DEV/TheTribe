using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CloudManager : MonoBehaviour {
	Transform CloudWallHolder;
	Transform CloudSkyHolder;
	public Image background;
	float moveDuration = 4;
	float moveSpeed = 2;
	static CloudManager instance;
	bool wallDisabled = false;
	public UnityEvent OnFirstWallGone;
	public UnityEvent OnSkyExit;

	void Awake () {
		instance = this;
		CloudWallHolder = transform.Find("CloudWall");
		CloudSkyHolder = transform.Find("CloudSky");

		// For Debug
		//ExitSky();
	}
	
	public static void MoveCloudWall()
	{
		if(instance)
		{
			instance.StartCoroutine(instance.HideCloudsWall());
		}
	}

	public static void ExitSky()
	{
		if (instance)
		{
			instance.StartCoroutine(instance.HideCloudsSky());
		}
	}

	protected IEnumerator HideCloudsSky()
	{
		// Si le 1er mur n'a pas bougé, attendre qu'il bouge
		if(wallDisabled == false)
		{
			yield return StartCoroutine(HideCloudsWall());
		}
		float startTime = 0;
		float percentage = 0;
		Color color = CloudSkyHolder.GetChild(0).GetComponent<Image>().color;
		Color back = background.color;
		while (startTime < moveDuration)
		{
			startTime += Time.deltaTime;
			percentage = startTime / moveDuration;
			color.a = 1 - percentage;
			back.a = color.a;
			background.color = back;
			foreach (Transform cloud in CloudSkyHolder)
			{
				if (cloud.transform.position.x < Screen.width / 2)
				{
					cloud.transform.position = cloud.transform.position + Vector3.left * moveSpeed;
				}
				else
				{
					cloud.transform.position = cloud.transform.position + Vector3.right * moveSpeed;
				}
				cloud.gameObject.GetComponent<Image>().color = color;
			}
			
			yield return 0;
		}
		OnSkyExit.Invoke();
		yield return new WaitForSeconds(0.5f);
		this.gameObject.SetActive(false);
	}

	protected IEnumerator HideCloudsWall()
	{
		yield return new WaitForSeconds(1);
		float startTime = 0;
		while(startTime < moveDuration)
		{
			startTime += Time.deltaTime;
			foreach (Transform cloud in CloudWallHolder)
			{
				if(cloud.transform.position.x < Screen.width / 2)
				{
					cloud.transform.position = cloud.transform.position + Vector3.left * moveSpeed;
				}
				else
				{
					cloud.transform.position = cloud.transform.position + Vector3.right * moveSpeed;
				}
			}
			yield return 0;
		}
		wallDisabled = true;
		OnFirstWallGone.Invoke();
	}
}
