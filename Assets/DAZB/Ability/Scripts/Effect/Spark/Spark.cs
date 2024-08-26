using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spark: MonoBehaviour {
    public LayerMask EnemyLayer;
    public ExplosionEffect ExplosionPrefab;
    public float MoveSpeed;
    public float LifeTime;
    private Transform target;

    private Rigidbody2D rigid;
    private float delayTime;

    private Collider2D collider;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    Coroutine routine;
    private void Start() {
        routine = StartCoroutine(SparkRoutine());
    }

    public void SetDelayTime(float value) {
        delayTime = value;
    }

    private IEnumerator SparkRoutine() {
        rigid.AddForce(Random.insideUnitCircle * 20, ForceMode2D.Impulse);
        yield return new WaitForSeconds(delayTime);
        FindEnemy();
        collider.enabled = true;
        Vector2 starPos = transform.position;
        Vector2 endPos = target.transform.position;

        if (target == null) {
            Destroy(gameObject, LifeTime);
            yield break;
        }

        float currentTime = 0;
        float totalTime = 1 / MoveSpeed;
        while (true) {
            endPos = target.transform.position;
            if (Vector2.Distance(transform.position, endPos) <= 0.1f) {
                Destroy(gameObject, LifeTime);
                yield break;
            }
            float t = currentTime / totalTime;
            transform.position = Vector2.Lerp(starPos, endPos, t);
            yield return null;
            currentTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        DestroyObject();
        StopCoroutine(routine);
    }

    private void DestroyObject() {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private Collider2D[] findEnemiesResult = new Collider2D[10];
    private void FindEnemy() {
        Transform shortestTarget = null;
        float shortestDistance = float.MaxValue;
        int enemiesCount = Physics2D.OverlapCircleNonAlloc(transform.position, 50, findEnemiesResult, EnemyLayer);
        if (enemiesCount > 0) {
            for (int i = 0; i < enemiesCount; i++) {
                Collider2D iter = findEnemiesResult[i];
                if (iter != null) {
                    float distance = Vector2.Distance(iter.transform.position, transform.position);
                    if (shortestDistance > distance) {
                        shortestDistance = distance;
                        shortestTarget = iter.transform;
                    }
                }
            }
        }
        target = shortestTarget;
    }

}