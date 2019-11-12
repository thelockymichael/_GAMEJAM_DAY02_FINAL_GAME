using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float speed;
    public Rigidbody2D myRigidbody;
    public float lifeTime;
    private float lifeTimeCounter;

    [Header("Enemy Kill Count")]
    public FloatValue enemyKillCount;

    private void Start() {
        lifeTimeCounter = lifeTime;
    }

    private void Update() {
        lifeTimeCounter -= Time.deltaTime;
        if (lifeTimeCounter <= 0) {
            Destroy(gameObject);
        }
    }

    //public void Setup(Vector2 velocity, Vector3 direction) {
    //    myRigidbody.velocity = velocity.normalized * speed;

    //    // Manipulation of rotation in object
    //    // Quaternion euler is a way to find the direction
    //    // that some object is facing in 3D space
    //    transform.rotation = Quaternion.Euler(direction);
    //}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Destroy(this.gameObject);
        }
    }
}