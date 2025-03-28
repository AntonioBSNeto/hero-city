using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour
{
    public GameObject[] bosses; // Array de referências aos chefes
    private int currentBossIndex = 0;
    private GameSceneManager sceneManager;

    void Start()
    {
        sceneManager = FindObjectOfType<GameSceneManager>();

        // Desativar todos os chefes no início
        foreach (GameObject boss in bosses)
        {
            boss.SetActive(false);
        }

        // Ativar o primeiro chefe
        if (bosses.Length > 0)
        {
            ActivateBoss(currentBossIndex);
        }
    }

    public void OnBossDeath()
    {
        currentBossIndex++;
        if (currentBossIndex < bosses.Length)
        {
            StartCoroutine(ActivateNextBoss());
        }
        else
        {
            // Todos os chefes foram derrotados, ir para a tela de HighScore
            sceneManager.LoadHighScoreAfterDelay(3f);
        }
    }

    private IEnumerator ActivateNextBoss()
    {
        yield return new WaitForSeconds(5f);
        ActivateBoss(currentBossIndex);
    }

    private void ActivateBoss(int index)
    {
        if (index < bosses.Length)
        {
            bosses[index].SetActive(true);
        }
    }
}