using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour 
{	
	public Controlls controllerSettings;

	public GameObject playerOne;
	public Vector3 playerPositionInicial = Vector3.up*2;
	public GameObject cameraGame;


	public GameObject sun;
	public Vector3 sunRotation = new Vector3(30,0,0);

	public int kill_y = -500;

	public bool paused = false;
	public bool gameOver = false;

	void Start () 
	{
		controllerSettings = new Controlls ();

		if (playerOne == null) Debug.Log ("Necesario agregar el prefab player");
		else
		{
			playerOne.transform.position = playerPositionInicial;
		}

		if (cameraGame == null) Debug.Log ("Necesario agregar el prefab cameraGame");
		else cameraGame.GetComponent<CameraControl> ().setTarget (playerOne);

		if (sun == null) Debug.Log ("Necesario agregar el sol");
		else sun.transform.rotation = Quaternion.Euler (sunRotation);
	}
	
	void Update () 
	{
		if (!gameOver) 
		{
			/*if (btnPause.active) 
			{
				btnPause.active = false;
				PauseGame ();
			}

			if (btnMainMenu.active) 
			{
				Application.LoadLevel (0);
			}

			if (btnExit.active) 
			{
				Application.Quit ();
			}*/
		}
		else 
		{
			//ANIMACION GAME OVER
		}
	}

	void PauseGame()
	{
		paused = !paused;
		string function;

		if (paused) 
		{
			function = "OnPauseGame";

		} 
		else 
		{
			function = "OnResumeGame";

		}

		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage (function, SendMessageOptions.DontRequireReceiver);
		}
	}

	public void SetGameOver(bool mode)
	{
		gameOver = mode;
		cameraGame.GetComponent<CameraControl> ().isFollowing = false;
		PauseGame ();
	}
}

public class Controlls
{
	public KeyCode forwards = KeyCode.D;
	public KeyCode back = KeyCode.A;
	public KeyCode jump = KeyCode.Space;
	public KeyCode attack = KeyCode.Mouse0;
	public KeyCode action = KeyCode.Mouse0;

	public Controlls () 
	{

	}
}