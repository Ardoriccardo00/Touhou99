using UnityEngine;
using Mirror;
using TMPro;

public class PlayerUIDisplay : NetworkBehaviour
{
    [Header("Setup")]
    Health healthReference = null;
    PlayerWeapon weaponReference = null;

    [SerializeField] TextMeshProUGUI healthText = null;
	[SerializeField] TextMeshProUGUI bombText = null;

	private void OnEnable()
	{
		healthReference = GetComponent<Health>();
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
