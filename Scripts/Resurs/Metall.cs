using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metall : MonoBehaviour {

    public enum Type
    {
        Default,
        Stone,
        Gold
    }
    public Type tupeOF = Type.Default;

    private MainScripts Main;
    [SerializeField]
    private Transform target;

    private void Start()
    {
        Main = GameObject.FindGameObjectWithTag("MainGo").GetComponent<MainScripts>();
        if (tupeOF != Type.Default) 
        Main.Metall.Add(this.gameObject);
    }

    public Transform Target()
    {
        return target;
    }
}
