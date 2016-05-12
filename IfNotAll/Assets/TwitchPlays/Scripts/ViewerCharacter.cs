using UnityEngine;
using System.Collections;

public class ViewerCharacter : MonoBehaviour {

    private bool isAlive = true;
    public bool IsAlive
    {
        get { return isAlive; }
    }

    private static int minGold = 2;
    private static int maxGold = 25;

    private static int minFood = 1;
    private static int maxFood = 10;

    public int gold;
    public int food;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateStartingStats()
    {
        gold = Random.Range(minGold, maxGold);
        food = Random.Range(minFood, maxFood);
    }

    public void Kill(string cause)
    {
        isAlive = false;
        ResourceTracker.crew--;
        ResourceTracker.dead++;
    }
}
