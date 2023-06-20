using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private Animal _animal;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    private void Start()
    {
        _animal = GetComponent<Animal>();
    }

    private void Update()
    {
        if (_animal.IsActiveAnimal)
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

            if (_numFound > 0)
            {
                var _interactable = _colliders[0].GetComponent<IInteractable>();

                if (_interactable != null)
                {
                    if (!_interactionPromptUI.isDisplayed && _interactable.CanInteract()) _interactionPromptUI.SetUpInteract();

                    if (InputManager.Instance.Interact())
                    {
                        _interactable.Interact(this);
                    }

                }
            }
            else
            {
                if (_interactable != null) _interactable = null;
                if (_interactionPromptUI.isDisplayed) _interactionPromptUI.CloseInteract();
            }
        }
        else
        {
            if (_interactionPromptUI.isDisplayed)
            {
                _interactionPromptUI.CloseInteract();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

    public LayerMask GetLayerMask()
    {
        return _interactableMask;
    }
}
