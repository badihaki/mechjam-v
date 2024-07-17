using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCam : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    private void LateUpdate()
    {
        // transform.LookAt(Vector3.Scale(Camera.main.transform.position, transform.forward));
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
