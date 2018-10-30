using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBuild : Building, ISpawn
{
    [SerializeField]
    private GameObject Human;
    private float nalog;
    private const float timeSpawn = 30;
    private float NextTime;
    private float KoefTime;
    [SerializeField]
    private GameObject spawnObj;


    public float TimeSpawn
    {
        get
        {
            float newKoefTime;
            float lastkoefTime;
            newKoefTime = timeSpawn * (Main.Char.Count + 1) / Resurs.res.Happines();
            lastkoefTime = KoefTime;
            if (newKoefTime == lastkoefTime)
            {
                return NextTime;
            }
            else
            {
                
                KoefTime = newKoefTime;                
                return newKoefTime * (KoefTime / NextTime);
            }
        }
        set
        {
            NextTime = value;
        }
    }

    private void Start()
    {
        StartParametrs();
        StartCoroutine(PlusCount());
        Count();
        TimeSpawn = timeSpawn * (Main.Char.Count + 1) / Resurs.res.Happines();        
    }

    public override void Selected() // Выделить здание
    {
        Sel();
        Menu.OpenBarn(charCount, TypeBuild);
    }

    public override float Count() // Количество добываемого ресурса
    {
        charCount = Main.Char.Count;
        return BaseCount * charCount * (Resurs.res.Nalog + 1);
    }

    private IEnumerator PlusCount()
    {
        yield return new WaitForSeconds(1.0f);
        Resurs.res.Money += Count() / 3600;
        StartCoroutine(PlusCount());
    }

    public override bool CostResouces()
    {
        throw new System.NotImplementedException();
    }

    public override void Creation()
    {
        throw new System.NotImplementedException();
    }

    public override void Cost()
    {
        throw new System.NotImplementedException();
    }

    public void Spawn()
    {

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
