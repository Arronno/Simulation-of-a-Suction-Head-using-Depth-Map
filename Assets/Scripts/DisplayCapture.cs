using UnityEngine;

public class DisplayCapture : MonoBehaviour
{
    // Grab the camera's view when this variable is true.
    bool grab;

    // The "m_Display" is the GameObject whose Texture will be set to the captured image.
    public Renderer m_Display;

    private void Update()
    {
        //Press space to start the screen grab
        if (Input.GetKeyDown(KeyCode.X))
            grab = true;
    }

    private void OnPostRender()
    {
        if (grab)
        {
            //Create a new texture with the width and height of the screen
            Texture2D texture = new Texture2D(Display.displays[0].renderingWidth, Display.displays[0].renderingHeight, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0, 0, Display.displays[0].renderingWidth, Display.displays[0].renderingHeight), 0, 0, false);
            texture.Apply();
            //Check that the display field has been assigned in the Inspector
            if (m_Display != null)
                //Give your GameObject with the renderer this texture
                m_Display.material.mainTexture = texture;
            //Reset the grab state
            grab = false;
        }
    }
}
