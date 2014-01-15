using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject targetFollow;
	public bool isFollowing;

	public Vector3 distanceToFollow = new Vector3(0, 4, -12);
	public Vector3 rotationToFollow = new Vector3(12, 0, 0);

	public float speed = 1;
	public float delay = 0.8f;

	void Start () 
	{
	}

	public void setTarget (GameObject target) 
	{
		isFollowing = true;
		targetFollow = target;

		transform.rotation = Quaternion.Euler(rotationToFollow);
		transform.position = targetFollow.transform.position + distanceToFollow;
	}

	void Update () 
	{
		if (isFollowing && targetFollow != null) 
		{
			speed = targetFollow.GetComponent<Player> ().speed * delay;

			Vector3 newPosition = new Vector3(targetFollow.transform.position.x -transform.position.x ,0,0);
			transform.Translate(newPosition * speed * Time.deltaTime);
		}
	}
}
