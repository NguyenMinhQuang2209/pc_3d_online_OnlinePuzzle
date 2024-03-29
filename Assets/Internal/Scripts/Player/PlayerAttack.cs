using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;
    [SerializeField] private List<AttackItem> attackItems = new();
    [SerializeField] private int defaultAttackIndex = 0;
    private AttackItem currentAttack;
    private int currentIndex = 0;
    private float currentTimer = 0f;
    private float currentTimeBwtAttack = 0f;

    public static string ATTACK_NAME = "Attack";
    public static string ATTACK_INDEX = "AttackIndex";
    public static string ATTACK_TYPE = "AttackType";

    public override void OnNetworkSpawn()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<PlayerMovement>().GetAnimator();
        currentAttack = attackItems[defaultAttackIndex];
        if (animator != null)
        {
            animator.SetInteger(ATTACK_TYPE, currentAttack.typeIndex);
        }
    }
    private void Update()
    {
        if (!IsOwner) return;
        if (animator == null)
        {
            animator = GetComponent<PlayerMovement>().GetAnimator();
            if (animator != null)
            {
                animator.SetInteger(ATTACK_TYPE, currentAttack.typeIndex);
            }
            return;
        }
        currentTimeBwtAttack = Mathf.Min(currentTimeBwtAttack + Time.deltaTime, currentAttack.timeBwtAttack);
        if (currentIndex > 0 && currentTimeBwtAttack >= currentAttack.timeBwtAttack)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= currentAttack.waitTime)
            {
                ReloadAttack();
            }
        }
        if (currentTimeBwtAttack >= currentAttack.timeBwtAttack && playerInput.onFoot.Attack.triggered)
        {
            animator.SetFloat(ATTACK_INDEX, currentIndex);
            animator.SetTrigger(ATTACK_NAME);
            currentTimer = 0f;
            currentTimeBwtAttack = 0f;
            currentIndex += 1;
            if (currentIndex >= currentAttack.total)
            {
                ReloadAttack();
            }
        }
    }
    public void ReloadAttack()
    {
        currentTimer = 0f;
        currentIndex = 0;
    }
}
[System.Serializable]
public class AttackItem
{
    public AttackType attackType;
    public int total = 1;
    public float waitTime = 1f;
    public float timeBwtAttack = 1f;
    public int typeIndex = 0;
}
