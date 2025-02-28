using UnityEngine;

public static class killCountController
{
    private static int killCount = 0;
    public static int Pontuacao
    {
        get { 
            return killCount; 
            }
        set { 
            killCount = value; 
            if (killCount < 0)
            {
                killCount = 0;
            }
            Debug.Log("teste Pontuação: " + killCount);
        }	 
    }
}
