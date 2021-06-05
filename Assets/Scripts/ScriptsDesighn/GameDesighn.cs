using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDesighn : MonoBehaviour
{
    [SerializeField]
    private ChangeUI _container;
    [SerializeField]
    private Image _playground;
    [SerializeField]
    private Image _frame;
    [SerializeField]
    private Image _buttonPause;

    private void Start()
    {
        _playground.sprite = _container.PlaygroundImage;
        _frame.sprite = _container.frameImage;
        _buttonPause.sprite = _container.ButtonPause;


    }


}
