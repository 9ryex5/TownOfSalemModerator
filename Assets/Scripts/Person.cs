using System;

[Serializable]
public class Person
{
    public string pName;
    public string pNameShort;
    public int[] gamesByRole;
    public int gamesModerated;
    public int[] winsByRole;
    public int[] gamesByRoleSession;
    public int gamesModeratedSession;
    public int[] winsByRoleSession;

    public Person(string _pName, string _pNameShort)
    {
        int nRoles = GlobalData.GD.GetNRoles();

        pName = _pName;
        pNameShort = _pNameShort;
        gamesByRole = new int[nRoles];
        winsByRole = new int[nRoles];
        gamesByRoleSession = new int[nRoles];
        winsByRoleSession = new int[nRoles];
    }

    public int getTotalGames()
    {
        int c = 0;

        for (int i = 0; i < gamesByRole.Length; i++)
        {
            c += gamesByRole[i];
        }

        return c;
    }

    public float getTotalWinRate()
    {
        int wins = 0;
        int games = getTotalGames();

        for (int i = 0; i < winsByRole.Length; i++)
        {
            wins += winsByRole[i];
        }

        return games == 0 ? 0 : wins / (float)games;
    }

    public int getTotalGamesSession()
    {
        int c = 0;

        for (int i = 0; i < gamesByRoleSession.Length; i++)
        {
            c += gamesByRoleSession[i];
        }

        return c;
    }

    public float getTotalWinRateSession()
    {
        int wins = 0;
        int games = getTotalGamesSession();

        for (int i = 0; i < winsByRoleSession.Length; i++)
        {
            wins += winsByRoleSession[i];
        }

        return games == 0 ? 0 : wins / (float)games;
    }

    public int GetGamesByRole(int _role)
    {
        return gamesByRole[_role];
    }

    public int GetWinsByRole(int _role)
    {
        return winsByRole[_role];
    }

    public float GetRateByRole(int _role)
    {
        return gamesByRole[_role] == 0 ? 0 : winsByRole[_role] / (float)gamesByRole[_role];
    }

    public int GetGamesByRoleSession(int _role)
    {
        return gamesByRoleSession[_role];
    }

    public int GetWinsByRoleSession(int _role)
    {
        return winsByRoleSession[_role];
    }

    public float GetRateByRoleSession(int _role)
    {
        return gamesByRoleSession[_role] == 0 ? 0 : winsByRoleSession[_role] / (float)gamesByRoleSession[_role];
    }

    public void incrementGameByRole(int _role)
    {
        ++gamesByRole[_role];
        ++gamesByRoleSession[_role];
    }

    public void incrementGamesModerated()
    {
        ++gamesModerated;
        ++gamesModeratedSession;
    }

    public void incrementWinsByRole(int _role)
    {
        ++winsByRole[_role];
        ++winsByRoleSession[_role];
    }

    public void newSession()
    {
        gamesByRoleSession = new int[GlobalData.GD.GetNRoles()];
        gamesModeratedSession = 0;
        winsByRoleSession = new int[GlobalData.GD.GetNRoles()];
    }
}
