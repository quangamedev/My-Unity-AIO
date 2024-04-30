using UnityEngine;

namespace Framework.Samples.ObjectPool
{
    public class ObjectPoolDemo : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _spawnInterval = 0.5f;
        [SerializeField] private ReturnToPoolOnCollision pooledVariantA;
        [SerializeField] private ReturnToPoolOnCollision pooledVariantB;
        [SerializeField] private ReturnToPoolOnCollision pooledVariantC;

        private float _nextSpawnTime;

        #endregion

        #region Unity Methods

        private void Start() { }

        void Update()
        {
            if (Time.time > _nextSpawnTime)
            {
                var A = pooledVariantA.SpawnFromPool();
                var B = pooledVariantB.SpawnFromPool();
                var C = pooledVariantC.SpawnFromPool();

                A.transform.position = new Vector3(getRandomInRange(), 10, getRandomInRange());
                B.transform.position = new Vector3(getRandomInRange(), 10, getRandomInRange());
                C.transform.position = new Vector3(getRandomInRange(), 10, getRandomInRange());

                _nextSpawnTime = Time.time + _spawnInterval;
            }

            float getRandomInRange()
            {
                return Random.Range(-8, 9);
            }
        }

        #endregion
    }
}