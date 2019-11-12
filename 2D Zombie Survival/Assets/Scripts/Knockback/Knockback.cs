using UnityEngine;

public class Knockback : MonoBehaviour {
    public float thrust = 1.5f;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Enemy") || other.CompareTag("Player")) {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit) {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.CompareTag("Enemy") && other.isTrigger) {
                    //hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Zombie>().Knock(hit, knockTime, damage);
                }
                if (other.CompareTag("Player")) {
                    //if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger) {
                    //    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                }
            }
        }
    }
}
