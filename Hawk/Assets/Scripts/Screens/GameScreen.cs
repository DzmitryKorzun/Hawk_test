using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private int score;
    private StringBuilder scoreSB = new StringBuilder("Score: ", 15);


}
