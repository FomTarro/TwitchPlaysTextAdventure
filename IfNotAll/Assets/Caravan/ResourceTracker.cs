using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ResourceTracker : Singleton<ResourceTracker>
{

    public static int crew = 0;
    public static int dead = 0;
    public static int gold = 0;
    public static int food = 0;

    public static int cleric = 0;
    public static int soldier = 0;
    public static int tracker = 0;
    public static int trader = 0;

    public static int day = -1;

    [SerializeField]
    private Text _crewText, _deadText, _foodText, _goldText, _soldierText, _clericText, _trackerText, _traderText, _dayText, _locText;

    private Ticker _ticker;

    // Use this for initialization
    void Start()
    {
        _ticker = Ticker.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        _crewText.text = " CREW: " + crew;
        _deadText.text = " DEAD: " + dead;
        _goldText.text = " HOPE: " + gold;
        _foodText.text = " FOOD: " + food;

        _soldierText.text = " GUNMAN: " + soldier;
        _clericText.text = " DOCTOR: " + cleric;
        _trackerText.text = " HUNTER: " + tracker;
        _traderText.text = " TRADER: " + trader;

        _dayText.text = " DAY: " + Mathf.Max(0, day).ToString();
    }

    public void IncrementDay()
    {
        day++;
        string dayString = "[cyan]DAY " + day + " BEGINS[cyan]";
        string buffer = "";
        /*
        for(int i = 0; i < dayString.Length; i++)
        {
            buffer += "-";
        }
        */
        //buffer = "-------------------------------------------------";
        _ticker.PrintToTicker(buffer + dayString + buffer);
    }

    public void SetLocation(string loc)
    {
        _locText.text = " LOC: " + loc;
    }

}
