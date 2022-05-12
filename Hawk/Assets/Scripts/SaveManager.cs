using UnityEngine;
using System;

public enum savePoint
{
    lastRace,
    maxScore,
    isSession
}

public class SaveManager: MonoBehaviour
{
    private object value;

    public T GetValue<T>(savePoint savePoint)
    {

        if (typeof(T) == typeof(int))
        {
            value = PlayerPrefs.GetInt(savePoint.ToString(), 0);
        }
        else if (typeof(T) == typeof(string))
        {
            value = PlayerPrefs.GetString(savePoint.ToString(), "");
        }
        else if (typeof(T) == typeof(float))
        {
            value = PlayerPrefs.GetFloat(savePoint.ToString(), 0);
        }
        return (T)Convert.ChangeType(value, typeof(T));
    }

    public void SetValue(savePoint savePoint, int value) 
    {
        PlayerPrefs.SetInt(savePoint.ToString(), value);
    }

    public void SetValue(savePoint savePoint, float value)
    {
        PlayerPrefs.SetFloat(savePoint.ToString(), value);
    }

    public void SetValue(savePoint savePoint, string value)
    {
        PlayerPrefs.SetString(savePoint.ToString(), value);
    }
}
