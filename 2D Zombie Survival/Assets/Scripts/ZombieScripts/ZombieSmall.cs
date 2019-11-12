using UnityEngine;

public class ZombieSmall : Zombie {
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    // Start is called before the first frame update
    void Start() {
        //currentState = EnemyState.idle;
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    void FixedUpdate() {
        CheckDistace();
    }

    public virtual void CheckDistace() {

        Vector3 difference = target.position - transform.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        //if (Vector3.Distance(target.position, transform.position) <= chaseRadius
        //    && Vector3.Distance(target.position, transform.position) > attackRadius) {

        //if (currentState == EnemyState.idle || currentState == EnemyState.walk
        //    && currentState != EnemyState.stagger) {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        //changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);
        //ChangeState(EnemyState.walk);
        //anim.SetBool("wakeUp", true);
        //    }
        //} else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
        //        anim.SetBool("wakeUp", false);
        //    }
    }
}
//public void changeAnim(Vector2 direction) {
//    //Debug.Log(direction);
//    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
//        if (direction.x > 0) {
//            SetAnimFloat(Vector2.right);
//        } else if (direction.x < 0) {
//            SetAnimFloat(Vector2.left);
//        }
//    } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
//        if (direction.y > 0) {
//            SetAnimFloat(Vector2.up);
//        } else if (direction.y < 0) {
//            SetAnimFloat(Vector2.down);
//        }
//    }
//}

