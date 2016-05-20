using UnityEngine;
using System.Collections;
using System;

public class Viewer {

    string username;
    public string Username
    {
        get { return username; }
    }
    Archetypes archetype;
    public Archetypes Archetype
    {
        get { return archetype; }
    }

    private bool isAlive = true;
    public bool IsAlive
    {
        get { return isAlive; }
    }

    public int gold = 0;
    public int food = 0;


    public Viewer(string name, string atype){
        username = name;
        switch (atype)
        {
            case "cleric":
                archetype = Archetypes.Cleric;
                break;
            case "soldier":
                archetype = Archetypes.Soldier;
                break;
            case "tracker":
                archetype = Archetypes.Tracker;
                break;
            case "trader":
                archetype = Archetypes.Vendor;
                break;
            default:
                Array a = Enum.GetValues(typeof(Archetypes));
                int index = UnityEngine.Random.Range(0, a.Length);
                archetype = (Archetypes)a.GetValue(index);
                break;
        }
        ResourceTracker.crew++;
        GenerateStartingBounty();

    }

    public Viewer(string name)
    {
        username = name;
        archetype = Archetypes.Streamer;
        ResourceTracker.crew++;
        GenerateStartingBounty();

    }

    void GenerateStartingBounty()
    {
        switch (archetype)
        {
            case Archetypes.Cleric:
                gold = UnityEngine.Random.Range(5, 11);
                food = UnityEngine.Random.Range(2, 4);
                ResourceTracker.cleric++;
                break;
            case Archetypes.Soldier:
                gold = UnityEngine.Random.Range(4, 10);
                food = UnityEngine.Random.Range(3, 5);
                ResourceTracker.soldier++;
                break;
            case Archetypes.Tracker:
                gold = UnityEngine.Random.Range(2, 8);
                food = UnityEngine.Random.Range(4, 8);
                ResourceTracker.tracker++;
                break;
            case Archetypes.Vendor:
                gold = UnityEngine.Random.Range(10, 17);
                food = UnityEngine.Random.Range(1, 3);
                ResourceTracker.trader++;
                break;
            case Archetypes.Streamer:
                gold = 15;
                food = 15;
                break;
            default:
                gold = UnityEngine.Random.Range(4, 10);
                break;

        }

        ResourceTracker.food += food;
        ResourceTracker.gold += gold;
    }

    public void Kill(string cause)
    {
        isAlive = false;
        ResourceTracker.crew--;
        ResourceTracker.dead++;
        switch (archetype)
        {
            case Archetypes.Cleric:
                ResourceTracker.cleric--;
                break;
            case Archetypes.Soldier:
                ResourceTracker.soldier--;
                break;
            case Archetypes.Tracker:
                ResourceTracker.tracker--;
                break;
            case Archetypes.Vendor:
                ResourceTracker.trader--;
                break;
            default:
                break;
        }

    }

}
