using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 15.0f;
    private float padding = 0.5f;

    float xmin;
    float xmax;
    float ymin;
    float ymax;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightTopMost = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance));
        xmin = leftBottomMost.x + padding;
        xmax = rightTopMost.x - padding;
        ymin = leftBottomMost.y + padding;
        ymax = rightTopMost.y - padding;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);f
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        
        // restrict the player to the game space
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
        transform.position = new Vector3(newX, newY, transform.position.z);
	}
}
