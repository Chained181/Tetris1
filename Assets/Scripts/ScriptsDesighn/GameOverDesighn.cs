using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDesighn : MonoBehaviour
{
    [SerializeField]
    private ChangeUI _container;
    [SerializeField]
    private Image _playground;
    [SerializeField]
    private Image _retryButton;
    [SerializeField]
    private Image _menuButton;
    void Start()
    {
        _playground.sprite = _container.PlaygroundImage;
        _retryButton.sprite = _container.ButtonRestart;
        _menuButton.sprite = _container.ButtonMenu;
    }

    
}
