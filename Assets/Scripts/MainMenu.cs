﻿using UnityEngine;

public class MainMenu : MonoBehaviour {
    public void StartButton() {
        SceneManagerController.Instance.ToGamplayScene();
    }
}