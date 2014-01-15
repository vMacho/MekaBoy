using UnityEngine;
using System.Collections;

public class PadController : MonoBehaviour 
{
	Rect pad = new Rect(0, 0, 100, 100);

	public bool InsidePad(Vector2 pos)
	{
		return (pad.Contains (pos)) ? true : false;
	}

	public ZonePad ZonePressed(Vector2 pos)
	{
		return (pos.x >= pad.width / 2) ? ZonePad.left : ZonePad.right;
	}
}

public enum ZonePad {left, right}
