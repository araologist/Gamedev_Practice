using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public GameObject Object;
    public float Speed;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var obj = Instantiate(Object);
                obj.SetActive(false);
                return obj;
            },
            actionOnGet: (obj) =>
            {
                obj.SetActive(true);
            },
            actionOnRelease: (obj) =>
            {
                obj.SetActive(false);
            },
            actionOnDestroy: (obj) =>
            {
                Destroy(obj);
            },
            collectionCheck: false,
            defaultCapacity: 10
        );
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject obj = pool.Get();
            StartCoroutine(ReleaseDelayed(obj));
            obj.transform.position = Vector2.zero;
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = randomDirection * Speed;
            var degree = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(0, 0, degree);
        }
    }

    private IEnumerator ReleaseDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        pool.Release(obj);
    }
}