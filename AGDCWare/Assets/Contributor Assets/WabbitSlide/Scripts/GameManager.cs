using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace buckslice {
    public class GameManager : MonoBehaviour {
        public BunnyController bunny;
        public Image overlay;
        public Text gameTimer;
        public Text mainMessage;
        public Text speedometer;
        public Gradient speedColor;

        private bool gameOver = false;
        private float maxRecordedSpeed;
        private float speedToWin = 100.0f;

        void Start() {
            Time.timeScale = 0.0f;
            StartCoroutine(instructions());
        }

        private IEnumerator instructions() {
            Color c = overlay.color;
            c.a = 0.5f;
            overlay.color = c;

            while (!Input.anyKeyDown) {
                yield return null;
            }

            c.a = 0.0f;
            overlay.color = c;
            mainMessage.enabled = false;
            Time.timeScale = 1.0f;
        }

        void Update() {
            if (Time.timeScale < 0.01f) {
                return;
            }

            if (!gameOver) {
                float timeLeft = 30.0f;
                float startTime = bunny.getStartTime();
                if (startTime > 0.0f) {
                    timeLeft -= Time.timeSinceLevelLoad - startTime;
                }

                RectTransform srt = speedometer.rectTransform;
                if (timeLeft > 0.0f) {
                    gameTimer.text = timeLeft.ToString("F1") + "s";

                    float bunnySpeed = bunny.getSpeed();
                    maxRecordedSpeed = Mathf.Max(maxRecordedSpeed, bunnySpeed);
                    speedometer.text = bunnySpeed.ToString("F2");
                    speedometer.color = speedColor.Evaluate(bunnySpeed / speedToWin);

                    // shake the speedometer based on speed
                    Vector3 offsetMin = srt.offsetMin;
                    Vector3 offsetMax = srt.offsetMax;
                    float growth = Mathf.Max(0.0f, Mathf.Min((bunnySpeed - 20.0f) / (speedToWin - 20.0f), 1.0f));
                    growth = growth * growth * 200.0f;
                    offsetMin.x = offsetMax.x = (Mathf.PerlinNoise(Time.time * 20f, 0f) - .5f) * growth;
                    offsetMin.y = offsetMax.y = (Mathf.PerlinNoise(Time.time * 13f, 0f) - .5f) * growth;
                    srt.offsetMin = offsetMin;
                    srt.offsetMax = offsetMax;

                    if (maxRecordedSpeed >= speedToWin) {    // you win!!!
                        gameOver = true;
                        setClosingMessage("You Win!");
                        srt.offsetMin = Vector2.zero;
                        srt.offsetMax = Vector2.zero;
                        Camera.main.transform.parent = null;
                        Camera.main.GetComponent<AudioSource>().Play();

                        StartCoroutine(fadeAndExit(false, 3.5f));
                    }
                } else {    // you lose :(
                    gameOver = true;
                    setClosingMessage("Too Slow!");
                    gameTimer.text = "0.0s";
                    srt.offsetMin = Vector2.zero;
                    srt.offsetMax = Vector2.zero;

                    StartCoroutine(fadeAndExit(false, 2.0f));
                }
            } else {
                if (maxRecordedSpeed >= speedToWin) {
                    float t = (Mathf.Sin(Time.timeSinceLevelLoad * 10.0f) + 1.0f) / 2.0f;
                    speedometer.color = Color.Lerp(speedColor.Evaluate(maxRecordedSpeed / speedToWin), Color.white, t);
                    mainMessage.color = speedometer.color;
                } else {
                    mainMessage.color = Color.blue;
                }
            }
        }

        private void setClosingMessage(string message) {
            mainMessage.text = message;
            RectTransform mmrt = mainMessage.rectTransform;
            mmrt.offsetMin = Vector2.zero;
            mmrt.offsetMax = Vector2.zero;
            mmrt.anchorMin = new Vector2(0.2f, 0.3f);
            mmrt.anchorMax = new Vector2(0.8f, 0.7f);
            mainMessage.enabled = true;
        }

        private IEnumerator fadeAndExit(bool fadein, float time) {
            float endTime = Time.realtimeSinceStartup + time;
            while (Time.realtimeSinceStartup < endTime) {
                float t = Time.realtimeSinceStartup;
                Color c = overlay.color;
                if (fadein) {
                    c.a = (endTime - t) / time;
                } else {
                    c.a = 1.0f - (endTime - t) / time;
                }
                overlay.color = c;
                yield return null;
            }

            // end this game
            AGDCWareFramework.LoadNextGame();
        }
    }
}