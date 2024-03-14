using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator animator;

    [Header("Default")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private int jumpTime = 1;
    [SerializeField] private float speedOffsetRound = 0.00001f;
    [SerializeField] private float speedRate = 1f;
    float _speedXAxis = 0f;
    float _speedYAxis = 0f;

    [Space(5)]
    [Header("Rotate")]
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float maxRotateAngle = 90f;
    [SerializeField] private float minRotateAngle = -90f;
    private float rotateXAxis = 0f;
    private float rotateYAxis = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.TryGetComponent<Animator>(out animator))
            {
                break;
            }
        }
    }
    private void Update()
    {

    }

    public void Movement(Vector2 input)
    {
        float speed = 0f;
        Vector3 dir = new Vector3(input.x, 0f, input.y).normalized;
        if (dir.sqrMagnitude >= 0.1f)
        {
            speed = moveSpeed;
            if (playerInput.onFoot.Run.IsPressed())
            {
                speed = runSpeed;
            }
            controller.Move(speed * Time.deltaTime * transform.TransformDirection(dir));
        }
        float targetYAxis = input.y == 0 ? 0f : speed == moveSpeed ? 0.5f * input.y : 1f * input.y;
        float targetXAxis = input.x == 0 ? 0f : speed == moveSpeed ? 0.5f * input.x : 1f * input.x;
        if (_speedXAxis + speedOffsetRound < targetXAxis || _speedXAxis - speedOffsetRound > targetXAxis)
        {
            _speedXAxis = Mathf.Lerp(_speedXAxis, targetXAxis, Time.deltaTime * speedRate);
        }
        else
        {
            _speedXAxis = targetXAxis;
        }

        if (_speedYAxis + speedOffsetRound < targetXAxis || _speedYAxis - speedOffsetRound > targetXAxis)
        {
            _speedYAxis = Mathf.Lerp(_speedYAxis, targetYAxis, Time.deltaTime * speedRate);
        }
        else
        {
            _speedYAxis = targetYAxis;
        }

        float horizontal = _speedXAxis;
        float vertical = _speedYAxis;
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }
    public void Rotate(Vector2 input)
    {
        Vector3 rotDir = new Vector3(input.x * mouseSensitivity * Time.deltaTime, input.y * mouseSensitivity * Time.deltaTime, 0f);
        rotateXAxis += rotDir.x;
        rotateYAxis += rotDir.y;
        rotateYAxis = ClamAngle(rotateYAxis, minRotateAngle, maxRotateAngle);
        rotateXAxis = ClamAngle(rotateXAxis, float.MinValue, float.MaxValue);
        mainCamera.transform.localRotation = Quaternion.Euler(rotateYAxis, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotateXAxis, 0f);
    }
    public float ClamAngle(float angle, float minAngle, float maxAngle)
    {
        if (angle > 360f) angle -= 360f;
        if (angle < -360f) angle += 360f;
        return Mathf.Clamp(angle, minAngle, maxAngle);
    }
    public void Jump()
    {

    }
}
