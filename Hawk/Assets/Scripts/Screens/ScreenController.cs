using Core;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private MenuScreen menuScreen;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private Meta meta;
    public GameScreen ShowGameScreen()
    {
        gameScreen.gameObject.SetActive(true);
        return gameScreen;
    }

    public MenuScreen ShowMenuScreen()
    {
        menuScreen.gameObject.SetActive(true);
        return menuScreen;
    }
}
