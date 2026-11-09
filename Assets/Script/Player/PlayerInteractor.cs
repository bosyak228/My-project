using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _range = 2.5f;
    [SerializeField] private Camera _camera;
    [SerializeField] private TextMeshProUGUI _hintText;
    [SerializeField] private PlayerInputReader _inputReader;

    public Inventory Inventory { get; private set; }

    private IInteractable _currentTarget;

    private void Awake()
    {
        Inventory = GetComponent<PlayerInventory>().Inventory;
        _inputReader.Interacted += OnInteractPressed;
    }

    private void OnDestroy() => _inputReader.Interacted -= OnInteractPressed;

    private void Update()
    {
        RefreshTarget();
        RefreshHint();
    }

    private void RefreshTarget()
    {
        _currentTarget = null;

        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, _range))
            _currentTarget = hit.collider.GetComponentInParent<IInteractable>();
    }

    private void RefreshHint()
    {
        bool hasTarget = _currentTarget != null;
        _hintText.gameObject.SetActive(hasTarget);

        if (hasTarget)
            _hintText.text = _currentTarget.InteractionHint + "  [E]";
    }

    private void OnInteractPressed()
    {
        if (!enabled) return;
        _currentTarget?.Interact(this);
    }

    private void OnDisable()
    {
        _currentTarget = null;

        if (_hintText != null)
            _hintText.gameObject.SetActive(false);
    }
}