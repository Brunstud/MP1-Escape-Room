public enum LabEntryMode
{
    Initial,
    FinalWin,
    Restart
}

public static class GameSessionData
{
    public static LabEntryMode LabMode = LabEntryMode.Initial;
    public static float PrincipalRemainingTime = 0f;
    public static int PrincipalCollectedCount = 0;
    public static int PrincipalTotalCollectibles = 0;

    public static void ResetResults()
    {
        PrincipalRemainingTime = 0f;
        PrincipalCollectedCount = 0;
        PrincipalTotalCollectibles = 0;
    }
}
