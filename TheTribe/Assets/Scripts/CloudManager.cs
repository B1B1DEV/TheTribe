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
	float moveSpeed = 3f;
	float godMoveSpeed = 3f;
	static CloudManager instance;
	bool wallDisabled = false;
	public UnityEvent OnFirstWallGone;
	public UnityEvent OnSkyExit;
	Transform godSprites;
	SpriteRenderer godRay;
	Coroutine godBlink;
	void Awake () {
		instance = this;
		CloudWallHolder = transform.Find("CloudWall");
		CloudSkyHolder = transform.Find("CloudSky");
		godSprites = transform.Find("God");
		godRay = transform.Find("GodRay").GetComponent<SpriteRenderer>();
		godRay.color = new Color(1,1,1,0);
		// For Debug
		MoveCloudWall();
	}
	
	public void MoveCloudWall()
	{
		StartCoroutine(instance.HideCloudsWall());
	}

	public void ExitSky()
	{

		StartCoroutine(instance.HideCloudsSky());
	}

	protected IEnumerator HideCloudsSky()
	{
		// Si le 1er mur n'a pas bougé, attendre qu'il bouge
		if(wallDisabled == false)
		{
			yield return StartCoroutine(HideCloudsWall());
		}
		if(godBlink != null) StopCoroutine(godBlink);
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
				if (cloud.transform.position.x < Camera.main.transform.position.x)
				{
					cloud.transform.position = cloud.transform.position + Vector3.left * moveSpeed * Time.deltaTime;
				}
				else
				{
					cloud.transform.position = cloud.transform.position + Vector3.right * moveSpeed * Time.deltaTime;
				}
				cloud.gameObject.GetComponent<Image>().color = color;
			}
			godRay.color = new Color(1, 1, 1, back.a);
			godSprites.transform.position = godSprites.transform.position + Vector3.up * godMoveSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		OnSkyExit.Invoke();
		yield return new WaitForSeconds(0.3f);
		color = CloudWallHolder.GetChild(0).GetComponent<Image>().color;
		while(color.a > 0.05f)
		{
			color.a = color.a - Time.deltaTime;
			foreach (Transform cloud in CloudWallHolder)
			{
				cloud.gameObject.GetComponent<Image>().color = color;
			}
			yield return 0;
		}
		
		this.gameObject.SetActive(false);
	}

	protected IEnumerator HideCloudsWall()
	{
		wallDisabled = true;
		yield return new WaitForSeconds(1);
		float startTime = 0;
		godBlink = StartCoroutine(BlinkGodRay());
		while (startTime < moveDuration)
		{
			startTime += Time.deltaTime;
			foreach (Transform cloud in CloudWallHolder)
			{
				if(cloud.transform.position.x < Camera.main.transform.position.x)//Screen.width / 2)
				{
					cloud.transform.position = cloud.transform.position + Vector3.left * moveSpeed * Time.deltaTime;
				}
				else
				{
					cloud.transform.position = cloud.transform.position + Vector3.right * moveSpeed * Time.deltaTime;
				}
			}
			yield return new WaitForEndOfFrame();
		}
		
		OnFirstWallGone.Invoke();
	}

	IEnumerator BlinkGodRay()
	{
		Color color = godRay.color;
		color.a = 0;
		while (true)
		{
			while(color.a > 0.5f)
			{
				color.a -= Time.deltaTime * 0.3f;
				godRay.color = color;
				yield return new WaitForEndOfFrame();
			}

			while (color.a < 1f)
			{
				color.a += Time.deltaTime * 0.3f;
				godRay.color = color;
				yield return new WaitForEndOfFrame();
			}
		}
	}
	
}
