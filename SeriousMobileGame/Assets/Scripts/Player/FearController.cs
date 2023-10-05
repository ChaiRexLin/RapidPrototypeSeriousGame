using UnityEngine;


public class FearController : MonoBehaviour {

    public static FearController Instance { get; private set; }

    //public event EventHandler OnFearChanged;

    [SerializeField] private float range = 5f;
    [SerializeField] private float fearMax = 50f;
    [SerializeField] private float fearMultiplier = 0.2f;
    [SerializeField] private float fearLoss = 1f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private DeerController deerController;


    private Collider[] colliderArray;
    private Vector3 runDirection;
    private bool isFeared = false;
    private float currentFear;
    private int amountOfHumans = 0;

    private float turnTimer = 0;

    private void Start() {
        Instance = this;
    }

    private void Update() {
        if (isFeared) {

            Quaternion awayRotation = Quaternion.LookRotation(runDirection, transform.forward);
            Vector3 euler = awayRotation.eulerAngles;
            euler.y -= 180;
            awayRotation = Quaternion.Euler(euler);

            if (turnTimer >= 1) {
                // move
                deerController.isMoving = true;
                deerController.PlayMovementSound();
                turnTimer += Time.deltaTime;
                transform.position += transform.forward * Time.deltaTime * movementSpeed;

                if (turnTimer >= 4) {
                    isFeared = false;
                    deerController.isActive = true;
                    deerController.isMoving = false;
                    currentFear = 0;
                    turnTimer = 0;
                }

            } else {
                // rotate
                turnTimer += Time.deltaTime;
                Debug.Log(turnTimer / 2);
                transform.rotation = Quaternion.Lerp(transform.rotation, awayRotation, turnTimer / 6);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            }

            return;
        }

        colliderArray = Physics.OverlapSphere(transform.position, range);
        amountOfHumans = 0;
        foreach (Collider collider in colliderArray) {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Human")) {
                amountOfHumans++;
            }
        }
        if (amountOfHumans == 0) { 
            if (currentFear - fearLoss * Time.deltaTime <= 0) {
                currentFear = 0;
            } else {
                currentFear -= fearLoss * Time.deltaTime;
            }
            return; 
        }
        float amountOfFear = amountOfHumans * fearMultiplier * Time.fixedDeltaTime;
        if (currentFear + amountOfFear >= fearMax) {
            currentFear = fearMax;
        } else {
            currentFear += amountOfFear;

        }
        if (currentFear >= fearMax) {
            //Debug.Log("SPOOKED");
            //rb.AddForce(Vector3.up * 5);
            isFeared = true;
            deerController.isActive = false;
            foreach (Collider collider in colliderArray) {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Human")) {
                    Transform humanTransform = collider.gameObject.transform;
                    runDirection = humanTransform.position - transform.position;
                    runDirection.y = 0;
                    return;
                }
            }
        }
    }



    public float GetNormalizedFear() {
        return currentFear / fearMax;
    }

}