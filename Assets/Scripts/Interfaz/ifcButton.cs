using UnityEngine;
using System.Collections;

public enum btnState{
	disable,
	idle,
	hover,
	pushed,
	novisible
};

public delegate void callback();

public class ifcButton : MonoBehaviour {
	
	public Color m_disable;
	public Color m_idle;
	public Color m_hover;
	public Color m_pushed;
	
	public callback ifcEvento{ get; set; }
	
	public btnState m_state;
	
	public btnState state { 
		get { return m_state; } 
		set{ 
			if(m_state!=value ){
				m_state=value;
				switch(m_state){
					case btnState.disable: guiTexture.color = m_disable; break;
					case btnState.idle: guiTexture.color = m_idle; break;
					case btnState.hover: guiTexture.color = m_hover; break;
					case btnState.pushed: guiTexture.color = m_pushed; break;
					case btnState.novisible: gameObject.guiTexture.enabled = false;gameObject.GetComponentInChildren<GUIText>().enabled = false; break;
				}
			}
		}
	}

	void Awake () {
		state = btnState.idle;
	}
	
	void OnMouseEnter(){
		if(state!=btnState.disable)
			state = btnState.hover;
	}
	
	void OnMouseExit(){
		if(state!=btnState.disable)
			state = btnState.idle;
	}

	void OnMouseDown(){
		if(state!=btnState.disable)
			state = btnState.pushed;
	}
	
	void OnMouseUpAsButton(){
		if(state!=btnState.disable){
			state = btnState.hover;

			if(ifcEvento!=null) ifcEvento();// Informar que se pulso el boton
		}
	}
}
