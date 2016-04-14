using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace buckslice {
    public class GameManager : MonoBehaviour {
        public BunnyController bunny;
        public Image overlay;
        public Text gameTimer;
        public Text speedometer;
        public Gradient speedColor;

        private bool gameOver = false;
        private float maxRecordedSpeed;

        void Start() {
            Time.timeScale = 0.0f;
            StartCoroutine(instructions());
        }

        private IEnumerator instructions() {
            RectTransform rt = gameTimer.rectTransform;
            rt.SetAsLastSibling();
            Vector2 amin = rt.anchorMin;
            Vector2 amax = rt.anchorMax;
            rt.anchorMin = Vector2.one * 0.1f;
            rt.anchorMax = Vector2.one * 0.9f;
            gameTimer.text = "WABBIT SLIDE\n\nMove left or right\n\nHit triple digit speed to win!";

            Color c = overlay.color;
            c.a = 0.5f;
            overlay.color = c;

            while (!Input.anyKeyDown) {
                yield return null;
            }

            rt.SetAsFirstSibling();
            rt.anchorMin = amin;
            rt.anchorMax = amax;

            c.a = 0.0f;
            overlay.color = c;

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
                    speedometer.color = speedColor.Evaluate(bunnySpeed / 100f);
                    Vector3 offsetMin = srt.offsetMin;
                    Vector3 offsetMax = srt.offsetMax;
                    float growth = Mathf.Max(0.0f, Mathf.Min((bunnySpeed - 20.0f) / (100.0f - 20.0f), 1.0f));
                    growth = growth * growth * 200.0f;
                    offsetMin.x = offsetMax.x = (Mathf.PerlinNoise(Time.time * 20f, 0f) - .5f) * growth;
                    offsetMin.y = offsetMax.y = (Mathf.PerlinNoise(Time.time * 13f, 0f) - .5f) * growth;
                    srt.offsetMin = offsetMin;
                    srt.offsetMax = offsetMax;

                    if (maxRecordedSpeed >= 100.0f) {    // you win!!!
                        gameOver = true;
                        speedometer.text = "You Win!";
                        srt.offsetMin = Vector2.zero;
                        srt.offsetMax = Vector2.zero;
                        srt.anchorMin = new Vector2(0.2f, 0.3f);
                        srt.anchorMax = new Vector2(0.8f, 0.7f);

                        Camera.main.transform.parent = null;
                        Camera.main.GetComponent<AudioSource>().Play();

                        StartCoroutine(fadeAndExit(false, 3.5f));
                    }
                } else {
                    gameOver = true;
                    gameTimer.text = "0.0s";
                    srt.offsetMin = Vector2.zero;
                    srt.offsetMax = Vector2.zero;
                    srt.anchorMin = new Vector2(0.2f, 0.3f);
                    srt.anchorMax = new Vector2(0.8f, 0.7f);
                    speedometer.text = "Too Slow!";
                    StartCoroutine(fadeAndExit(false, 2.0f));
                }
            } else {
                // old bunny dancing code
                float musicAngle = Time.realtimeSinceStartup * 4f * Mathf.PI / 3.031f;
                //float r = Mathf.Sin(musicAngle) - .5f;
                //transform.Rotate(0, r * 2f, 0, Space.World);
                float t = (Mathf.Sin(musicAngle * 2.0f) + 1.0f) / 2.0f;
                speedometer.color = Color.Lerp(speedColor.Evaluate(maxRecordedSpeed / 100f), Color.white, t);
            }
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