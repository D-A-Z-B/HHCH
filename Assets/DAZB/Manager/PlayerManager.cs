using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager> {
    private Body body;
    public Body Body {
        get {
            if (body == null) {
                body = FindObjectOfType<Body>();
                if (body == null) {
                    Debug.LogError("body가 존재하지 않음");
                }
            }
            return body;
        }
    }

    private Head head;
    public Head Head {
        get {
            if (head == null) {
                head = FindObjectOfType<Head>();
                if (head == null) {
                    Debug.LogError("head가 존재하지 않음");
                }
            }
            return head;
        }
    }
}