using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int index;
    private string pName;
    private Role startingRole;
    private Role role;
    private bool isRoleSet;
    private bool isCursed;
    private bool isAlive;
    private bool isCleaned;
    private bool isWinner;
    private List<Player> killers;
    private List<Player> visitors;

    public Player(int _index, string _pName)
    {
        index = _index;
        pName = _pName;
        startingRole = GlobalData.GD.GetRole(11);   //Townie
        role = GlobalData.GD.GetRole(11);           //Townie
        isAlive = true;
        killers = new List<Player>();
        visitors = new List<Player>();
    }

    public int getIndex()
    {
        return index;
    }

    public void setIndex(int _index)
    {
        index = _index;
    }

    public string getPName()
    {
        return pName;
    }

    public void setPName(string _pName)
    {
        pName = _pName;
    }

    public Role getStartingRole()
    {
        return startingRole;
    }

    public void setStartingRole(Role _role)
    {
        startingRole = _role;
        role = _role;
        isRoleSet = true;
    }

    public Role getRole()
    {
        return role;
    }

    public void setRole(Role _role)
    {
        role = _role;
    }

    public bool getIsRoleSet()
    {
        return isRoleSet;
    }

    public void setNightImmune(bool _nightImmune)
    {
        role.nightImmune = _nightImmune;
    }

    public void setGood(bool _good)
    {
        role.good = _good;
    }

    public Color getStartingColor()
    {
        return GlobalData.GD.GetRoleColor(startingRole.ID);
    }

    public Color getColor()
    {
        return GlobalData.GD.GetRoleColor(role.ID);
    }

    public Faction getFaction()
    {
        return role.faction;
    }

    public bool getIsCursed()
    {
        return isCursed;
    }

    public void setIsCursed(bool _isCursed)
    {
        isCursed = _isCursed;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }

    public void setIsAlive(bool _isAlive)
    {
        isAlive = _isAlive;
    }

    public bool getIsCleaned()
    {
        return isCleaned;
    }

    public void setIsCleaned(bool _isCleaned)
    {
        isCleaned = _isCleaned;
    }

    public bool getIsWinner()
    {
        return isWinner;
    }

    public void setIsWinner(bool _isWinner)
    {
        isWinner = _isWinner;
    }

    public List<Player> getKillers()
    {
        return killers;
    }

    public void addKiller(Player _killer)
    {
        killers.Add(_killer);
    }

    public void removeKiller(Player _killer)
    {
        if (killers.Contains(_killer))
            killers.Remove(_killer);
        else
        {
            Debug.LogError("Killer not found");
        }
    }

    public void clearKillers()
    {
        killers.Clear();
    }

    public List<Player> getVisitors()
    {
        return visitors;
    }

    public void addVisitor(Player _visitor)
    {
        visitors.Add(_visitor);
    }

    public void clearVisitors()
    {
        visitors.Clear();
    }
}
