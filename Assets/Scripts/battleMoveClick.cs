using UnityEngine;
using UnityEngine.SceneManagement;

public class battleMoveClick : MonoBehaviour
{
    public static bool mouseClick;
    public static bool inEnemy;
    public static Animator anim;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    void OnMouseDown()
    {
        mouseClick = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
            inEnemy = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
            inEnemy = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
            inEnemy = false;
    }
}