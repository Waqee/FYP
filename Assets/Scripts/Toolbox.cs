using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Toolbox : MonoBehaviour {

	public GameObject Cube;
	public GameObject Sphere;
	public GameObject Cylinder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (SixenseInput.Controllers [0].GetButtonDown (SixenseButtons.ONE))
			Instantiate (Cube,transform.position,Quaternion.identity);
		if (SixenseInput.Controllers [0].GetButtonDown (SixenseButtons.TWO))
			Instantiate (Sphere,transform.position,Quaternion.identity);
		if (SixenseInput.Controllers [0].GetButtonDown (SixenseButtons.THREE))
			Instantiate (Cylinder,transform.position,Quaternion.identity);
		if (SixenseInput.Controllers [1].GetButtonDown (SixenseButtons.ONE))
			Instantiate ((Object)Grabber.current,transform.position,Quaternion.identity);
		if (SixenseInput.Controllers [1].GetButtonDown (SixenseButtons.TWO))
			Destroy ((Object)Grabber.current);
		if(SixenseInput.Controllers [0].GetButtonDown (SixenseButtons.START))
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);		
	}
}
