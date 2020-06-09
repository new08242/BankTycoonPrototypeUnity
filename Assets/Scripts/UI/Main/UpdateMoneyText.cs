using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoneyText : MonoBehaviour
{
    private Color alphaColor;
    private bool Isfade = false;
    private float timeToFade = 1.0f;

    private void Start() {
        Isfade = true;
        alphaColor = gameObject.GetComponent<Text>().color;
        alphaColor.a = 0;
    }

    private void OnEnable() {
        Isfade = true;
        alphaColor = gameObject.GetComponent<Text>().color;
        alphaColor.a = 0;
    }

    private void Update() {
        Fade();
    }

    private void Fade() {
        if (Isfade) {
            gameObject.GetComponent<Text>().color = Color.Lerp(gameObject.GetComponent<Text>().color, alphaColor, timeToFade * Time.deltaTime);

            // complete fade
            if (gameObject.GetComponent<Text>().color.a < 0.05) {
                Isfade = false;
                gameObject.SetActive(false);
            }
        }
    }
}
