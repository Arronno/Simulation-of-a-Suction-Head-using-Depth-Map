using OpenCvSharp;
using UnityEngine;

public class OpenCVProtocol : MonoBehaviour
{
    Texture2D baseTexture;
    //public GameObject source;
    public RenderTexture sCam;
    [HideInInspector]
    public bool suctionCheck;
    //double[] myArray;

    //Mat currentImage;
    Mat srcMat;

    // Start is called before the first frame update
    void Start()
    {
        baseTexture = new Texture2D(sCam.width, sCam.height, TextureFormat.RGBA32, false, false);
        suctionCheck = false;
        //currentImage = OpenCvSharp.Unity.TextureToMat(baseTexture);
        //currentImage = new Mat();
        //Debug.Log(currentImage);
    }

    // Update is called once per frame
    void Update()
    {
        RenderTexture.active = sCam;
        baseTexture.ReadPixels(new UnityEngine.Rect(0, 0, baseTexture.width, baseTexture.height), 0, 0);
        baseTexture.Apply();

        srcMat = new Mat(baseTexture.height, baseTexture.width, MatType.CV_8UC3, baseTexture.GetRawTextureData());
        //currentImage = OpenCvSharp.Unity.TextureToMat(baseTexture);
        //Debug.Log(baseTexture);
        //currentImage.GetArray(8191, 3839, myArray);
        //Debug.Log(myArray);
        //double s = OpenCvSharp.Unity.(currentImage)[0];
        Scalar csum = srcMat.Sum();

        double finalSum = 0;

        for(int i=0; i<3; i++)
        {
            finalSum += csum[i];
        }

        if (finalSum < 10900000000)
        {
            suctionCheck = true;
        }
        else
        {
            suctionCheck = false;
        }

        Debug.Log(suctionCheck);
        /*
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log(finalSum);
        }
        */
    }
}
