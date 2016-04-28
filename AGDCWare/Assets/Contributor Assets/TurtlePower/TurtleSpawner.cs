using UnityEngine;
using System.Collections;

namespace AndresGonzalez
{
    public class TurtleSpawner : MonoBehaviour
    {
        public GameObject turtlepfab;
        public float TurtlesPerSecond;
        private Bounds debox;

        void Start()
        {
            debox = GetComponent<BoxCollider2D>().bounds;
        }

        void FixedUpdate()
        {
            //spawnTurtle();
        }

        public void spawnTurtle()
        {
            GameObject turtle = Instantiate(turtlepfab) as GameObject;
            //Get random location within collider box
            turtle.transform.position = getRandWithinBox();
        }

        public Vector2 getRandWithinBox()
        {
            return (Vector2)debox.center + new Vector2(Random.Range(-debox.extents.x, debox.extents.x), Random.Range(-debox.extents.y, debox.extents.y));
        }
    }
}
