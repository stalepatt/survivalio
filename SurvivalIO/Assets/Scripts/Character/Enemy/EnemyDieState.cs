using UnityEngine;

public class EnemyDieState : StateMachineBehaviour
{
    private EnemyCharacter _enemyCharacter;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyCharacter = animator.GetComponent<EnemyCharacter>();

        Managers.GameManager.CurrentChapter.SetData(killCount: 1);
        Managers.PoolManager.Spawner.SpawnExp(_enemyCharacter.Stat.Exp, _enemyCharacter.transform);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyCharacter.Return();
    }
}
