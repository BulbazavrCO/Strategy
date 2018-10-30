using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour {

    [SerializeField]
    private Mesh[] deskMesh;
	void Start ()
    {
        int rand = Random.Range(0, deskMesh.Length);
        GetComponent<MeshFilter>().mesh = deskMesh[rand];
	}
	
	
}
