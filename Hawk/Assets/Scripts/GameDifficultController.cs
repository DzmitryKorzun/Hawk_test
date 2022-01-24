using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
public enum GameDifficult
{
    Easy,
    Medium,
    Hard
}

public class GameDifficultController : MonoBehaviour
{
    [Serializable]
    private class GameDifficultClass
    {
        [SerializeField] private float coefficient;
        [SerializeField] private GameDifficult gameDifficult;

        public GameDifficult GameDifficult => gameDifficult;
        public float Coefficient => coefficient;
    }

    [SerializeField] GameDifficultClass[] gameDifficults;
    [SerializeField] Dropdown dropdownDifficult;
    [SerializeField] EnemySpawner enemySpawner;

    private GameDifficult selectedGameDifficult;
    private int ID = 0;

    private void Start()
    {
        dropdownDifficult.onValueChanged.AddListener(ChoiceGameDifficulty);
        enemySpawner.DifficultCoefficient = gameDifficults[0].Coefficient;
    }

    private void ChoiceGameDifficulty(int arg0)
    {
        selectedGameDifficult = (GameDifficult)Enum.GetValues(typeof(GameDifficult)).GetValue(arg0);
        for (int i = 0; i < gameDifficults.Length; i++)
        {
            if (gameDifficults[i].GameDifficult.Equals(selectedGameDifficult))
            {
                ID = i;
                break;
            }
        }
        enemySpawner.DifficultCoefficient = gameDifficults[ID].Coefficient;
    }
}
