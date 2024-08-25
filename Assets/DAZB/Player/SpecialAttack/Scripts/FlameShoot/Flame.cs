using UnityEngine;

public class Flame : MonoBehaviour {
    [SerializeField] private ExplosionEffect explosionEffectPrefab; // 나중에 풀매니저 생기면
    private Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 dir, float power) {
        rigid.AddForce(dir * power, ForceMode2D.Impulse);
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