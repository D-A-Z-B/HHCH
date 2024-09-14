using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour {
    [SerializeField] private ExplosionEffect explosionEffectPrefab; // 나중에 풀매니저 생기면
    private Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private Coroutine coroutine;
    public void Shoot(float power) {
        coroutine = StartCoroutine(ShootRoutine(power));
    }

    public IEnumerator ShootRoutine(float power) {
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        DestroyObject();
    }

    private void DestroyObject() {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}