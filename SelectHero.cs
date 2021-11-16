using UnityEngine;
using UnityEngine.UI;

public class SelectHero : MonoBehaviour
{
    [SerializeField] private GameObject _outline;
    public static GameObject staticOutline;
    public static GameObject battleMove;
    
    void OnMouseDown()
    {
        battleMove = this.gameObject.transform.GetChild(2).gameObject;

        if (gameObject.tag == "Hero" && GridMovement.selectedHero == null)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BattleScene")
                battleMove.SetActive(true);
            
            staticOutline = _outline;
            staticOutline.SetActive(true);
            GridMovement.selectedHero = gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        try
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                globals.enemy = col.gameObject.name.Split('(')[0];
                globals.heroesNearEnemy.Add(gameObject.name.Split('(')[0]);
                TurnOnBattleButton._battleButton.GetComponent<Image>().enabled = true;
                TurnOnBattleButton.enemy = col.gameObject;
            }
        }
        catch
        {

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        try
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                TurnOnBattleButton._battleButton.GetComponent<Image>().enabled = false;
                globals.heroesNearEnemy.Clear();
            }
        }
        catch
        {

        }
    }
}