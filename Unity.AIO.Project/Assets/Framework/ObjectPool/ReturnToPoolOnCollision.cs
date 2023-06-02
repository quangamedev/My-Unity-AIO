using UnityEngine;

namespace Framework.Samples.ObjectPool
{
    public class ReturnToPoolOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            this.ReturnToPool();
        }
    }
}