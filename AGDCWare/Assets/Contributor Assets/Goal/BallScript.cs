namespace GoalNamespace
{
    using UnityEngine;
    using System.Collections;

    public class BallScript : MonoBehaviour
    {
        public Transform player;
        public float maxFollowSpeed;
        public float shootSpeed;

        private bool following;
        private bool gameOver;

        void Start()
        {
            following = true;
            gameOver = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (gameOver)
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
                GetComponent<Rigidbody2D>().velocity = (this.transform.position - player.position).normalized * shootSpeed;
                following = false;
            }
            if (following)
            {
                if (this.transform.position.y != player.position.y)
                {
                    float scaledFollowSpeed = Mathf.Abs((player.position.y - this.transform.position.y) * maxFollowSpeed);
                    if (scaledFollowSpeed > maxFollowSpeed)
                        scaledFollowSpeed = maxFollowSpeed;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, player.position.y, this.transform.position.z), scaledFollowSpeed * Time.fixedDeltaTime);
                }
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                if (gameOver)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
                else
                {
                    Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                    GetComponent<Rigidbody2D>().velocity = new Vector3(vel.x, -vel.y, vel.z);
                }
            }
            else if (collision.gameObject.CompareTag("Lose"))
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameOver = true;
            }
        }

        public void SetGameOver()
        {
            gameOver = true;
        }

        public bool IsGameOver()
        {
            return gameOver;
        }
    }
}
