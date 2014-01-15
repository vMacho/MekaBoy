using UnityEngine;
using System.Collections;

public class PowerItem : MonoBehaviour 
{
	public powerHability power = powerHability.none;
	public float speed = 1;
	public float amplitude = 0.1f;

	Vector2 btnPosition;

	public PowerItem(Vector3 pos, powerHability pw)
	{
		transform.position = pos;
		power = pw;

		Vector3 objToCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint (
														new Vector3 (transform.position.x, transform.position.y, transform.position.z));

	}

	void Update () 
	{
		transform.Rotate (Vector3.up * Time.deltaTime * speed);
		transform.position += new Vector3 (0, Mathf.Sin (Time.timeSinceLevelLoad * speed) * amplitude, 0) * Time.deltaTime;

		/*if (btnAction != null && btnAction.visible) 
		{
			Vector3 objToCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint (
				new Vector3 (transform.position.x, transform.position.y, transform.position.z));
			
			btnAction.position = new Vector2 (objToCamera.x, objToCamera.y - 150);

			Debug.Log(objToCamera);
		}*/
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") 
		{
			collision.gameObject.GetComponent<Player>().power = power;
			Destroy(gameObject);
		}
	}
}

public enum powerHability {none, knight, ninja, cowboy}
