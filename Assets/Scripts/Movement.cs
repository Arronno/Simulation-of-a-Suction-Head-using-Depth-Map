using UnityEngine;

public class Movement : MonoBehaviour
{
    SuctionManager handler;
    public int edgeHit, centralHit;
    Color highlightMat;
    Color currentMat;
    OpenCVProtocol ocp;

    public GameObject ring;
    //GameObject objectHit;
    //GameObject suctionHead;

    // Start is called before the first frame update
    void Start()
    {
        handler = FindObjectOfType<SuctionManager>();
        edgeHit = handler.edgeHit;
        centralHit = handler.centralHit;
        highlightMat = handler.highlightMat.color;
        ocp = FindObjectOfType<OpenCVProtocol>();
        //objectHit = handler.objectHit.transform.gameObject;
        //suctionHead = handler.raycastObject;
    }

    void Parent(GameObject suctionHead, GameObject objectHit)
    {
        objectHit.transform.parent = suctionHead.transform;
    }

    void ReleaseChild(GameObject suctionHead, GameObject objectHit)
    {
        objectHit.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        float multiplier = 0.1f;
        currentMat = GetComponent<Renderer>().material.color;

        if (ring.transform.position.y <= 0.08 & ring.transform.parent == null)
        {
            ring.gameObject.GetComponent<Rigidbody>().useGravity = false;
            ring.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ring.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ring.transform.position = new Vector3(ring.transform.position.x, 0.05f, ring.transform.position.z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.up * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(- transform.up * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.forward * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(- transform.forward * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(transform.right * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(- transform.right * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            ReleaseChild(gameObject, ring);
            //gameObject.GetComponent<FixedJoint>().connectedBody = null;
            ring.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (transform.position.y < 0.55f & Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x, 0.53f, transform.position.z);
        }

        /*
        if (centralHit == 0 & edgeHit == 1)
        {
            Parent(gameObject, objectHit);
        }
        */
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Ring"))
        {
            /*
            if (currentMat == highlightMat & Input.GetKey(KeyCode.Space))
            {
                Parent(gameObject, ring);
                Debug.Log("Hooked");
            }
            */

            if (ocp.suctionCheck & Input.GetKey(KeyCode.Space))
            {
                Parent(gameObject, ring);
                Debug.Log("Hooked");
                //gameObject.AddComponent<FixedJoint>().connectedBody = ring.gameObject.GetComponent<Rigidbody>();
                //gameObject.AddComponent<FixedJoint>().breakForce = 15.0f;
            }
            
            //Debug.Log(other.gameObject);
            //Debug.Log(centralHit);
            //Debug.Log(edgeHit);
        }
    }  
}
