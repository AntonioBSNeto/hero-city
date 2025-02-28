using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public Text killCountText;
    void Update()
    {
        this.killCountText.text = "Score: " + killCountController.Pontuacao; // talvez precise usar killCountController.Pontuacao.toString()
    }
}
