using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Toolbox : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.ONE))
            PhotonNetwork.Instantiate("SplitableCube", transform.position, Quaternion.identity, 0);
        if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.TWO))
            PhotonNetwork.Instantiate("SplitableSphere", transform.position, Quaternion.identity, 0);
        if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.THREE))
            PhotonNetwork.Instantiate("SplitableCylinder", transform.position, Quaternion.identity, 0);
        //if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.ONE))
         //   Instantiate((Object)Grabber.current, transform.position, Quaternion.identity);
        if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.TWO) && (Object)Grabber.current != null)
            Destroy((Object)Grabber.current);
        if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.START))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
