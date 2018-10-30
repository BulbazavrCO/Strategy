using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : Building
{
    [SerializeField]
    private float stone;
    [SerializeField]
    private float lumber;
    [SerializeField]
    private float money;


    public override void Cost()
    {
        Resurs.res.Money -= money;
        Resurs.res.Lumber -= lumber;
        Resurs.res.Stone -= stone;
    }

    public override bool CostResouces()
    {
        return (Resurs.res.Money >= money && Resurs.res.Lumber >= lumber && Resurs.res.Stone >= stone);
    }

    public override float Count()
    {
        return BaseCount;
    }

    public override void Creation()
    {
        Compleate();
        mainBuild.transform.localPosition = new Vector3(mainBuild.transform.localPosition.x, -6.0f, mainBuild.transform.localPosition.z);
        StartCoroutine(TimeBuild(60.0f));
    }

    public override void Load()
    {
        throw new System.NotImplementedException();
    }

    public override void Save()
    {
        throw new System.NotImplementedException();
    }

    public override void Selected()
    {
        Sel();
        Menu.OpenBarn(charCount, TypeBuild);
    }
   
}
