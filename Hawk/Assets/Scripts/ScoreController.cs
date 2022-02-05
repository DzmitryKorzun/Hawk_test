using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private float distanceRewardRatio;

    private int score;
    private int maxScore;
    private float mapMoveSpeed;
    private float pointPart;
    private SaveManager saveManager;

    public int Score => score;
    public int MaxScore => maxScore;
    
    public void AddBonusScore(int value)
    {
        score += value;
    }

    public void Setting(float mapMoveSpeed, SaveManager saveManager)
    {
        this.mapMoveSpeed = mapMoveSpeed;
        this.saveManager = saveManager;
        this.maxScore = saveManager.GetValue<int>(savePoint.maxScore);
    }

    private void FixedUpdate()
    {
        pointPart += (mapMoveSpeed * distanceRewardRatio);
        score += (int)(pointPart);
        if (pointPart > 1) pointPart = 0;
    }

    private void record—heck()
    {
        if (maxScore < score)
        {
            saveManager.SetValue(savePoint.maxScore, score);
            saveManager.SetValue(savePoint.lastRace, score);
        }
        else
        {
            saveManager.SetValue(savePoint.lastRace, score);
        }
    }
    private void OnDisable()
    {
        record—heck();
    }
}
