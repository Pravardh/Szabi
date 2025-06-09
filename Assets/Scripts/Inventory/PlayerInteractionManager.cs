using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    private Interactable interactable;

    private bool canInteract = false;

    public bool CanInteract { get { return canInteract; } }
    public Interactable Interactable { get { return interactable; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out interactable))
        {
            canInteract = true;
            Debug.Log("Came into contact with interactable: ");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactable _interactable))
        {
            if (interactable == _interactable)
            {
                canInteract = false;
                interactable = null;
                Debug.Log("Came out of contact with interactable: ");

            }

        }

    }

}
