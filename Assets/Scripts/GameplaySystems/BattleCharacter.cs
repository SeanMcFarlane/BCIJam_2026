using System.Collections;
using UnityEngine;


public class BattleCharacter : MonoBehaviour {
	public RectTransform HealthbarFillTransform;
	public string CharacterName = "CharacterName";
	public int Health = 100;
	public int MaxHealth = 100;

	//Set reference manually in prefab
	public Animator CharacterAnimator;

	public CharacterAbility[] EquippedAbilities;

	public void StartUsingAbility(CharacterAbility Ability, BattleCharacter Target) {
		StartCoroutine(PlayAbilityAnimation(Ability, Target));
	}

	private IEnumerator PlayAbilityAnimation(CharacterAbility Ability, BattleCharacter Target) {
		BattleActor battleActor = gameObject.GetComponent<BattleActor>();
		yield return StartCoroutine(battleActor.AttackRoutine());
		CompleteUsingAbilityAndApplyEffects(Ability, Target);
	}

	public void CompleteUsingAbilityAndApplyEffects(CharacterAbility Ability, BattleCharacter Target) {
		Target.Health -= Ability.Damage;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		HealthbarFillTransform.anchorMax = new Vector2(0.995f*((float)Health/(float)MaxHealth), HealthbarFillTransform.anchorMax.y);
	}
}