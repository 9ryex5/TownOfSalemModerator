using UnityEngine;
using System.Collections.Generic;

public class DataTransfer : MonoBehaviour
{
    public static DataTransfer DT; //Singleton

    private bool splash;                    //Flag to check if this is the first game of the session
    private List<Person> playingPersons;    //People that are going to play (Excludes Moderator)
    private Role[] playingRoles;            //Roles that will be playing
    private List<Person> lastPlaying;       //People that played last game (Includes Moderator)

    private void Awake()
    {
        if (DT == null)
        {
            DT = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        splash = true;
    }

    private void Start()
    {
        lastPlaying = new List<Person>();
    }

    public bool getSplash()
    {
        return splash;
    }

    public void setSplash(bool _splash)
    {
        splash = _splash;
    }

    public List<Person> getPlayingPersons()
    {
        return playingPersons;
    }

    public void setPlayingPersons(List<Person> _playerNames)
    {
        playingPersons = _playerNames;
    }

    public Role[] getPlayingRoles()
    {
        return playingRoles;
    }

    public void setPlayingRoles(Role[] _roles)
    {
        playingRoles = _roles;
    }

    public List<Person> getLastPlaying()
    {
        return lastPlaying;
    }

    public void setLastPlaying(List<Person> _lastPlaying)
    {
        lastPlaying = new List<Person>(_lastPlaying);
    }
}
