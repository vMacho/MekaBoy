using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int life = 3;
	public float speed = 20;
	public float jump = 1;
	public int strong = 10;

	public float rozamiento = 0.8f;
	public float gravity = -9.8f;
	public Vector3 velocity;

	public Controlls controllerSettings;
	public bool pauseGame;

	public powerHability power = powerHability.none;

	PadController Pad;


	PlayerStates states = PlayerStates.Idle;

	void Start () 
	{
		velocity = new Vector3 ();

		controllerSettings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameData>().controllerSettings;
		if(controllerSettings == null) Debug.Log ("ERROR NO SE ENCONTRO LA CONFIGURACION POR DEFECTO");

		pauseGame = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameData> ().paused;

		Pad = GameObject.Find ("PadController").GetComponent<PadController> ();
	}

	void Update()
	{
		if(!pauseGame)
		{
			UpdateControllerPC ();
			UpdateControllerPhone();

			UpdateAnim ();

			UpdateLife();
		}
	}

	void UpdateControllerPC()
	{
		if (controllerSettings != null) 
		{
			if (Input.GetKey (controllerSettings.forwards))
			{
				velocity.x = speed * Time.deltaTime;
				transform.rotation = Quaternion.Euler(Vector3.zero);
			}
			else if (Input.GetKey (controllerSettings.back))
			{
				velocity.x = speed * Time.deltaTime;
				transform.rotation = Quaternion.Euler(Vector3.up * 180);
			}

			/*if (Input.GetKey (controllerSettings.jump) && states == PlayerStates.Idle)
			{
					velocity.y = jump;
					states = PlayerStates.Jump;
			} 
			else if (states != PlayerStates.Jump)  velocity.y = gravity * Time.deltaTime * -1;*/
			
			//velocity = new Vector3 (velocity.x * rozamiento, velocity.y + gravity * Time.deltaTime, velocity.z);
			velocity = new Vector3 (velocity.x * rozamiento, velocity.y, velocity.z);
			
			if(Mathf.Abs(velocity.x) < 0.1f) velocity.x = 0;
			velocity.z = 0;
			transform.Translate (velocity);


			if(Input.GetKey (controllerSettings.attack) && states != PlayerStates.Attack)
			{
				states = PlayerStates.Attack;
			}
		}
	}

	void UpdateControllerPhone()
	{
		Vector2 touchDeltaPosition = Vector2.zero;
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
		{
			touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			if (Pad != null && Pad.InsidePad (touchDeltaPosition)) 
			{
				if (Pad.ZonePressed(touchDeltaPosition) == ZonePad.left) 
				{
					velocity.x = speed * Time.deltaTime;
					transform.rotation = Quaternion.Euler (Vector3.zero);	
				}
				
				
				if (Pad.ZonePressed(touchDeltaPosition) == ZonePad.right)
				{
					velocity.x = speed * Time.deltaTime;
					transform.rotation = Quaternion.Euler(Vector3.up * 180);
				}		
			}
		}
		velocity = new Vector3 (velocity.x * rozamiento, velocity.y, velocity.z);
		
		if(Mathf.Abs(velocity.x) < 0.1f) velocity.x = 0;
		velocity.z = 0;
		transform.Translate (velocity);
		
		
		if(touchDeltaPosition.x > Screen.width/2 && states != PlayerStates.Attack && states != PlayerStates.Jump)
		{
			states = PlayerStates.Attack;
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		/*ContactPoint contact = collision.contacts[0];
		Vector3 pos = contact.point;

		if (collision.gameObject.tag == "Ground") 
		{
			states = PlayerStates.Idle;
			//transform.position = new Vector3(transform.position.x, pos.y + 0.5f, transform.position.z);
		}

		foreach (ContactPoint cont in collision.contacts) {
			Debug.DrawRay(cont.point, cont.normal, Color.red);
		}*/
	}

	void OnCollisionExit(Collision collision)
	{
		states = PlayerStates.Jump;
	}

	void UpdateAnim()
	{
		switch (states) 
		{
			case PlayerStates.Attack:
				Debug.Log("ATACA!");
				states = PlayerStates.Idle;
				break;
			case PlayerStates.Idle:
				break;
			case PlayerStates.Jump:
				break;
			case PlayerStates.Dead:
				Destroy(gameObject);
				break;
		}
	}

	void OnPauseGame ()
	{
		pauseGame = true;
	}

	void OnResumeGame ()
	{
		pauseGame = false;
	}

	void UpdateLife()
	{
		if (transform.position.y <= GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameData> ().kill_y) 
		{
			life = 0;
		}

		if (life <= 0) 
		{
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameData> ().SetGameOver(true);
			states = PlayerStates.Dead;
		}
	}

	enum PlayerStates
	{
		Idle,
		Jump,
		Attack,
		Dead
	};
}
