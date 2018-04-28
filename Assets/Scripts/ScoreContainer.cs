using System.Collections;
using System.Collections.Generic;

public static class ScoreContainer {
    private static PhotonPlayer[] pList;
    public static PhotonPlayer[] scores
    {
        get
        {
            return pList;
        }
        set
        {
            pList = value;
        }
    }
}
