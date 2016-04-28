using UnityEngine;
using System.Collections;

namespace AndresGonzalez
{
    public class Turtle : MonoBehaviour
    {
        public float speed = 1.0f;

        private int team;
        private SpriteRenderer _SpRen;

        void Start()
        {
            _SpRen = GetComponent<SpriteRenderer>();
            //Assign Color
            TeamSwap(Mathf.CeilToInt(Random.value * 3));
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position += new Vector3(0, speed * Time.fixedDeltaTime, 0);
        }

        public void TeamSwap(int newTeam)
        {
            if(newTeam == 1) // red
            {
                _SpRen.color = Color.red;
            }
            else if(newTeam == 2) // green
            {
                _SpRen.color = Color.green;
            }
            else if(newTeam == 3) // blue
            {
                _SpRen.color = Color.blue;
            }
            this.team = newTeam;
        }
    }
}
