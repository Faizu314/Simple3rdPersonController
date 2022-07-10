using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] [Range(0.01f, 1f)] private float rotationDampening;
    [SerializeField] private PlayerInputValues inputValues;

    private Transform mTransform;
    private CharacterController mCharController;
    private Transform mCamera;
    private Quaternion mCamRotInXZ;

    void Start()
    {
        mTransform = transform;
        mCamera = Camera.main.transform;
        mCharController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (inputValues.RawMovementDir.sqrMagnitude < 0.001f)
            return;

        CalculateCamRotInXZ();
        Translate();
        Rotate();
    }

    private void CalculateCamRotInXZ()
    {
        Plane plane = new Plane(Vector3.up, mCamera.position);
        Vector3 camToPlayerInXZ = plane.ClosestPointOnPlane(mCamera.position + mCamera.forward);
        camToPlayerInXZ = camToPlayerInXZ - mCamera.position;
        mCamRotInXZ = Quaternion.FromToRotation(Vector3.forward, camToPlayerInXZ);
    }

    private void Translate()
    {
        Vector3 displacementDir = mCamRotInXZ * new Vector3(inputValues.MovementDir.x, 0f, inputValues.MovementDir.y);
        mCharController.Move(Time.deltaTime * movementSpeed * displacementDir);
    }
    private void Rotate()
    {
        Vector3 forwardDir = mCamRotInXZ * new Vector3(inputValues.MovementDir.x, 0f, inputValues.MovementDir.y);
        Quaternion targetRotation = Quaternion.LookRotation(forwardDir, Vector3.up);
        mTransform.rotation = Quaternion.RotateTowards(mTransform.rotation, targetRotation, 100 * Time.deltaTime / rotationDampening);
    }
}
