using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnOnBattleButton : MonoBehaviour
{
    public static GameObject _battleButton;
    public static GameObject enemy;

    void Start()
    {
        _battleButton = GameObject.Find("battleButton").GetComponent<Button>().gameObject;
        _battleButton.GetComponent<Image>().enabled = false;
    }

    public void BattleButtonClick()
    {
        Spawner.enemies.Remove(enemy);

        foreach (GameObject obj in Spawner.enemies)
        {
            Character characterObj = new Character(obj.name, obj.transform.position);
            SavingScript.characters.Add(characterObj);
        }

        foreach (GameObject obj in BuyHeroes.heroes)
        {
            Character characterObj = new Character(obj.name, obj.transform.position);
            SavingScript.characters.Add(characterObj);
        }
        SavingScript.TriggerSave(Spawner.castleCoords);

        SceneManager.LoadScene("BattleScene");
    }
}
