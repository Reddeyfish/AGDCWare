using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

namespace DerekEdrich
{
    public class PigChopScene : MonoBehaviour
    {
        [SerializeField]
        protected GameObject logPrefab;
        [SerializeField]
        protected GameObject pigPrefab;

        [SerializeField]
        protected Transform tutorial;

        [Range(0, 1)]
        [SerializeField]
        protected float pigFrequency;
        [SerializeField]
        protected float gameDurationSeconds;
        [SerializeField]
        protected float secondsPerObject;

        // Use this for initialization
        void Start()
        {
            Assert.IsTrue(gameDurationSeconds > 0);
            Assert.IsTrue(secondsPerObject > 0);
            Assert.IsTrue(secondsPerObject < gameDurationSeconds);
            StartCoroutine(PigChopRoutine());
        }

        IEnumerator PigChopRoutine()
        {
            float time = 0;
            while (!Input.anyKeyDown)
            {
                yield return null;
                time += Time.deltaTime;
            }

            Destroy(tutorial.gameObject);

            while (time < gameDurationSeconds)
            {
                GameObject spawnedChoppable = Random.value < pigFrequency ? SimplePool.Spawn(pigPrefab) : SimplePool.Spawn(logPrefab);

                yield return new WaitForSeconds(secondsPerObject);
                time += secondsPerObject;

                SimplePool.Despawn(spawnedChoppable);
            }
            AGDCWareFramework.LoadNextGame();
        }
    }
}