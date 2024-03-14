using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
    public static int bestScore
    {
        get => PlayerPrefs.GetInt(Const.BEST_SOCRE, 0);
        set
        {
            int curScore = PlayerPrefs.GetInt(Const.BEST_SOCRE,0);
            if(value > curScore)
            {
                PlayerPrefs.SetInt(Const.BEST_SOCRE, value);
            }
        }
    }
}
