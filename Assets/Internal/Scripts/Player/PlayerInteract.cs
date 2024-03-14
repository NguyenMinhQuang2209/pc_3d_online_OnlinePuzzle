using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    private PlayerInput playerInput;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        Interact();
    }
    public void Interact()
    {
        InteractController.instance.InteractText("");
        Vector2 centerPoint = new(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(centerPoint);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject.TryGetComponent<Interactible>(out var interact))
            {
                InteractController.instance.InteractText(interact.promptMessage);
                if (playerInput.onFoot.Interact.triggered)
                {
                    interact.BaseInteract();
                }
            }
        }
    }
}
