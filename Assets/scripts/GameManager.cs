using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }
  public GameObject starterBulletPrefab;
  private void Awake()
  {
    if (Instance != null)
    {
      DestroyImmediate(gameObject);
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }

  private void OnDestroy()
  {
    if (Instance == this)
    {
      Instance = null;
    }
  }

  private void Start()
  {
    Application.targetFrameRate = 60;
    SingleBulletPoolManager.Instance.SetBulletType(starterBulletPrefab, 20);
  }
}