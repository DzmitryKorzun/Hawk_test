using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private int score;
    private StringBuilder scoreSB = new StringBuilder("Score: ", 15);
    private ScoreController scoreController;

    public void Setting(ScoreController scoreController)
    {
        this.scoreController = scoreController;
    }

    private void FixedUpdate()
    {
        score = scoreController.Score;
        scoreSB.Append("Score: ");
        scoreSB.Append(score);
        scoreText.text = scoreSB.ToString();
        scoreSB.Clear();
    }
}
