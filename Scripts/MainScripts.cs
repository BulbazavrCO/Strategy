using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScripts : MonoBehaviour {
    
    [SerializeField]
    private List<int> checkX = new List<int>();
    [SerializeField]
    private List<int> checkY = new List<int>();
    private InZone selectZone;

    public static bool create;      
    public List<GameObject> Prefab  = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> Tree = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> TreeA = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> Metall = new List<GameObject>();
    [HideInInspector]
    public List<Building> Building = new List<Building>();
   
    public List<Working> Char = new List<Working>();
        
    public Building selectBuild { get; set; }
    private Building _target;    
    private float timeCreate = 0;   
    private BuildMenu Menu;
    private int posT = 0;

    [Header("Создание зон")]
    [SerializeField]
    private GameObject mapZone;
    public InZone[,] zones { get; set; }
    [HideInInspector]
    public List<InZone> Zone = new List<InZone>();

    // Use this for initialization
    void Start ()
    {        
        create = false;        
        Menu = GetComponent<BuildMenu>();
        CreateMap();              
    }
	
	// Update is called once per frame
	void Update ()
    {        
		if (create) // Создать здание
        {                     
            RaycastHit _hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100.0f, LayerMask.GetMask("Plane")))
            {                
                _target.transform.position = _target.CreatePosition((int)(_hit.point.x / 10) * 10 + 5, _hit.point.y, (int)(_hit.point.z / 10) * 10 - 5);                
                if (_target.CheckCreate(UpdateSelection(_hit.point.x,_hit.point.z)) == false)
                {                    
                    timeCreate = 0;                    
                }
                else
                {                    
                    if (_target.Mine() == false)
                    {
                        timeCreate += Time.deltaTime;
                        if (timeCreate >= 2)
                        {
                            posT = (posT + 1 > 3) ? 0 : posT + 1;
                            _target.Creation(posT);
                            timeCreate = 0;
                        }
                    }
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        _target.Creation();
                        _target = null;
                        create = false;
                    }
                   
                }
            }
        }
        else // Выделить здание на которое нажили
        {           
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit _hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit) && UpdateSelection(_hit.point.x,_hit.point.z))
                {                   
                    if (_hit.collider.tag == "Complete" && Menu.On == false && _hit.collider.GetComponent<Building>().OnBuild == false)
                    {
                        _hit.collider.GetComponent<Building>().Selected();
                    }
                    else
                    {
                        if (selectBuild != null && Menu.On == false)
                        {                           
                            selectBuild.Default();
                        }
                    }
                }
            }
        }     
        
	}

    void CreateMap() // Создать зоны карты
    {
        zones = new InZone[40, 40];
        for (int i = -10; i < 10; i++)
        {
            for(int j = -10; j < 10; j++)
            {
                InZone go = Instantiate(mapZone).GetComponent<InZone>();
                go.ZonPosition(i, j, transform, checkX, checkY);
                Zone.Add(go);
                zones[i + 10, j + 10] = go;
            }
        }
    }

    public bool UpdateSelection(float x, float z) // Проверить находится ли объект в купленой зоне здание
    {        
        if (zones[(int)((500 + x) / 50), (int)((500 + z) / 50)].Buy)
        {           
            return true;
        }
        return false;
    }

    public void CreateGO (GameObject gg) // Создать здание
    {
        if (gg.GetComponent<Building>().CostResouces())
        {
            posT = 0;
            create = true;
            _target = Instantiate(gg, Vector3.zero, Quaternion.identity).GetComponent<Building>();            
            _target.Create();
            Menu.BuildInterface();            
        }
        
    }       

    public void OpenBuildMenu(GameObject go) // Открыть меню здания
    {
        Menu.On = false;
        if (selectBuild != null)
        {
            selectBuild.Default();
        }
        go.SetActive(!go.activeSelf);
    }   
   
}
