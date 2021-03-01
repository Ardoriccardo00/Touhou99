using UnityEngine;
using Mirror;
using TMPro;

public class PlayerUIDisplay : NetworkBehaviour
{
    [Header("Setup")]
    PlayerHealth healthReference = null;
    PlayerWeapon weaponReference = null;

    [SerializeField] TextMeshProUGUI healthText = null;
	[SerializeField] TextMeshProUGUI bombText = null;

	private void OnEnable()
	{
		healthReference = GetComponent<PlayerHealth>();
		weaponReference = GetComponent<PlayerWeapon>();

		healthReference.EventHealthChanged += HandleHealthChange;
		weaponReference.EventBombPowerChanged += HandleBombChange;
	}

	private void OnDisable()
	{
		healthReference.EventHealthChanged -= HandleHealthChange;
	}

	[ClientRpc]
	private void HandleHealthChange(float currentHealth, float maxHealth)
	{
		healthText.text = currentHealth + "/" + maxHealth;
	}

	[ClientRpc]
	private void HandleBombChange(float bombPower, float bombMax)
	{
		bombText.text = bombPower + "%";
	}
}
