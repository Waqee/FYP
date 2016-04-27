using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour {
    public OnClickRequestOwnership script;
    public static GameObject current;
    public GameObject leftpointer;
	public static bool grabbed = false;
	public Vector3 offset;
    public float scalingdist;
	Quaternion pointerrotation;
    Quaternion objectrotation;
    Color color;

    int changecol = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!grabbed) {
			if (current != null && current.GetComponent<Renderer>())
				current.GetComponent<Renderer> ().material.color = color;
			current = null;
			Collider[] colliders;
			colliders = Physics.OverlapSphere (transform.position, 0000000.1f);
			float min = Mathf.Infinity;
			foreach (var collider in colliders) {
				var go = collider.gameObject;
                if (go.name != "Floor" && go.name != "CutPlane" && go.name != "Wall" && Vector3.Distance(transform.position, go.transform.position) < min)
                {
                    current = go;
                    color = current.GetComponent<Renderer>().material.color;
                }
			}
            if (current != null && current.GetComponent<Renderer>())
                current.GetComponent<Renderer>().material.color = Color.green;
		}

		if (current && !SixenseInput.Controllers [1].GetButton (SixenseButtons.BUMPER) && SixenseInput.Controllers [1].GetButtonDown (SixenseButtons.TRIGGER)) {
			grabbed = true;
            OnClickRequestOwnership other = (OnClickRequestOwnership)current.GetComponent(typeof(OnClickRequestOwnership));
            if(other != null)
                other.Take();
            offset = current.transform.position - transform.position;
		}

		if (grabbed) {
			if (current == null)
				grabbed = false;
			else {
				current.transform.position = offset + transform.position;

                if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.ONE))
                    scalingdist = Vector3.Distance(transform.position, leftpointer.transform.position);

                if (SixenseInput.Controllers[0].GetButton(SixenseButtons.ONE))
                    current.transform.localScale = Vector3.one * (Vector3.Distance(transform.position, leftpointer.transform.position) - scalingdist + 1);


                if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.THREE))
                {
                    pointerrotation = leftpointer.transform.rotation;
                    objectrotation = current.transform.rotation;
                }

                if (SixenseInput.Controllers[0].GetButton(SixenseButtons.THREE))
                    current.transform.rotation = (leftpointer.transform.rotation * Quaternion.Inverse( pointerrotation)) * objectrotation;

                if (SixenseInput.Controllers[0].GetButton(SixenseButtons.TWO) && current.transform.parent)
                {
                    current.GetComponent<Renderer>().material.color = color;
                    current = current.transform.parent.gameObject;
                }


                if (SixenseInput.Controllers [1].GetButtonUp (SixenseButtons.TRIGGER))
					grabbed = false;
			}

		}


	}
}
