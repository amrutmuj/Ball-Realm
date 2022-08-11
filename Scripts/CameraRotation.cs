using UnityEngine;
using System.Collections;
public class CameraRotation : MonoBehaviour
{

    private float x;
    private float y;
    private Vector3 rotateValue;


    void Update()
    {
        CamRotationScene();
    }

    public void CamRotationScene()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CamRotation")
        {
            Destroy(other.gameObject);

        }
      
    }
}