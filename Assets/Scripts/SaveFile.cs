using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveFile : MonoBehaviour
{

    public static SaveFile SF; //Singleton
    public int version;

    private int totalGames;
    private int[] totalGamesByRole;
    private int[] totalWinsByRole;

    private List<Person> allPersons;

    private float volume;
    private int language;

    // Variants
    public bool bodyguardGuardTwice;
    public bool doctorCanSaveBodyguard;
    public bool doctorCanSelfHeal;
    public bool sheriffFindWerewolfFullMoon;
    public bool survivorLynchedDisableTownAbilities;
    public bool blackmailedCanVote;
    public bool godfatherAloneTwoKills;
    public bool executionerPickTarget;
    public bool executionerOnlyWinner;
    public bool jesterOnlyWinner;
    public bool werewolfImmuneFullMoon;
    public bool witchDieCursedDie;
    public bool blackmailerPlaysBeforeConsigliere;

    private void Awake()
    {
        if (SF == null)
        {
            SF = this;
            DontDestroyOnLoad(gameObject);

            loadGameData();
            loadPlayerData();
            loadVariantsData();
            loadSettingsData();

            saveVersionData();
        }
        else
        {
            Destroy(this);
        }
    }

    public void NewSession()
    {
        for (int i = 0; i < allPersons.Count; i++)
            allPersons[i].newSession();
    }

    public int getTotalGames()
    {
        return totalGames;
    }

    public void incrementTotalGames()
    {
        ++totalGames;
    }

    public int getTotalGamesbyRole(int _role)
    {
        return totalGamesByRole[_role];
    }

    public void incrementGamesByRole(int _role)
    {
        ++totalGamesByRole[_role];
    }

    public int getTotalWinsbyRole(int _role)
    {
        return totalWinsByRole[_role];
    }

    public void incrementWinsByRole(int _role)
    {
        ++totalWinsByRole[_role];
    }

    public int getTotalPersons()
    {
        return allPersons.Count;
    }

    public Person GetPerson(int index)
    {
        return allPersons[index];
    }

    public void addPerson(Person _person)
    {
        allPersons.Add(_person);
    }

    public void removePerson(Person _person)
    {
        allPersons.Remove(_person);
    }

    public void SortPlayers()
    {
        allPersons.Sort(SortByAbc);
    }

    public int SortByAbc(Person p1, Person p2)
    {
        return p1.pName.CompareTo(p2.pName);
    }

    public float getVolume()
    {
        return volume;
    }

    public void SetVolume(float _volume)
    {
        volume = _volume;
    }

    public int getLanguage()
    {
        return language;
    }

    public void SetLanguage(int _language)
    {
        language = _language;
    }

    public void saveVersionData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/VersionData.vd");

        VersionData data = new VersionData();

        data.version = version;

        bf.Serialize(file, data);
        file.Close();
    }

    public void saveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.gd");

        GameData data = new GameData();

        data.totalGames = totalGames;
        data.totalGamesByRole = totalGamesByRole;
        data.totalWinsByRole = totalWinsByRole;

        bf.Serialize(file, data);
        file.Close();
    }

    public void loadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameData.gd", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            totalGames = data.totalGames;
            totalGamesByRole = data.totalGamesByRole;
            totalWinsByRole = data.totalWinsByRole;
        }
        else
        {
            totalGames = 0;
            totalGamesByRole = new int[GlobalData.GD.GetNRoles()];
            totalWinsByRole = new int[GlobalData.GD.GetNRoles()];
        }
    }

    public void ResetGameData()
    {
        File.Delete(Application.persistentDataPath + "/GameData.gd");
        loadGameData();
    }

    public void SavePlayerData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.pd");

        PlayerData data = new PlayerData();

        data.allPersons = allPersons;

        bf.Serialize(file, data);
        file.Close();
    }

    public void loadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.pd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.pd", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            allPersons = data.allPersons;
        }
        else
            allPersons = new List<Person>();
    }

    public void ResetPlayerData()
    {
        File.Delete(Application.persistentDataPath + "/PlayerData.pd");
        loadPlayerData();
    }

    public void SaveVariantsData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/VariantsData.vd");

        VariantsData data = new VariantsData();

        data.bodyguardGuardTwice = bodyguardGuardTwice;
        data.doctorCanSaveBodyguard = doctorCanSaveBodyguard;
        data.doctorCanSelfHeal = doctorCanSelfHeal;
        data.sheriffFindWerewolfFullMoon = sheriffFindWerewolfFullMoon;
        data.survivorLynchedDisableTownAbilities = survivorLynchedDisableTownAbilities;
        data.blackmailedCanVote = blackmailedCanVote;
        data.godfatherAloneTwoKills = godfatherAloneTwoKills;
        data.executionerPickTarget = executionerPickTarget;
        data.executionerOnlyWinner = executionerOnlyWinner;
        data.jesterOnlyWinner = jesterOnlyWinner;
        data.werewolfImmuneFullMoon = werewolfImmuneFullMoon;
        data.witchDieCursedDie = witchDieCursedDie;
        data.blackmailerPlaysBeforeConsigliere = blackmailerPlaysBeforeConsigliere;

        bf.Serialize(file, data);
        file.Close();
    }

    public void loadVariantsData()
    {
        if (File.Exists(Application.persistentDataPath + "/VariantsData.vd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/VariantsData.vd", FileMode.Open);
            VariantsData data = (VariantsData)bf.Deserialize(file);
            file.Close();

            bodyguardGuardTwice = data.bodyguardGuardTwice;
            doctorCanSaveBodyguard = data.doctorCanSaveBodyguard;
            doctorCanSelfHeal = data.doctorCanSelfHeal;
            sheriffFindWerewolfFullMoon = data.sheriffFindWerewolfFullMoon;
            survivorLynchedDisableTownAbilities = data.survivorLynchedDisableTownAbilities;
            blackmailedCanVote = data.blackmailedCanVote;
            godfatherAloneTwoKills = data.godfatherAloneTwoKills;
            executionerPickTarget = data.executionerPickTarget;
            executionerOnlyWinner = data.executionerOnlyWinner;
            jesterOnlyWinner = data.jesterOnlyWinner;
            werewolfImmuneFullMoon = data.werewolfImmuneFullMoon;
            witchDieCursedDie = data.witchDieCursedDie;
            blackmailerPlaysBeforeConsigliere = data.blackmailerPlaysBeforeConsigliere;
        }
        else
        {
            bodyguardGuardTwice = true;
            doctorCanSaveBodyguard = true;
            doctorCanSelfHeal = true;
            sheriffFindWerewolfFullMoon = true;
            survivorLynchedDisableTownAbilities = false;
            blackmailedCanVote = true;
            godfatherAloneTwoKills = false;
            executionerPickTarget = true;
            executionerOnlyWinner = false;
            jesterOnlyWinner = false;
            werewolfImmuneFullMoon = true;
            witchDieCursedDie = true;
            blackmailerPlaysBeforeConsigliere = false;
        }
    }

    public void SaveSettingsData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SettingsData.sd");

        SettingsData data = new SettingsData();

        data.volume = volume;
        data.language = language;

        bf.Serialize(file, data);
        file.Close();
    }

    public void loadSettingsData()
    {
        if (File.Exists(Application.persistentDataPath + "/SettingsData.sd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SettingsData.sd", FileMode.Open);
            SettingsData data = (SettingsData)bf.Deserialize(file);
            file.Close();

            volume = data.volume;
            language = data.language;
        }
        else
        {
            volume = 1;
            language = 0;
        }
    }

    [Serializable]
    class VersionData
    {
        public int version;
    }

    [Serializable]
    class GameData
    {
        public int totalGames;
        public int[] totalGamesByRole;
        public int[] totalWinsByRole;
    }

    [Serializable]
    class PlayerData
    {
        public List<Person> allPersons;
    }

    [Serializable]
    class VariantsData
    {
        public bool bodyguardGuardTwice;
        public bool doctorCanSaveBodyguard;
        public bool doctorCanSelfHeal;
        public bool sheriffFindWerewolfFullMoon;
        public bool survivorLynchedDisableTownAbilities;
        public bool blackmailedCanVote;
        public bool godfatherAloneTwoKills;
        public bool executionerPickTarget;
        public bool executionerOnlyWinner;
        public bool jesterOnlyWinner;
        public bool werewolfImmuneFullMoon;
        public bool witchDieCursedDie;
        public bool blackmailerPlaysBeforeConsigliere;
    }

    [Serializable]
    class SettingsData
    {
        public float volume;
        public int language;
    }
}