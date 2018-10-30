using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Woodcutter : Building {

    [SerializeField]
    [Header("Стоимость дерева")]
    private int lumber;
    [SerializeField]
    [Header("Стоимость камня")]
    private int stone;
    

    public override void Creation()
    {
        Compleate();
        mainBuild.transform.localPosition = new Vector3(mainBuild.transform.localPosition.x, -6.0f, mainBuild.transform.localPosition.z);
        StartCoroutine(TimeBuild(60.0f));
    }

    public override void Cost()
    {
        Resurs.res.Stone -= stone;
        Resurs.res.Lumber -= lumber;        
    }

    public override void Selected()
    {
        Sel();
        Menu.OpenBarn(charCount, TypeBuild);
    }

    public override float Count()
    {
        return BaseCount * charCount;
    }
    
    public override void AddHumans()
    {
        if (charCount < 3)
        {
            foreach (var Worker in Main.Char)
            {
                if (Worker.IsWork == false)
                {
                    foreach (var wood in Main.Tree)
                    {
                        if (wood.GetComponent<Closed>().closed == false)
                        {
                            Worker.Work(target, Working.TypeWork.Wood);
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

    public override bool CostResouces()
    {
        return (Resurs.res.Stone >= stone && Resurs.res.Lumber >= lumber);
    }    

    private IEnumerator PlusCount()
    {
        yield return new WaitForSeconds(1.0f);
        Resurs.res.Lumber += Count() / 3600;
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
