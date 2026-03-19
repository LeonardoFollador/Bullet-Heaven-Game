using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class ScoreController
{
    public static int score = 0;
    private static Dictionary<string, int> scoreByEnemyType = new Dictionary<string, int>()
    {
        {"Enemy_1_caveira", 100},
        {"Enemy_2_minotauro", 200 },
        {"Enemy_3_voador", 25 },
        {"Enemy_4_coelho", 300},
        {"Enemy_5_capetinha", 150},
        {"Boss_1", 500 }
    };

    public static void updateScore(string enemyType)
    {
        string key = enemyType.Replace("(Clone)", "").Trim();

        if (scoreByEnemyType.ContainsKey(key))
        {
            score += scoreByEnemyType[key];
        }
        else
        {
            Debug.LogWarning("Enemy n�o encontrado no score: " + key);
        }
    }



    public static int getScore()
    {
        return score;
    }

    public static void restartScore()
    {
        score = 0;
    }
}
