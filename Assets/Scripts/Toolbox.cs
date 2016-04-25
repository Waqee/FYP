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
        {
            GameObject imported = OBJLoader.LoadOBJFile("Assets/Models/7h15t5bvooe8-Aventador/Avent.obj");

            imported.transform.position = transform.position;

            foreach (Transform child in imported.transform)
            {
                child.gameObject.AddComponent<Splitable>();
                child.gameObject.AddComponent<MeshCollider>();
            }
            
            //imported.AddComponent<MeshFilter>();
            //MeshFilter[] meshFilters = imported.GetComponentsInChildren<MeshFilter>();
            //CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            //for (int i=0; i < meshFilters.Length; i++)
            //{
            //    if (meshFilters[i].sharedMesh == null)
            //        continue;
            //    combine[i].mesh = meshFilters[i].sharedMesh;
            //    combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //}
            //imported.transform.GetComponent<MeshFilter>().mesh = new Mesh();
            //imported.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            //imported.AddComponent<MeshCollider>();
        }
        //if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.ONE))
         //   Instantiate((Object)Grabber.current, transform.position, Quaternion.identity);
        if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.FOUR) && (Object)Grabber.current != null)
            Destroy((Object)Grabber.current);
        if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.START))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
