using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    public int speed = 1;
    public float currentTime;

    // Time UI Component
    public Text timeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        timeDisplay.text = currentTime.ToString("n2");
        Debug.Log("time scale: " + Time.timeScale);
    }

    public void Faster() {
        Time.timeScale += 0.5f;
    }

    public void Slower() {
        Time.timeScale -= 0.5f;
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Play() {
        Time.timeScale = 1;
    }
}
