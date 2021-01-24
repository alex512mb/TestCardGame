using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Button buttonRandomChange = default;
    [SerializeField]
    private GameController gameController;

    private void Awake()
    {
        buttonRandomChange.onClick.AddListener(ButtomRandomChange_OnClick);
    }

    private void ButtomRandomChange_OnClick()
    {
        gameController.ApplyRandomValueBySequence();
    }


}
