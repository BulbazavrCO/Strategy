using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Working : MonoBehaviour, IHealth {

    public enum TypeWork
    {
        Default = 0,
        Apples,
        Wood,
        Inelligence,
        Stone,
        Claymoremith
    }

    TypeWork TypeW = TypeWork.Default;

    [Header("Состояния работы")]
    public List<AllState> AllStatesc = new List<AllState>(); // Состояния работы

    private Animator anim; // Аниматор
    private NavMeshAgent agent; // Навмеш
    public bool IsWork { get; set; }   // Состояние работы
    public bool Full { get; set; } 
    [Header("Все предметы")]
    public GameObject[] AllItems;
    public GameObject hair;
    public GameObject boroda;
    public Material[] ColorHair;
    private int IntHair;
    [SerializeField]
    private GameObject target; // Где добывать
    [SerializeField]
    private GameObject targetBase; // Куда носить    
    private MainScripts Main;   
    public GameObject veshi;
    public Material DefaultVeshi;
    [SerializeField]
    private float HP;
    [SerializeField]
    private float maxHP;

	void Start ()
    {
        Main = GameObject.FindGameObjectWithTag("MainGo").GetComponent<MainScripts>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Main.Char.Add(this);
        IntHair = Random.Range(0, 3);        
        hair.GetComponent<Renderer>().material = ColorHair[IntHair];
        boroda.GetComponent<Renderer>().material = ColorHair[IntHair];       
    }    
	void Update ()
    {
	switch (TypeW)
        {
            case TypeWork.Default:
                Idle();
                break;
            case TypeWork.Apples:
                Apple();
                break;
            case TypeWork.Wood:
                Wood();
                break;
            case TypeWork.Inelligence:
                Intelligence();
                break;
            case TypeWork.Stone:
                Stone();
                break;
            case TypeWork.Claymoremith:
                Claymore();
                break;
        }
	}   

    public void Work(GameObject _target,TypeWork type) // Начать работу
    {
        IsWork = true;
        TypeW = type;
        targetBase = _target;
    }

    public void DontWork() // Отменить работу
    {
        TypeW = TypeWork.Default;
        IsWork = false;
        if (target.GetComponent<Closed>() != null)
            target.GetComponent<Closed>().closed = false;
        Full = false;
        target = null;
        DefaultItems();
    }

    private void Idle() // Спокойствие
    {

    }

    private void Claymore()
    {

    }

    private void Apple() // Добыча яблок
    {
        if (target == null)
        {
            SetTarget(1, Main.TreeA);
        }
        else
        {            
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (!Full)
            {
                if (distance <= 3.0f)
                {
                    StartCoroutine(Sbor(4.0f,2,4,2));                    
                }
            }
            else
            {
                if (target == targetBase && distance <= 3.0f)
                {
                    target = null;
                    Full = false;
                }
            }
        }
    }

    private void Stone() // Добыча камня
    {        
        if (target == null)
        {
            SetTarget(3, Main.Metall);
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (!Full)
            {
                if (distance <= 3.0f)
                {
                    StartCoroutine(Sbor(4.0f, 4, 3, 5));
                }
            }
            else
            {
                if (target == targetBase && distance <= 3.0f)
                {
                    target = null;
                    Full = false;
                }
            }
        }
    }

    private void Wood() // Добыча дерева
    {
        if (target == null)
        {
            SetTarget(3, Main.Tree);
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (!Full)
            {
                if (distance <= 3.0f)
                {                    
                    StartCoroutine(Sbor(4.0f, 4, 3,5));                    
                }
            }
            else
            {
                if (target == targetBase && distance <= 3.0f)
                {
                    target = null;
                    Full = false;
                }
            }
        }
    }

    private void Intelligence() // Разведка
    {
        if (target == null)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (var go in Main.Zone)
            {
                list.Add(go.gameObject);
            }           
            SetTarget(6, list);
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (!Full)
            {
                if (distance <= 10.0f)
                {
                    StartCoroutine(Sbor(4.0f, 6, 5, 6));                    
                }
            }
            else
            {
                if (target == targetBase && distance <= 3.0f)
                {
                    target = null;
                    Full = false;
                }
            }
        }
    }

    private void DefaultItems() // Стандартое состояние
    {
        for (int i = 0; i < AllItems.Length; i++)
        {
            AllItems[i].SetActive(false);
        }
        veshi.GetComponent<Renderer>().material = DefaultVeshi;
    }

    private void NewItems(int i)
    {
        DefaultItems();
        foreach (var items in AllStatesc[i].CurentState)
        {
            items.SetActive(true);
        }
    }    
    private IEnumerator Sbor(float time, int indexItem1, int indexAnim, int indexItem2)
    {      
        agent.isStopped = true;
        anim.SetInteger("Per", indexAnim);
        Full = true;        
        transform.LookAt(target.transform.transform.position);
        NewItems(indexItem1);
        veshi.GetComponent<Renderer>().material = AllStatesc[indexItem1].mat;
        yield return new WaitForSeconds(time);
        NewItems(indexItem2);
        target.GetComponent<Closed>().closed = false;
        target = targetBase;
        anim.SetInteger("Per", 2);
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        veshi.GetComponent<Renderer>().material = AllStatesc[indexItem2].mat;
    } // Тайминг работы

    private void SetTarget(int v, List<GameObject> massN) // Определение таргета
    {
        List<GameObject> mass = new List<GameObject>();
        foreach (var g in massN)
        {
            if (g.GetComponent<Closed>().closed == false)
                mass.Add(g);
        }        
        GameObject go = mass[0];
        float min = Vector3.Distance(transform.position, go.transform.position);
        for (int i = 0; i < mass.Count; i++)
        {
            go = mass[i];                            
            if (Vector3.Distance(transform.position, go.transform.position) <= min)
            {               
                min = Vector3.Distance(transform.position, go.transform.position);
                target = go;                              
            }                                      
        }        
        if (target != null)
        {            
            target.GetComponent<Closed>().closed = true;
            anim.SetInteger("Per", 2);
            agent.SetDestination(target.transform.position);
            DefaultItems();
            foreach (var items in AllStatesc[v].CurentState)
            {
                items.SetActive(true);
                veshi.GetComponent<Renderer>().material = AllStatesc[v].mat;                
            }
        }
    }
    public void SetHeath(float value)
    {
        HP -= value;
    }

    public int GetHealth()
    {
        return HP < 0 ? 0 : (int)HP;
    }

    public int GetMaxHealth()
    {
        return (int)maxHP;
    }
}

#region AllStates
[System.Serializable]
public class AllState
{
    public string nameState;
    public List<GameObject> CurentState = new List<GameObject>();
    public Material mat;
}
#endregion
