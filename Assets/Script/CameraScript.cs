using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    private Transform hedef;
    // Use this for initialization
    void Start () {
        hedef = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = new Vector2(Mathf.Clamp(hedef.position.x, xMin, xMax), Mathf.Clamp(hedef.position.y, yMin, yMax));
	}
}
