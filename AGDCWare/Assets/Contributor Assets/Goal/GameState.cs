namespace GoalNamespace
{
    using UnityEngine;
    public class GameState : MonoBehaviour {
        public enum States {
            Unkicked,
            Kicked,
            Won,
            Lost
        }
        public static States state = States.Unkicked;
        private static States prevState = States.Unkicked;

        void Start()
        {
            AGDCWareFramework.resetTimer(10);
            state = States.Unkicked;
        }

        void Update()
        {
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
                    DoWinSequence();
            }

            prevState = state;
        }

        void DoLoseSequence()
        {
            AGDCWareFramework.resetTimer(3);
            Debug.Log("LOSE");
        }

        void DoWinSequence()
        {
            AGDCWareFramework.resetTimer(3);
            Debug.Log("WIN");
        }
    }
}