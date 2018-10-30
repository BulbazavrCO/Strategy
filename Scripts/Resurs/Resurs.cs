using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeResource
{
    Boards = 0
}

public enum TypeFood
{    
    Bread = 0
}

public class Resurs : MonoBehaviour
{

    private float happiness = 1f; // Общее счастье
    private float nalogHap = 1f; // Процент счастья от налогов
    private float foodHap = 1f; // Процент счастья от еды
    private float peopleHap = 1f; // Процент счастья от населения    
    public static int MaxNaselenie = 10; // Максимальное население    
    private int naselenie = 4; // Население
    public static Resurs res; // Синглтон
    public int Nalog { get; set; } // Тип налога
    private MainScripts main; // Ссылка на главный скрипт
    [SerializeField]
    private float[] KoefNalog; // Коэффициент налога

    [Header("Интерфейсы ресурсов")]
    public Text _money;
    public Text _stone;
    public Text _aples;
    public Text _lumber;
    public Text _gems;
    public Text _happines;


    private Dictionary<int, float> StockResouces = new Dictionary<int, float>();

    private Dictionary<int, float> BarnFood = new Dictionary<int, float>();

    #region Очки и левел

    public Text _level;
    public Slider _score;
    public Text _scoreCount;

    [SerializeField]
    private float[] level;

    private float score = 0;
    public int Level { get; private set; }

    public void SetScore(float value)
    {
        score += value;
        float sc = score;
        for (int i = 0; i < level.Length; i++)
        {
            if (score < level[i])
            {
                _scoreCount.text = (int)sc + "/" + level[i];
                _score.value = sc / level[i];
                _level.text = "Level " + (i);
                break;
            }
            sc -= level[i];
        }
    }

    #endregion

    #region Деньги

    private float money = 500; // Деньги

    public float Money
    {
        get
        {
            return money;
        }
        set
        {
            _money.text = ((int)value).ToString();
            money = value;
        }
    }
    #endregion

    #region Камень

    private float stone = 300; // Камень

    public float Stone
    {
        get
        {
            return stone;
        }
        set
        {
            _stone.text = ((int)value).ToString();
            stone = value;
        }
    }
    #endregion

    #region Яблоки

    private float aples = 400; // Яблоки

    public float Aples
    {
        get
        {
            return aples;
        }
        set
        {
            _aples.text = ((int)value).ToString();
            aples = value;
        }
    }
    #endregion

    #region Дерево

    private float lumber = 500; // Дерево

    public float Lumber
    {
        get
        {
            return lumber;
        }
        set
        {
            _lumber.text = ((int)value).ToString();
            lumber = value;
        }
    }

    #endregion

    #region Кристалы

    private int gems = 5; // Кристалы

    public int Gems
    {
        get
        {
            return gems;
        }
        set
        {
            _gems.text = ((int)value).ToString();
            gems = value;
        }
    }

    #endregion

    #region Очки разведки

    private float intellegence = 0; // Очки разведки

    public float Intellegence
    {
        get
        {
            return intellegence;
        }
        set
        {
            intellegence = value;
        }
    }
    #endregion



    void Start()
    {
        res = this;
        Nalog = 3;
        _money.text = ((int)Money).ToString();
        _stone.text = stone.ToString();
        _aples.text = ((int)aples).ToString();
        _lumber.text = ((int)Lumber).ToString();
        _gems.text = gems.ToString();
        StartCoroutine(SetLevel(0));
        main = GetComponent<MainScripts>();
        Happines();
    }


    void Update()
    {

    }

    private float PeopleHap() // Счастье относительно количества населения
    {
        naselenie = main.Char.Count;
        peopleHap = Mathf.Pow(0.95f, naselenie);
        return peopleHap;
    }

    private float FoodHap() // Счастье относительно количества населения
    {
        foodHap = 1.0f;
        return foodHap;
    }

    private float NalogHap() // Счастье относительно количества населения
    {
        nalogHap = KoefNalog[Nalog];
        return nalogHap;
    }

    public float Happines() // Общее счастье
    {
        happiness = (NalogHap() * FoodHap() * PeopleHap());
        _happines.text = ((int)(happiness * 100)).ToString() + "%";
        return happiness;
    }

    public void SetFood(TypeFood food, float value) // Добавить еду в амбар
    {
        if (StockResouces.ContainsKey((int)food))
        {
            StockResouces[(int)food] += value;
        }
        else
        {
            StockResouces[(int)food] = value;
        }
    }

    public void SetResouce(TypeResource res, float value) // Добавить ресурс на склад
    {
        if (StockResouces.ContainsKey((int)res))
        {
            StockResouces[(int)res] += value;
        }
        else
        {
            StockResouces[(int)res] = value;
        }        
    }

    private IEnumerator SetLevel(float score)
    {
        SetScore(score);
        yield return new WaitForSeconds(60.0f);
        StartCoroutine(SetLevel(naselenie * happiness));
    }
}
