using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ResourceTracker : MonoBehaviour {

    public static int crew = 0;
    public static int dead = 0;
    public static int gold = 0;
    public static int food = 0;

    public static int cleric = 0;
    public static int soldier = 0;
    public static int tracker = 0;
    public static int trader = 0;

    [SerializeField]
    private Text crewText, deadText, foodText, goldText, soldierText, clericText, trackerText, traderText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    crewText.text = " CREW: " + crew;
        deadText.text = " DEAD: " + dead;
        goldText.text = " GOLD: " + gold;
        foodText.text = " FOOD: " + food;

        soldierText.text = " SOLDIER: " + soldier;
        clericText.text = " CLERIC: " + cleric;
        trackerText.text = " TRACKER: " + tracker;
        traderText.text = " TRADER: " + trader;
    }
}
