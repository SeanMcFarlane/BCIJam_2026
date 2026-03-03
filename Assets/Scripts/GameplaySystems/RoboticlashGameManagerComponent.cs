using UnityEngine;

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
			EnemyCharacter.StartUsingAbility(EnemyCharacter.EquippedAbilities[Random.Range(0, EnemyCharacter.EquippedAbilities.Length-1)], PlayerCharacter);
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
}
