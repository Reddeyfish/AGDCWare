namespace GoalNamespace
{
    using UnityEngine;
    using System.Collections;

    public class PlayerMove : MonoBehaviour
    {
        public float maxMoveSpeed;

        // Update is called once per frame
        void FixedUpdate()
        {
            this.transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * maxMoveSpeed * Time.fixedDeltaTime, 0f));
        }
    }
}
