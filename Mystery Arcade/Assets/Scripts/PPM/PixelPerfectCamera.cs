using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {
    Vector2 position;
    private void Awake()
    {
        GetComponent<Camera>().orthographicSize = Screen.height / 2;
        position = transform.position;
        transform.position = new Vector3(position.x - .1f, position.y - .1f, -10);
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(position.x - .1f, position.y - .1f, -10);
    }
}
