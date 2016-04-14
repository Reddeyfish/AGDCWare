using UnityEngine;
using System.Collections;

namespace DerekEdrich
{

    public class LoadScene : MonoBehaviour
    {

        void Start()
        {
            AGDCWareFramework.LoadNextGame();
        }
    }
}