using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterConfig : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;
    public Transform head;
    public Transform hat;
    public Transform head_aim;

    private Animator animator;

    [Header("FlashLight")]
    [SerializeField] private GameObject flashLight;
    [SerializeField] private Rig hand_hold_rig;
    [SerializeField] private int hand_hold_layer_index;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void FlashLightAction(bool v)
    {
        flashLight.SetActive(v);
        hand_hold_rig.weight = v ? 1f : 0f;
        animator.SetLayerWeight(hand_hold_layer_index, v ? 1f : 0f);
    }
}
