using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InZone : MonoBehaviour {

    public List<GameObject> Tree = new List<GameObject>();
    public List<GameObject> TreeA = new List<GameObject>();
    private MainScripts main;
    public int X { get; set; }
    public int Z { get; set; } 
    public bool Buy { get; set; }

    private void Start()
    {
              
    }    

    public void ZonPosition(int x, int z, Transform pos,List<int> checkX, List<int> checkZ) // Назначить вектор положения зонам
    {
        float i, j;
        X = x + 10;
        Z = z + 10;
        i = 25 + 50*x;
        j = 25 + 50*z;       
        transform.position = new Vector3(i, 0, j);
        main = pos.GetComponent<MainScripts>();
        gameObject.name = "(" + X.ToString() + " | " + Z.ToString() + ")"; 
        for (int f = 0; f< checkX.Count; f++)
        {
            if (X == checkX[f] && Z == checkZ[f])
            {
                Buy = true;
                break;
            }
        }
        transform.SetParent(pos);
    }
    

    
}
