namespace GoalNamespace
{
    using UnityEngine;
    using System.Collections;

    public class PlayerMove : MonoBehaviour
    {
        public float maxMoveSpeed;
        public float shootSpeed;
        public BallScript ball;

        void FixedUpdate()
        {
            // Player can move up and down
            this.transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * maxMoveSpeed * Time.fixedDeltaTime, 0f));

            // Game state-specific actions
            switch (GameState.state)
            {
                case GameState.States.Unkicked:

                    // Player shoots the ball
                    if (Input.GetKey(KeyCode.Space))
                    {
                        ball.ShootFrom(this.transform.position, shootSpeed);
                        GameState.state = GameState.States.Kicked;
                    }

                    // Ball follows player
                    else
                    {
                        ball.MoveTo(this.transform.position, Time.fixedDeltaTime);
                    }
                    break;

                case GameState.States.Kicked:

                    // Player can make a face to weird out the Goalie
                    if (Input.GetKey(KeyCode.Space))
                    {

                    }
                    break;
                default: break;
            }
        }
    }
}
