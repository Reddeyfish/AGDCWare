namespace GoalNamespace
{
    using UnityEngine;
    using System.Collections;

    public class GoalScript : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                GameState.state = GameState.States.Won;
            }
        }
    }
}