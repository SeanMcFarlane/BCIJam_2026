using UnityEngine;
using System.Collections;


public class RoboticlashGameManagerComponent : MonoBehaviour {
	public bool IsPlayerTurn = true;
	Animator ShowRunnerAnimator;

	//Set manually
	public BattleCharacter PlayerCharacter;
	public BattleCharacter EnemyCharacter;


	void TriggerNextTurn() {
		//TODO: Play an animation, wait for completion, and THEN call start next turn, rather than right away
		//ShowRunnerAnimator.Play("Animation");
		StartNextTurn();
	}

	void StartNextTurn() {
		IsPlayerTurn = !IsPlayerTurn;

		if(!IsPlayerTurn) {
			//Enemy uses a random ability on the player
			StartUsingAbility(EnemyCharacter, EnemyCharacter.EquippedAbilities[Random.Range(0, EnemyCharacter.EquippedAbilities.Length-1)], PlayerCharacter);
		}
	}

	public void StartUsingAbility(BattleCharacter User, CharacterAbility Ability, BattleCharacter Target) {
		StartCoroutine(PlayAbilityAnimation(User, Ability, Target));
	}

	private IEnumerator PlayAbilityAnimation(BattleCharacter User, CharacterAbility Ability, BattleCharacter Target) {
		BattleActor abilityUserBattleActor = User.GetComponent<BattleActor>();
		yield return StartCoroutine(abilityUserBattleActor.AttackRoutine());
		CompleteUsingAbilityAndApplyEffects(User, Ability, Target);
	}

	public void CompleteUsingAbilityAndApplyEffects(BattleCharacter User, CharacterAbility Ability, BattleCharacter Target) {
		Target.Health -= Ability.Damage;

		TriggerNextTurn();
	}
}
