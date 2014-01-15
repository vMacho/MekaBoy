using UnityEngine;
using System.Collections;

public enum ActualScene{MainMenu,Options,Credits,Game}

public class ifcGeneral : MonoBehaviour {
	public static ifcGeneral instance{ get; private set; }	
	public ActualScene stage;

	void Awake(){ instance = this; }
		
	void Start()
	{
		Debug.Log (stage);
		switch (stage) 
		{
			case ActualScene.MainMenu:
				SetEvent("btnStartGame", OnBotonStart );
				SetEvent("btnOptions", OnBotonOptions );
				SetEvent("btnCredits", OnBotonCredits );
				SetEvent("btnExit", OnBotonExit );
				break;
			case ActualScene.Options:
				SetEvent("btnBack", OnBotonBack );
				break;
			case ActualScene.Credits:
				SetEvent("btnBack", OnBotonBack );
				break;
			case ActualScene.Game:
				
				break;
		}
	}
	void OnBotonBack(){
		Application.LoadLevel(0);
	}

	void OnBotonOptions(){
		Application.LoadLevel(1);
	}

	void OnBotonCredits(){
		Application.LoadLevel(2);
	}

	void OnBotonStart(){
		Application.LoadLevel(3);
	}

	void OnBotonExit(){
		Application.Quit ();
	}
	
	public void SetEvent( string _path, callback _callback ){
		Transform t = transform.Find (_path);
		if(t!=null){
			ifcButton btn = t.gameObject.GetComponent<ifcButton>();
			if( btn!=null ) btn.ifcEvento = (_callback);
		}
	}
	
}
