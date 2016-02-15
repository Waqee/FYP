using UnityEngine;
using System.Collections;

public class Toolbox : MonoBehaviour {

	public GameObject Cube;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (SixenseInput.Controllers [1].GetButtonDown (SixenseButtons.ONE))
			Instantiate (Cube,transform.position,Quaternion.identity);
	}
}
