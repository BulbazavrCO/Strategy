using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour {


    [Header("Интерфейсы отдельных строений")]
    public List<BuildOfInterface> Inter = new List<BuildOfInterface>();

    [Header("Обычные строения")]
    public Text Build_name;
    public GameObject Build;
    public Text _Resurs;
    public Text CountRes;
    private int level;

    [SerializeField]
    private GameObject build_Interface;
    public GameObject[] Humans;
    public GameObject[] AllInteface;
    public GameObject[] AllBuildInterface;
    private MainScripts Main;
    [SerializeField] [Header("Налог")]
    private Text _nalog;
    private bool on = false;

    public bool On // Переменная отвечающая за наведение на интерфейс
    {
        get
        {
            return on;
        }
        set
        {
            on = value;
        }
    }

    private void Start()
    {
        Main = GetComponent<MainScripts>();        
    }
   

    public void OpenBuild(string _name, string _resurs, string _countRes, int _level)
    {
        Build.SetActive(true);
        Build_name.text = _name;
        _Resurs.text = _resurs;
        CountRes.text = _countRes + "/час";
        Default();
    }   
    

    public void OpenBarn(int humans, TypeBuild type)
    {
        foreach (var i in Inter[(int)type].Interface)
        {
            i.SetActive(true);
        }
        ActiveHumans(humans);
    }

    private void OpenHome()
    {
        
    }

    public void OPenCastle(float value)
    {
        Resurs.res.Nalog = (int)value;
        Resurs.res.Happines();
        CountRes.text = ((int)Main.selectBuild.Count()).ToString() + "/час";        
        _nalog.text = (value + 1).ToString();        
    }

    private void OpenMainBuild()
    {

    }

    private void OpenIntelligence()
    {

    }

    private void OpenWood()
    {
        
    }

    private void ActiveHumans(int h)
    {
        if (h <= 3)
        {
            for (int i = 0; i < h; i++)
            {
                Humans[i].SetActive(true);
            }
        }
        else
        {
            Humans[3].SetActive(true);
            Humans[4].SetActive(true);
            Humans[4].GetComponent<Text>().text = "x" + h.ToString();
        }
    }

    private void Default()
    {
        for (int i =0; i < AllInteface.Length; i++)
        {
            AllInteface[i].SetActive(false);
        }
    }

    public void AddWorkers()
    {      
        Main.selectBuild.AddHumans();
        Main.selectBuild.Selected();
    }

    public void RemoveWorkers()
    {
        Main.selectBuild.RemoveHumans();
        Main.selectBuild.Selected();
    }

    public void SwapInterface(GameObject go)
    {
        for (int i =0; i < AllBuildInterface.Length; i++)
        {
            AllBuildInterface[i].SetActive(false);
        }
        go.SetActive(true);
    }

    public void BuildInterface()
    {
        On = false;
        build_Interface.SetActive(false);
    }
}
