using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputReader playerInputReader;
    private NavMeshAgent playerNavMesh;

    [SerializeField]
    private PlayerInteractionManager playerInteractionManager;

    [SerializeField]
    private LayerMask clickableLayermask;

    private Camera mainCamera;

    private void Awake()
    {
        playerInputReader = GetComponent<PlayerInputReader>();
        playerNavMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        playerInputReader.OnPlayerMove += MovePlayer;

        mainCamera = Camera.main;
    }

    private void MovePlayer()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10000, clickableLayermask))
        {
            Vector3 hitpoint = hit.point;
            playerNavMesh.SetDestination(hitpoint);

            //Step 1: Check if you have clicked on item
            //Step 2: If player is near item
            //Step 3: Interact

            Debug.Log(hit.transform.gameObject);
            if (hit.transform.TryGetComponent(out Interactable _interactable)) //The interactable that you clicked
            {
                playerNavMesh.stoppingDistance = 5.0f;

                Debug.Log("Hit interactable");
                if (playerInteractionManager.CanInteract && playerInteractionManager.Interactable == _interactable) //Interactable that sphere is overlapped with
                {
                    _interactable.Interact();
                }
            }
            else
            {
                playerNavMesh.stoppingDistance = 0.0f;

            }
        }

    }

    public void ChangePlayerSpeed(float newSpeed)
    {
        playerNavMesh.speed = newSpeed;
    }


}
