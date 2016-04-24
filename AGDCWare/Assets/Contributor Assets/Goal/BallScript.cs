namespace GoalNamespace
{
    using UnityEngine;
    using System.Collections;

    public class BallScript : MonoBehaviour
    {
        public float maxFollowSpeed;

        // Shoots the ball
        public void ShootFrom(Vector3 shootFrom, float power)
        {
            GetComponent<Rigidbody2D>().velocity = (this.transform.position - shootFrom).normalized * power;
        }

        // Moves the ball according to some time scale
        public void MoveTo(Vector3 goTo, float timeScale)
        {
            float scaledFollowSpeed = Mathf.Abs((goTo.y - this.transform.position.y) * maxFollowSpeed);
            if (scaledFollowSpeed > maxFollowSpeed)
                scaledFollowSpeed = maxFollowSpeed;
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, goTo.y, this.transform.position.z), scaledFollowSpeed * timeScale);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector3(vel.x, -vel.y, vel.z);

                if (GameState.state == GameState.States.Won || GameState.state == GameState.States.Lost)
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            else if (collision.gameObject.CompareTag("Lose") && GameState.state != GameState.States.Won)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GameState.state = GameState.States.Lost;
            }
        }
    }
}
