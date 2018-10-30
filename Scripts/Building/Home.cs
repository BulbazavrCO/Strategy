using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Building {

    [SerializeField]
    [Header("Стоимость монет")]
    private int money;
    

    public override void Creation()
    {             
        Compleate();
        mainBuild.transform.localPosition = new Vector3(mainBuild.transform.localPosition.x, -6.0f, mainBuild.transform.localPosition.z);
        StartCoroutine(TimeBuild(60.0f));
    }

    public override void Cost()
    {
        Resurs.res.Money -= money;        
    }

    public override void Selected()
    {
        
    }

    public override bool CostResouces()
    {
        return (Resurs.res.Money >= money);
    }

    public override float Count()
    {
        return BaseCount;
    }

    public override void Save()
    {
        throw new System.NotImplementedException();
    }

    public override void Load()
    {
        throw new System.NotImplementedException();
    }
}
