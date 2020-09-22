using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPhysics : MonoBehaviour
{
    public float smoothingAmount = 15.0f;
    public Transform target;

    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;

    private Rigidbody rigidBody = null;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // när skriptet körs, så ser det här till att handen är exakt där det ska vara från start
        TeleportToTarget();
    }

    private void Update()
    {
        SetTargetPosition();
        SetTargetRotation();
    }

    private void SetTargetPosition()
    {
        float time = smoothingAmount * Time.unscaledDeltaTime;
        targetPosition = Vector3.Lerp(targetPosition, target.position, time);

    }

    private void SetTargetRotation()
    {
        // Det här är vart vi är nu och vart vi ska/vill göra, och detta avgör hur många steg vi kan gå åt det hållet

        float time = smoothingAmount * Time.unscaledDeltaTime;
        targetRotation = Quaternion.Slerp(targetRotation, target.rotation, time);
    }

    private void FixedUpdate()
    {
        // Eftersom vi har en rigidbody så måste vi köra fixedupdate
        MoveToController();
        RotateToController();
    }

    private void MoveToController()
    {
        Vector3 positionDelta = targetPosition - transform.position;
        rigidBody.velocity = Vector3.zero;
        rigidBody.MovePosition(transform.position + positionDelta);
    }

    private void RotateToController()
    {
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.MovePosition(targetPosition);
    }

    public void TeleportToTarget()
    {
        targetPosition = target.position;
        targetRotation = target.rotation;

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
