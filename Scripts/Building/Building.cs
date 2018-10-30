using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour,IHealth ,ISave {

    [SerializeField]
    protected int level; // Уровень здания
    [SerializeField]
    protected Renderer MeshBuild; // Меш здания
    [SerializeField]
    private Material Ghost; // Материал когда можно строить
    [SerializeField]
    private Material GhostNone;// Материал когда нельзя строить   
    [SerializeField]
    protected string BuildName; // Имя здания
    [SerializeField]
    protected string resurs;  // Тип ресурса
    [SerializeField]
    protected TypeBuild TypeBuild; // Тип здания

    [SerializeField]
    protected float HP; // Хп здания
    [SerializeField]
    protected float maxHP; // Максимальное хр здания

    [SerializeField]
    private Vector3[] CreatePositions; // позиции при прокрутки строения
    [SerializeField]
    private Transform CreateTransform; // трансформ объекта кручения
    [SerializeField] [Header("Базовое количество добываемого ресурса")]
    protected float BaseCount; // базовое количество добываемого ресурса

    [SerializeField]
    private Material[] Outmat; // Материал выделения
    [SerializeField]
    private Material[] mat; // Стандартный материал

    [SerializeField]
    protected GameObject createBuild; // Строительная площадка
    [SerializeField]
    protected GameObject mainBuild; //Здание которое строится

    protected int charCount; // количество человек       
    
    protected MainScripts Main; //Главный скрипт
    protected BuildMenu Menu; // Меню
    public GameObject target;  // Место куда приносить ресурс 
    protected List<Working> Humans = new List<Working>(); // Люди работающие в этом здании 
    private GameObject TriggerCol;  
    public bool OnBuild { get; private set; }
    protected int Index; // Индекс поворота объекта объекта
    bool notCollision; // Переменная отвечающая за коллизию объекта
    Vector3 CreateTarget;

	
	void Start ()
    {
        StartParametrs();        
    }

    private void Update()
    {
        if (gameObject.tag != "Create" && OnBuild == true)
        {
            Build(CreateTarget);
        }
    }

    public void Create() // Создать объект
    {        
        gameObject.tag = "Create";
        if (Mine()) 
        notCollision = true;
    }
   
    public void Creation(int index) // Крутить здание
    {
        CreateTransform.eulerAngles = new Vector3(0, CreateTransform.eulerAngles.y + 90.0f, 0);
        Index = index;
        CreateTransform.localPosition = CreatePositions[Index];        
    }

    protected void Compleate() // Поставить на выбраное место готовое здание
    {               
        gameObject.tag = "Complete";
        if (TriggerCol != null && Mine())
            Destroy(TriggerCol);
        Cost();
        Default();
        Main.Building.Add(this);
        OnBuild = true;
        CreateTarget = mainBuild.transform.localPosition;
        createBuild.SetActive(true);        
    }

    public void ReCreate() // Переместить построенное здание
    {        
        Create();
    }

    public void Default() // Стандартные настройки для здания
    {
        MeshBuild.materials = mat;
        Main.selectBuild = null;
    }  

    protected void Sel()
    {
        if (Main.selectBuild != null)
        Main.selectBuild.Default();
        Main.selectBuild = this;
        MeshBuild.materials = Outmat;        
        Menu.OpenBuild(BuildName, resurs, ((int)Count()).ToString(), level);
    }

    public bool CheckCreate(bool up) // Проверка на возможность строительства
    {       
        if (up == true && notCollision == false)
        {            
            MeshBuild.material = Ghost;
            return true;
        }
        else
        {
            MeshBuild.material = GhostNone;
            return false;
        }
    }

    protected void StartParametrs() // Параметры задаваемые при старте
    {
        Main = GameObject.FindGameObjectWithTag("MainGo").GetComponent<MainScripts>();
        Menu = GameObject.FindGameObjectWithTag("MainGo").GetComponent<BuildMenu>();
        HP = maxHP;
        if (gameObject.tag == "Complete" && Main != null)
        {
            Main.Building.Add(this);            
        }
    }

    public Vector3 CreatePosition(float x, float y, float z) // Позиция здания
    {
        RaycastHit _hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100.0f, LayerMask.GetMask("Stone")))
        {
            transform.eulerAngles = _hit.transform.eulerAngles;
            TriggerCol = _hit.collider.gameObject;
            return _hit.collider.GetComponent<Metall>().Target().position;
        }
        return new Vector3(x, y, z);
    }   

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "Create")
        {
            if (Mine() == false)
            {
                notCollision = true;
            }
            else
            {
                if (other.tag == "Stone")
                {
                    notCollision = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Create")
        {
            if (Mine() == false)
            {
                notCollision = false;                
            }
            else
            {
                notCollision = true;                
            }
        }
    }

    private void Build(Vector3 target)
    {
        mainBuild.transform.localPosition = Vector3.MoveTowards(mainBuild.transform.localPosition, target, 0.1f * Time.deltaTime);        
    }

    protected IEnumerator TimeBuild(float time)
    {
        yield return new WaitForSeconds(time);
        createBuild.SetActive(false);
        OnBuild = false;
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

    #region Virtual and Abstract methods

    public abstract bool CostResouces();

    public abstract void Creation();

    public abstract void Selected();

    public abstract void Cost();

    public abstract float Count();

    public abstract void Save();

    public abstract void Load();

    public virtual void AddHumans()
    {

    }

    public virtual void RemoveHumans()
    {

    }    

    public virtual bool Mine()
    {
        return false;
    }    

    #endregion

}
