using UnityEngine;
using System.Collections;

namespace DerekEdrich
{
    public class BallToss : MonoBehaviour
    {

        [SerializeField]
        protected GameObject thrownSpherePrefab;

        [SerializeField]
        protected float velocity;

        [SerializeField]
        protected Transform[] targets;

        Vector3[] initialPositions;

        float nextBulletTime;
        Countdown victoryCountdown;

        // Use this for initialization
        void Start()
        {
            Callback.FireAndForget(AGDCWareFramework.LoadNextGame, 30, this);
            nextBulletTime = Time.timeSinceLevelLoad;
            initialPositions = new Vector3[targets.Length];
            for (int i = 0; i < targets.Length; i++)
            {
                initialPositions[i] = targets[i].position;
            }
            victoryCountdown = new Countdown(victoryCountdownRoutine, this);
        }

        IEnumerator victoryCountdownRoutine()
        {
            return Callback.Routines.FireAndForgetRoutine(AGDCWareFramework.LoadNextGame, 2, this);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown && Time.timeSinceLevelLoad >= nextBulletTime)
            {
                nextBulletTime = Time.timeSinceLevelLoad + 1;
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = velocity;
                mousePos = mousePos.toWorldPoint() - transform.position;
                GameObject instantiatedSphere = Instantiate(thrownSpherePrefab, transform.position, Quaternion.identity) as GameObject;
                instantiatedSphere.GetComponent<Rigidbody>().velocity = mousePos;
            }

            bool victory = true;

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].position.y > initialPositions[i].y - 0.25f)
                    victory = false;
            }

            if (victory)
            {
                victoryCountdown.Play();
            }
        }
    }
}