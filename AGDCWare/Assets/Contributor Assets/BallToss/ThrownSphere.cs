﻿using UnityEngine;
using System.Collections;

namespace DerekEdrich
{
    public class ThrownSphere : MonoBehaviour
    {

        [SerializeField]
        protected GameObject soundSpherePrefab;

        void OnCollisionEnter(Collision col)
        {
            if (col.impulse.sqrMagnitude > 1)
            {
                    Instantiate(soundSpherePrefab, col.contacts[0].point, Quaternion.identity);
            }
        }

        void Update()
        {
            if (transform.position.y < -10)
                Destroy(this.gameObject);
        }
    }
}