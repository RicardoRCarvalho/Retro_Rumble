using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject trackingTarget;
    public float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Lerp(transform.position.x, trackingTarget.transform.position.x, Time.deltaTime * followSpeed);
        float y = Mathf.Lerp(transform.position.y, trackingTarget.transform.position.y, Time.deltaTime * followSpeed);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}