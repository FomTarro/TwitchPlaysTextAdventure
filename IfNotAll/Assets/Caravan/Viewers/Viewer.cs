using UnityEngine;
using System.Collections;
using System;

public class Viewer {

    string _username;
    public string Username
    {
        get { return _username; }
    }
    public string UsernameFormatted
    {
        get { return "[white]" + _username + "[white]"; }
    }

    Archetypes _archetype;
    public Archetypes Archetype
    {
        get { return _archetype; }
    }

    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
    }

    public int _gold = 0;
    public int Currency
    {
        get { return _gold; }
    }
    public int _food = 0;
    public int Food
    {
        get { return _food; }
    }
    public int _deathDay = 0;


    public Viewer(string name, string atype){
        atype = atype.ToLower();
        _username = name;
        switch (atype)
        {
            case "doctor":
                _archetype = Archetypes.Cleric;
                break;
            case "gunman":
                _archetype = Archetypes.Soldier;
                break;
            case "hunter":
                _archetype = Archetypes.Tracker;
                break;
            case "trader":
                _archetype = Archetypes.Vendor;
                break;
            default:
                Array a = Enum.GetValues(typeof(Archetypes));
                int index = UnityEngine.Random.Range(1, a.Length);
                _archetype = (Archetypes)a.GetValue(index);
                break;
        }
        ResourceTracker.crew++;
        GenerateStartingBounty();

    }

    public Viewer(string name)
    {
        _username = name;
        _archetype = Archetypes.Streamer;
        ResourceTracker.crew++;
        GenerateStartingBounty();

    }

    void GenerateStartingBounty()
    {
        switch (_archetype)
        {
            case Archetypes.Cleric:
                _gold = UnityEngine.Random.Range(5, 11);
                _food = UnityEngine.Random.Range(2, 4);
                ResourceTracker.cleric++;
                break;
            case Archetypes.Soldier:
                _gold = UnityEngine.Random.Range(4, 10);
                _food = UnityEngine.Random.Range(3, 5);
                ResourceTracker.soldier++;
                break;
            case Archetypes.Tracker:
                _gold = UnityEngine.Random.Range(2, 8);
                _food = UnityEngine.Random.Range(4, 8);
                ResourceTracker.tracker++;
                break;
            case Archetypes.Vendor:
                _gold = UnityEngine.Random.Range(10, 17);
                _food = UnityEngine.Random.Range(1, 3);
                ResourceTracker.trader++;
                break;
            case Archetypes.Streamer:
                _gold = 15;
                _food = 15;
                break;
            default:
                _gold = UnityEngine.Random.Range(4, 10);
                _food = UnityEngine.Random.Range(1, 3);
                break;

        }

        ResourceTracker.food += _food;
        ResourceTracker.gold += _gold;
    }

    public void Kill(string cause)
    {
        _isAlive = false;
        _deathDay = ResourceTracker.day;
        Ticker.Instance.PrintToTicker(UsernameFormatted + " was [red]killed[red].");
        ResourceTracker.crew--;
        ResourceTracker.dead++;
        switch (_archetype)
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
