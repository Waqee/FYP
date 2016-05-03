using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class Toolbox : MonoBehaviour
{

    public GameObject floor;

    public GameObject left;

    public GameObject right;

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
            List<int> idsList = new List<int>();

            int objectID = PhotonNetwork.AllocateViewID();
            idsList.Add(objectID);
            GameObject imported = ImportObject(transform.position, objectID);


            foreach (Transform child in imported.transform)
            {
                //Generate ID and add to list. 
                int partID = PhotonNetwork.AllocateViewID();
                idsList.Add(partID);
                // Set up child
                SetUpChild(child, partID);
            }

            PhotonView photonView = this.GetComponent<PhotonView>();
            photonView.RPC("SpawnOnNetwork", PhotonTargets.OthersBuffered, transform.position, idsList.ToArray());

            //GameObject imported = OBJLoader.LoadOBJFile("Models/F-14A_Tomcat/F-14A_Tomcat.obj");

            //imported.transform.position = transform.position;
            //imported.transform.localScale = Vector3.one * 0.1f;

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
        // Object Copy (don't delete this code)
        //if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.ONE) && (Object)Grabber.current != null)
        //PhotonNetwork.Instantiate(Grabber.current.name, Grabber.current.transform.position, Grabber.current.transform.rotation, 0);
        if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.FOUR) && Grabber.current != null)
        {
            if (Grabber.current.tag == "imported")
            {
                PhotonView photonView = this.GetComponent<PhotonView>();
                photonView.RPC("Delete", PhotonTargets.AllBuffered, Grabber.current.name);
            }
            else
                PhotonNetwork.Destroy(Grabber.current);                
        }
        if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.START))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.FOUR))
        {
            floor.SetActive(false);
            left.SetActive(false);
            right.SetActive(false);

            OBJExporter exporter = new OBJExporter();
            exporter.Export("New Models/Model.obj");

            floor.SetActive(true);
            left.SetActive(true);
            right.SetActive(true);
        }
    }

    [PunRPC]
    void SpawnOnNetwork(Vector3 pos, int[] ids)
    {
        GameObject imported = ImportObject(pos, ids[0]);

        int i = 1;
        foreach (Transform child in imported.transform)
            SetUpChild(child, ids[i++]);
    }

    [PunRPC]
    void Delete(string name)
    {
        GameObject toDelete = GameObject.Find(name);
        if (toDelete != null)
            Destroy(toDelete);
    }

    // Imports game object.
    GameObject ImportObject(Vector3 position, int viewID)
    {
        GameObject imported = OBJLoader.LoadOBJFile("Models/F-14A_Tomcat/F-14A_Tomcat.obj");
        imported.transform.position = position;
        imported.transform.localScale = Vector3.one * 0.1f;

        // Name and tag the object
        imported.name = viewID.ToString();
        imported.tag = "imported";

        // Add the photon view and set it up.
        imported.AddComponent<PhotonView>();
        imported.AddComponent<OnClickRequestOwnership>();
        imported.GetComponent<PhotonView>().viewID = viewID;
        imported.GetComponent<PhotonView>().ObservedComponents = new List<Component>();
        imported.GetComponent<PhotonView>().ownershipTransfer = OwnershipOption.Takeover;
        imported.GetComponent<PhotonView>().ObservedComponents.Add(imported.transform);
        imported.GetComponent<PhotonView>().onSerializeTransformOption = OnSerializeTransform.All;
        imported.GetComponent<PhotonView>().synchronization = ViewSynchronization.UnreliableOnChange;

        return imported;
    }

    // Sets up child component of an imported game object.
    void SetUpChild(Transform child, int viewID)
    {
        // Add the splitable script and set it up.
        child.gameObject.AddComponent<Splitable>();
        child.gameObject.GetComponent<Splitable>().Convex = true;
        child.gameObject.GetComponent<Splitable>().UseCapUV = true;
        // Add the mesh collider.
        child.gameObject.AddComponent<MeshCollider>();
        // Add the ownership transfer script.
        child.gameObject.AddComponent<OnClickRequestOwnership>();
        // Name and tag the object
        child.gameObject.name = viewID.ToString();
        child.gameObject.tag = "imported";
        // Add the photon view and set it up.
        child.gameObject.AddComponent<PhotonView>();
        child.gameObject.AddComponent<OnClickRequestOwnership>();
        child.gameObject.GetComponent<PhotonView>().viewID = viewID;
        child.gameObject.GetComponent<PhotonView>().ObservedComponents = new List<Component>();
        child.gameObject.GetComponent<PhotonView>().ownershipTransfer = OwnershipOption.Takeover;
        child.gameObject.GetComponent<PhotonView>().ObservedComponents.Add(child.gameObject.transform);
        child.gameObject.GetComponent<PhotonView>().onSerializeTransformOption = OnSerializeTransform.All;
        child.gameObject.GetComponent<PhotonView>().synchronization = ViewSynchronization.UnreliableOnChange;
    }
}
