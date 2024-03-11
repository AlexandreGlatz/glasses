using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup2 : MonoBehaviour
{
    public Camera cameraB;
    public Camera cameraA;

    public Material cameraMatB;
    public Material cameraMatA;

    public Camera cameraB1;
    public Camera cameraA1;

    public Material cameraMatB1;
    public Material cameraMatA1;

    public Camera cameraB2;
    public Camera cameraA2;

    public Material cameraMatB2;
    public Material cameraMatA2;

    public Camera cameraB3;
    public Camera cameraA3;

    public Material cameraMatB3;
    public Material cameraMatA3;

    public Camera cameraB4;
    public Camera cameraA4;

    public Material cameraMatB4;
    public Material cameraMatA4;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;

        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }
        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = cameraA.targetTexture;

        //

        if (cameraB1.targetTexture != null)
        {
            cameraB1.targetTexture.Release();
        }
        cameraB1.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB1.mainTexture = cameraB1.targetTexture;

        if (cameraA1.targetTexture != null)
        {
            cameraA1.targetTexture.Release();
        }
        cameraA1.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA1.mainTexture = cameraA1.targetTexture;

        //

        if (cameraB2.targetTexture != null)
        {
            cameraB2.targetTexture.Release();
        }
        cameraB2.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB2.mainTexture = cameraB2.targetTexture;

        if (cameraA2.targetTexture != null)
        {
            cameraA2.targetTexture.Release();
        }
        cameraA2.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA2.mainTexture = cameraA2.targetTexture;

        //

        if (cameraB3.targetTexture != null)
        {
            cameraB3.targetTexture.Release();
        }
        cameraB3.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB3.mainTexture = cameraB3.targetTexture;

        if (cameraA3.targetTexture != null)
        {
            cameraA3.targetTexture.Release();
        }
        cameraA3.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA3.mainTexture = cameraA3.targetTexture;

        //

        if (cameraB4.targetTexture != null)
        {
            cameraB4.targetTexture.Release();
        }
        cameraB4.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB4.mainTexture = cameraB4.targetTexture;

        if (cameraA4.targetTexture != null)
        {
            cameraA4.targetTexture.Release();
        }
        cameraA4.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA4.mainTexture = cameraA4.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
