using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour {


    public float speed = 30;    
    public float yMin = 10;
    public float yMax = 20;    
    public float speedRotation;
    public float scrol;   
    public static bool OnInterface = false;

    // Use this for initialization
    void Start ()
    {
             
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (OnInterface == false)
        {            
            if (Input.GetKey(KeyCode.Mouse0))
            {
                float trans = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
                float trans2 = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
                transform.Translate(-trans, 0, 0);
                transform.Translate(0, 0, -trans2);

            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                float trans = Input.GetAxis("Mouse X") * speedRotation * Time.deltaTime;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + trans, transform.eulerAngles.z * 0);
            }


            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (transform.position.y < yMax)
                {
                    transform.Translate(0, scrol * Time.deltaTime, 0);
                }
            }
            else
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (transform.position.y > yMin)
                {
                    transform.Translate(0, -scrol * Time.deltaTime, 0);
                }
            }
        }
    }
}
