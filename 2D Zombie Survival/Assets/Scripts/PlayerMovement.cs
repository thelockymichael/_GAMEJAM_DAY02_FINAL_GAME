using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Start is called before the first frame update

    // Player Movement and Physics
    [Header("Player Movement and Physics")]
    public float speed;
    private float initSpeed;
    private float runSpeed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator anim;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform bulletSpawn;
    private Vector3 mousePosition;
    public float bulletSpeed = 20f;
    //public GameObject crosshairs;
    //private Vector3 target;

    [Header("Player Health")]
    public FloatValue currentHealth;


    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        initSpeed = speed;
        runSpeed = initSpeed * 2;
    }

    // Update is called once per frame
    void FixedUpdate() {
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // convert mouse position into world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 difference = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);

        //Debug.Log("Mouse screen position: " + mousePosition);
        //// get direction you want to point at
        //Vector3 direction = (mousePosition - (Vector3)transform.position).normalized;

        //// set vector of transform directly
        //transform.up = direction;

        if (Input.GetButton("attack")) {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            ShootBullets(direction, rotationZ);
        } else if (Input.GetButton("run")) {
            Debug.Log("Why are you running?");
            speed = runSpeed;
        } else {
            speed = initSpeed;
        }

        UpdateAnimationMove();
    }
    private void ShootBullets(Vector2 direction, float rotationZ) {
        GameObject b = Instantiate(bullet) as GameObject;
        b.transform.position = bulletSpawn.transform.position;
        b.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

    }
    Vector3 ChooseArrowDirection() {
        float temp = Mathf.Atan2(new Vector2(0f, Input.mousePosition.y).y, new Vector2(Input.mousePosition.x, 0f).x) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    void UpdateAnimationMove() {
        if (change != Vector3.zero) {
            MoveCharacter();
        }
    }

    void MoveCharacter() {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }


    public void Knock(float knockTime, float damage) {
        Debug.Log("You have hit player");

        currentHealth.RunTimeValue -= damage;
        //playerHealthSignal.Raise();
        if (currentHealth.RunTimeValue > 0) {
            //playerHealthSignal.Raise();
            StartCoroutine(KnockCo(knockTime));
        } else {
            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator KnockCo(float knockTime) {
        //playerHit.Raise();
        if (myRigidbody) {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            //currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;

            //enemy.isKinematic = true;
        }
    }

}

//private void MakeArrow() {
//    if (playerInventory.currentMagic > 0) {
//        Vector2 temp = new Vector2(anim.GetFloat("moveX"), anim.GetFloat("moveY"));
//        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
//        arrow.Setup(temp, ChooseArrowDirection());
//        playerInventory.ReduceMagic(arrow.magicCost);
//        reduceMagic.Raise();
//    }
//}




//public float speed;
//public Rigidbody2D myRigidbody;
//public float lifeTime;
//private float lifeTimeCounter;
//public float magicCost;

//private void Start() {
//    lifeTimeCounter = lifeTime;
//}

//private void Update() {
//    lifeTimeCounter -= Time.deltaTime;
//    if (lifeTimeCounter <= 0) {
//        Destroy(gameObject);
//    }
//}

//public void Setup(Vector2 velocity, Vector3 direction) {
//    myRigidbody.velocity = velocity.normalized * speed;

//    // Manipulation of rotation in object
//    // Quaternion euler is a way to find the direction
//    // that some object is facing in 3D space
//    transform.rotation = Quaternion.Euler(direction);
//}

//private void OnTriggerEnter2D(Collider2D other) {
//    if (other.CompareTag("enemy")) {
//        Destroy(this.gameObject);
//    }
//}
