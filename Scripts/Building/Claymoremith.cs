using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claymoremith : Building {

    [SerializeField]
    [Header("Стоимость дерева")]
    private int lumber = 40;
    [SerializeField]
    private BoxCollider col;
    [SerializeField]
    Vector3[] ColliderPos;
    [SerializeField]
    Vector3[] ColliderSize;

    public override void Creation()
    {
        Compleate();
        GetComponent<BoxCollider>().center = ColliderPos[Index];
        GetComponent<BoxCollider>().size = ColliderSize[Index];
        GetComponent<BoxCollider>().enabled = true;
        Destroy(col);
        mainBuild.transform.localPosition = new Vector3(mainBuild.transform.localPosition.x, -6.0f, mainBuild.transform.localPosition.z);
        StartCoroutine(TimeBuild(60.0f));
    }

    public override void Cost() // Определение цены
    {
        Resurs.res.Lumber -= lumber;
    }

    public override void Selected() // Выделить здание
    {
        Sel();
        Menu.OpenBarn(charCount, TypeBuild);
    }

    public override float Count()
    {
        return BaseCount * charCount;
    }


    public override bool CostResouces()
    {
        return (Resurs.res.Lumber >= lumber);
    }

    public override void AddHumans()
    {
        if (charCount < 3)
        {
            foreach (var Worker in Main.Char)
            {
                if (Worker.IsWork == false)
                {
                    foreach (var metal in Main.Metall)
                    {
                        if (metal.GetComponent<Closed>().closed == false)
                        {
                            Worker.Work(target, Working.TypeWork.Claymoremith);
                            Humans.Add(Worker);
                            charCount++;
                            if (charCount == 1)
                                StartCoroutine(PlusCount());
                            break;

                        }
                    }
                    break;
                }

            }
        }        
    }

    public override void RemoveHumans()
    {
        if (charCount > 0)
        {
            charCount--;
            Humans[Humans.Count - 1].DontWork();
            Humans.Remove(Humans[Humans.Count - 1]);
            if (charCount == 0)
                StopCoroutine(PlusCount());
        }
    }

    private IEnumerator PlusCount()
    {
        yield return new WaitForSeconds(1.0f);
        Resurs.res.Stone += Count() / 3600;
        StartCoroutine(PlusCount());
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
