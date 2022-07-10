using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputValues inputValues;
    [SerializeField] private float armLength;
    [SerializeField] private Vector2 yOffsetRange;
    [SerializeField] [Range(0f, 1f)] private float xSpeed;
    [SerializeField] [Range(0f, 1f)] private float ySpeed;
    [SerializeField] [Range(0.01f, 1f)] private float xDampening;
    [SerializeField] [Range(0.01f, 1f)] private float yDampening;

    private CinemachineTransposer mVCam;
    private float mOrbitTheta;
    private float mGoalOrbitTheta;
    private float mAngleY;
    private float mGoalAngleY;

    private void Start()
    {
        mOrbitTheta = mGoalOrbitTheta = 270 * Mathf.Deg2Rad;
        mVCam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        LerpOrbitTheta();
        LerpAngleY();
        OrbitPlayer();
        UpdateAngleY();
    }

    private void LerpOrbitTheta()
    {
        mGoalOrbitTheta += inputValues.LookDir.x * xSpeed * 0.1f;
        mOrbitTheta = Mathf.Lerp(mOrbitTheta, mGoalOrbitTheta, Time.deltaTime / xDampening);
    }

    private void LerpAngleY()
    {
        mGoalAngleY += inputValues.LookDir.y * ySpeed * 0.1f;
        if (mGoalAngleY < yOffsetRange.x)
            mGoalAngleY = yOffsetRange.x;
        if (mGoalAngleY > yOffsetRange.y)
            mGoalAngleY = yOffsetRange.y;
        mAngleY = Mathf.Lerp(mAngleY, mGoalAngleY, Time.deltaTime / yDampening);
    }

    private void UpdateAngleY()
    {
        mVCam.m_FollowOffset.y = mAngleY;
    }

    private void OrbitPlayer()
    {
        float offsetZ = Mathf.Sin(mOrbitTheta) * armLength;
        float offsetX = Mathf.Cos(mOrbitTheta) * armLength;
        mVCam.m_FollowOffset.z = offsetZ;
        mVCam.m_FollowOffset.x = offsetX;
    }
}
