using System.Collections;

using UnityEngine;

public class Zombie : MonoBehaviour {
    [Header("Enemy stats")]
    public FloatValue maxHealth;
    public float health;
    public int baseAttack;
    public int moveSpeed;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;
    //public LootTable thisLoot;
    [Header("Unity Component")]
    protected Animator anim;
    protected Rigidbody2D myRigidbody;

    [Header("Enemy Kill Count")]
    public FloatValue enemyKillCount;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void OnEnable() {
        health = maxHealth.initialValue;

    }

    private void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DeathEffect();
            //MakeLoot();
            //if (roomSignal)
            //    roomSignal.Raise();
            gameObject.SetActive(false);
        }
    }

    private void DeathEffect() {
        if (deathEffect) {
            enemyKillCount.initialValue += 1;
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage) {
        Debug.Log("Knocked");
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    public IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime) {
        if (myRigidbody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            //currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
            //enemy.isKinematic = true;
        }
    }
}
