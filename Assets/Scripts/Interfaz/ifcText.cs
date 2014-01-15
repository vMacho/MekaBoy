using UnityEngine;
using System.Collections;

public class ifcText : MonoBehaviour {
	
	public bool m_shadow = true;
	
	GameObject m_iShadow;
	string m_lastText;
	
	// Use this for initialization
	void Start () {
		m_iShadow = GameObject.Instantiate(gameObject) as GameObject;
		GameObject.DestroyImmediate( m_iShadow.GetComponent<ifcText>() );
		m_iShadow.transform.parent = transform;
		
		m_iShadow.transform.localPosition = transform.localPosition;
		m_iShadow.guiText.pixelOffset = guiText.pixelOffset + new Vector2(3,-3);
		
		Material mat = m_iShadow.guiText.material;
		mat.color = Color.black;
		m_iShadow.guiText.material = mat;
		
	}
	
	// Update is called once per frame
	void Update () {
		if( m_lastText != guiText.text ){
			m_iShadow.guiText.text = guiText.text;
			m_lastText = guiText.text;
		}
	}
}
