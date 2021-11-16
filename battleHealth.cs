using UnityEngine;
using UnityEngine.SceneManagement;

public class battleHealth : MonoBehaviour
{
    private float _hp = 100f;
    private Animator _anim;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            _anim = gameObject.GetComponentInChildren<Animator>();
            gameObject.GetComponent<CircleCollider2D>().radius = 1f;
        }
    }

    void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            if (battleMoveClick.inEnemy)
            {
                _anim.SetTrigger("Damage");
                battleMoveClick.anim.SetTrigger("Attack");
                _hp -=25f;
            }
        }
    }

    void Update()
    {
        if (_hp <= 0)
        {
            SceneManager.LoadScene("MainScene");
            SavingScript.TriggerLoad();
            SavingScript.characters.Clear();
            SQLscript.RequestExecution("UPDATE playerinfo SET coinCount = coinCount + 15");
            globals.heroesNearEnemy.Clear();
        }
    }
}