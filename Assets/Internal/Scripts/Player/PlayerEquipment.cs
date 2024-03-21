using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerEquipment : NetworkBehaviour
{
    private CharacterConfig characterConfig;
    private bool interact = false;

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            InteractWithFlashLight(interact);
            interact = !interact;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            characterConfig.HandHoldAction(interact);
            interact = !interact;
        }
    }
    public void InteractWithFlashLight(bool v)
    {
        if (characterConfig == null)
        {
            characterConfig = GetComponent<PlayerMovement>().GetCharacterConfig();
        }
        characterConfig.FlashLightAction(v);
    }

}
