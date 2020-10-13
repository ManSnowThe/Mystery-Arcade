using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeY;
    // public float smoothTimeX;

    public GameObject cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        // float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, cam.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
