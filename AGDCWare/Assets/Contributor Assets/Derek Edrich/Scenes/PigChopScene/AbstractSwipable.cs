using UnityEngine;
using System.Collections;

namespace DerekEdrich
{

    [RequireComponent(typeof(Collider2D))]
    public abstract class AbstractSwipable : MonoBehaviour, ISwipable
    {
        public abstract void Notify(SwipableMessage m);
    }
}