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

        float nextBulletTime;

        // Use this for initialization
        void Start()
        {
            Callback.FireAndForget(AGDCWareFramework.LoadNextGame, 30, this);
            nextBulletTime = Time.timeSinceLevelLoad;
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
        }
    }
}