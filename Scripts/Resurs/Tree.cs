using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public enum Type
    {
        lumber = 0,
        aple = 1
    }
    private MainScripts Main;
    public Type typeOf = Type.lumber;   

	// Use this for initialization
	void Start ()
    {
        Main = GameObject.FindGameObjectWithTag("MainGo").GetComponent<MainScripts>();
		if (typeOf == Type.lumber)
        {
            if (Main.UpdateSelection(transform.position.x, transform.position.z))
                Main.Tree.Add(gameObject);
        }
        if (typeOf == Type.aple)
        {
            if (Main.UpdateSelection(transform.position.x, transform.position.z))
                Main.TreeA.Add(gameObject);
        }
    }	
	
}
