namespace GoalNamespace
{
    using UnityEngine;
    using System.Collections;

    public class GoalieScript : MonoBehaviour
    {
        public GameObject ball;
        public float maxFollowSpeed;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (this.transform.position.y != ball.transform.position.y && !ball.GetComponent<BallScript>().IsGameOver())
            {
                float scaledFollowSpeed = Mathf.Abs((ball.transform.position.y - this.transform.position.y) * maxFollowSpeed * 0.5f);
                if (scaledFollowSpeed > maxFollowSpeed)
                    scaledFollowSpeed = maxFollowSpeed;
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, ball.transform.position.y, this.transform.position.z), scaledFollowSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
