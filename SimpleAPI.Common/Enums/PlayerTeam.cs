using System;

namespace SimpleAPI.Common.Enums
{
    public enum TeamConferences
    {
        AFC,
        NFC
    }

    public enum TeamDivisions
    {
        North,
        South,
        East,
        West
    }

    public enum PlayerStatus
    {
        A, //Active
        S, //Suspended
        R, //Retired
        I, //Inactive
        EX, //Exempt

    }

    /// <summary>
    /// These should probably be stored on the game/roster, not directly on the player
    /// </summary>
    public enum PlayerGameStatus
    {
        A, //Active
        P, //Probably
        Q, //Questionable
        D, //Doubtful
        O, //Out
        SUS, //Suspended
        IR, //Injured Reserve
        NON, //Non football injury
        PUP, //Physcially unable to Perform
        EXE, //Exempt
    }

    [Flags]
    public enum PlayerPosition :int
    {
        None=1,
        QB=2, 
        WR=4,
        RB=8,
        TE=16,
    }
}
