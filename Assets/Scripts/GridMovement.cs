using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private Tilemap _map;
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private float _movementSpeed;

    private static Vector3 _destination;
    public static GameObject selectedHero;
    private float cameraSpeed = 10f;
    private Animator anim;
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        _lr = _lr.GetComponent<LineRenderer>();
    }

    void MouseClick()
    {
        if (SceneManager.GetActiveScene().name != "BattleScene" || battleMoveClick.mouseClick)
        {
            battleMoveClick.mouseClick = false;
            anim = selectedHero.GetComponentInChildren<Animator>();

            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3Int gridPos = _map.WorldToCell(mousePos);
            if(_map.HasTile(gridPos))
            {
                _destination = _map.CellToWorld(gridPos);

                if (selectedHero.transform.position.x < _destination.x)
                    selectedHero.transform.localScale = new Vector3(-1, 1, 1);
                else if (selectedHero.transform.position.x > _destination.x)
                    selectedHero.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void Update() 
	{
        if (selectedHero != null)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast (ray, out hit))
                    Debug.Log(hit.transform.name);

                MouseClick();
            }
                
            if (Vector3.Distance(selectedHero.transform.position, _destination) > 0.1f)
            {
                SelectHero.battleMove.SetActive(false);
                anim.SetBool("isWalking", true);
                selectedHero.transform.position = Vector3.MoveTowards(new Vector3(selectedHero.transform.position.x, selectedHero.transform.position.y, -0.1f), 
                                                        new Vector3(_destination.x, _destination.y, -0.1f), 
                                                        _movementSpeed * Time.deltaTime);
                
                _lr.SetPosition(0, new Vector3(selectedHero.transform.position.x, selectedHero.transform.position.y, -0.05f));
                _lr.SetPosition(1, new Vector3(_destination.x, _destination.y, -0.05f));

                if (selectedHero.transform.position == new Vector3(_destination.x, _destination.y, -0.1f))
                {
                    anim.SetBool("isWalking", false);
                    selectedHero = null;
                    SelectHero.staticOutline.SetActive(false);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (selectedHero != null && SceneManager.GetActiveScene().name != "BattleScene")
        {
            Camera.main.transform.parent = selectedHero.transform;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, 
                                                            new Vector3(selectedHero.transform.position.x, selectedHero.transform.position.y, Camera.main.transform.position.z), 
                                                            cameraSpeed * Time.deltaTime);
        }
    }
}