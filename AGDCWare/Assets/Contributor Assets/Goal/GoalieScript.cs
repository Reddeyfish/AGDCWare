namespace GoalNamespace
{
    using UnityEngine;
    using System.Collections;

    public class GoalieScript : MonoBehaviour
    {
        public GameObject ball;
        public float maxFollowSpeed;

        // FixedUpdate is called once per physics update
        void FixedUpdate()
        {
            // Follow the ball's movement
            if (GameState.state != GameState.States.Won && GameState.state != GameState.States.Lost)
            {
                float scaledFollowSpeed = Mathf.Abs((ball.transform.position.y - this.transform.position.y) * maxFollowSpeed * 0.5f);
                if (scaledFollowSpeed > maxFollowSpeed)
                    scaledFollowSpeed = maxFollowSpeed;
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, ball.transform.position.y, this.transform.position.z), scaledFollowSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
