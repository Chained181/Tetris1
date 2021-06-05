using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeDesighn : MonoBehaviour
{
    [SerializeField]
    private ChangeUI _container;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private Image _startbutton;
    [SerializeField]
    private Image _menuButton;

    void Start()
    {
        _background.sprite = _container.backgroundImage;
        _startbutton.sprite = _container.playButton;
        _menuButton.sprite = _container.ButtonMenu;
    }

    
}
