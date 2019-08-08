using UnityEngine;

public class SuctionManager : MonoBehaviour
{
    public GameObject raycastObject;
    public RaycastHit objectHit;

    public int edgeHit;
    public int centralHit;

    private Vector3[] edgePoints = new Vector3[8];
    public MeshCollider planeMesh;
    Renderer suctionMat;
    Material defaultMat;

    public Material highlightMat;

    float radius;
    float biasFactor;

    private void Start()
    {
        radius = raycastObject.transform.localScale.z / 2.0f;
        biasFactor = 0.7f * radius;
        
        suctionMat = raycastObject.GetComponent<Renderer>();
        defaultMat = suctionMat.material;
    }



    public void CheckForHit()
    {
        Vector3 currentPosition;
        int i;

        currentPosition = raycastObject.transform.position;

        Vector3 vectorModifier(float x, float z)
        {
            Vector3 moddedVector;

            moddedVector = new Vector3(currentPosition.x + x, currentPosition.y, currentPosition.z + z);

            return moddedVector;
        }

        Vector3 downWard = raycastObject.transform.TransformDirection(Vector3.down);

        // Edgepoint Modifier

        edgePoints[0] = vectorModifier(radius, 0f);
        edgePoints[1] = vectorModifier(biasFactor, biasFactor);
        edgePoints[2] = vectorModifier(0, radius);
        edgePoints[3] = vectorModifier(-biasFactor, biasFactor);
        edgePoints[4] = vectorModifier(-radius, 0);
        edgePoints[5] = vectorModifier(-biasFactor, -biasFactor);
        edgePoints[6] = vectorModifier(0, -radius);
        edgePoints[7] = vectorModifier(biasFactor, -biasFactor);
      

        // DrawRay

        int frameCheckpoint = 0;
        edgeHit = 1;
            
        for (i = 0; i < 8; i++)
        {
            //Debug.DrawRay(edgePoints[i], downWard * 5, Color.blue);

            if (Physics.Raycast(edgePoints[i], downWard, out objectHit, 50))
            {
                if (objectHit.collider == planeMesh)
                {
                    edgeHit = 0;
                }
            }
        }

        centralHit = 0;

        //Debug.DrawRay(raycastObject.transform.position, downWard * 5, Color.red);

        if (Physics.Raycast(currentPosition, downWard, out objectHit, 50))
        {
            if (objectHit.collider == planeMesh)
            {
                centralHit = 1;
            }

            else
            {
                centralHit = 0;
            }
        }

        if (edgeHit == 1 && centralHit == 1)
        {
            frameCheckpoint = 1;
        }

        if (frameCheckpoint == 1)
        {
            Debug.Log("Holographic Object Found with NG");
        }

        if (edgeHit == 1 & centralHit == 0)
        {
            // Do Ya Thang
            if (suctionMat != null)
            {
                suctionMat.material = highlightMat;
            }
        }

        else
        {
            suctionMat.material = defaultMat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHit();
    }
}
