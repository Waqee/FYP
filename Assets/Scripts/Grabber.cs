using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour {

	public static GameObject current;
	public static bool grabbed = false;
	public Vector3 offset;
	public float rotation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!grabbed) {
			if (current != null)
				current.GetComponent<Renderer> ().material.color = Color.white;
			current = null;
			Collider[] colliders;
			colliders = Physics.OverlapSphere (transform.position, 0000000.1f);
			float min = Mathf.Infinity;
			foreach (var collider in colliders) {
				var go = collider.gameObject; 
				if (go.name != "Floor" && go.name != "CutPlane" && go.name != "Wall" && Vector3.Distance (transform.position, go.transform.position) < min)
					current = go;
			}
			if (current != null)
				current.GetComponent<Renderer> ().material.color = Color.green;
		}

		if (current && !SixenseInput.Controllers [1].GetButton (SixenseButtons.BUMPER) && SixenseInput.Controllers [1].GetButtonDown (SixenseButtons.TRIGGER)) {
			grabbed = true;
			offset = current.transform.position - transform.position;
		}

		if (grabbed) {
			if (current == null)
				grabbed = false;
			else {
				current.transform.position = offset + transform.position;
				if(SixenseInput.Controllers [1].GetButtonUp (SixenseButtons.TRIGGER))
					grabbed = false;
			}

		}


	}
}
