namespace GoalNamespace
{
    using UnityEngine;
    using UnityEngine.UI;

    public class GameState : MonoBehaviour {
        public enum States {
            Unkicked,
            Kicked,
            Won,
            Lost
        }
        public static States state = States.Unkicked;
        public Text timerText;
        public Text lifeText;
        public Camera cam;
        public ParticleSystem partOne;
        public ParticleSystem partTwo;

        private States prevState = States.Unkicked;
        private static Color[] colors = { Color.yellow, Color.magenta, Color.green, Color.red, Color.blue };
        private Color chosenColor;
        private int colorIndex = 0;
        private float grayLevel = 0f;
        private float angle;
        private Vector2 textPos;
        private Vector3 camPos;
        private float screenShake = 0;

        void Start()
        {
            AGDCWareFramework.resetTimer(6);
            state = States.Unkicked;
            prevState = States.Unkicked;
            chosenColor = colors[0];
            textPos = new Vector2(timerText.rectTransform.anchoredPosition.x, timerText.rectTransform.anchoredPosition.y);
            camPos = cam.transform.position;
        }

        void Update()
        {
            lifeText.text = "Lives: " + AGDCWareFramework.getCurrentLives().ToString();

            if (screenShake > 0)
            {
                cam.transform.position = new Vector3(camPos.x + Random.Range(-screenShake, screenShake), camPos.y + Random.Range(-screenShake, screenShake), camPos.z);
                screenShake -= 3 * Time.deltaTime;
            }
            else
            {
                screenShake = 0;
            }
            if (AGDCWareFramework.timeRemaining() <= 0)
            {
                if (state != States.Won && state != States.Lost)
                    state = States.Lost;
                else
                    AGDCWareFramework.LoadNextGame();
            }
            if (state != prevState)
            {
                if (state == States.Lost)
                    DoLoseSequence();
                else if (state == States.Won)
                {
                    DoWinSequence();
                }
            }

            if (state == States.Won)
            {
                if (timerText.color.Equals(chosenColor))
                    chosenColor = colors[colorIndex++ % 4];
                Vector3 c = Vector3.MoveTowards(new Vector3(timerText.color.r, timerText.color.g, timerText.color.b), new Vector3(chosenColor.r, chosenColor.g, chosenColor.b), 6 * Time.deltaTime);
                timerText.color = new Color(c.x, c.y, c.z);
                timerText.rectTransform.anchoredPosition = new Vector2(textPos.x + Random.Range(-6, 6), textPos.y + Random.Range(-6, 6));
            }
            else if (state == States.Lost)
            {
                grayLevel = 0.15f + 0.2f * Mathf.Abs(Mathf.Sin(angle));
                angle += 1.5f * Mathf.PI * Time.deltaTime;
                timerText.color = new Color(grayLevel, grayLevel, grayLevel);
            }
            else
            {
                grayLevel = 0.3f * Mathf.Abs(Mathf.Sin(angle));
                angle += 1.5f * Mathf.PI * Time.deltaTime;
                timerText.color = new Color(grayLevel, 0.6f * grayLevel, 0);

                if (timerText.text == "3" || timerText.text == "2")
                {
                    grayLevel = 0.2f + 0.4f * Mathf.Abs(Mathf.Sin(angle));
                    angle += 2 * Mathf.PI * Time.deltaTime;
                    timerText.color = new Color(grayLevel, 0.6f * grayLevel, 0);
                    if (timerText.text == "3")
                    {
                        timerText.fontSize = 90;
                        timerText.rectTransform.anchoredPosition = new Vector2(textPos.x + Random.Range(-1, 1), textPos.y + Random.Range(-1, 1));
                    }
                    else
                    {
                        timerText.fontSize = 105;
                        timerText.rectTransform.anchoredPosition = new Vector2(textPos.x + Random.Range(-2, 2), textPos.y + Random.Range(-2, 2));
                    }
                }
                else if (timerText.text == "1")
                {
                    grayLevel = Mathf.Abs(Mathf.Sin(angle));
                    angle += 6 * Mathf.PI * Time.deltaTime;
                    timerText.color = new Color(1, grayLevel, 0);
                    timerText.fontSize = 120;
                    timerText.rectTransform.anchoredPosition = new Vector2(textPos.x + Random.Range(-4, 4), textPos.y + Random.Range(-4, 4));
                }
                timerText.text = Mathf.CeilToInt(AGDCWareFramework.timeRemaining()).ToString();
            }
            prevState = state;
        }

        void DoLoseSequence()
        {
            AGDCWareFramework.resetTimer(2);
            timerText.text = "Miss...";
            timerText.fontSize = 50;
            AGDCWareFramework.setMaxLives(AGDCWareFramework.getCurrentLives() - 1);
        }

        void DoWinSequence()
        {
            AGDCWareFramework.resetTimer(2);
            timerText.color = colors[0];
            timerText.text = "GOAL!";
            timerText.fontSize = 125;
            screenShake = 3;
            partOne.Play();
            partTwo.Play();
        }
    }
}