using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour
{
    public GameObject[] bosses; // Array de referências aos chefes
    private int currentBossIndex = 0;

    void Start()
    {
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