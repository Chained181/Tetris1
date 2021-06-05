using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDesignh : MonoBehaviour
{
    [SerializeField]
    private ChangeUI _container;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private Image _startbutton;
    [SerializeField]
    private Image _exitButton;

    void Start()
    {

        _background.sprite = _container.backgroundImage;
        _startbutton.sprite = _container.playButton;
        _exitButton.sprite = _container.ExitButton;
    }

    
}
