using UnityEngine.AI;
using UnityEngine;

public class RiverInteraction : MonoBehaviour
{
    private PlayerController _playerControllerComponent;
    private NavMeshAgent _navMeshAgentComponent;
    private KiterController _kiterController;
    private RusherController _rusherController;

    // Start is called before the first frame update
    void Start()
    {
        _playerControllerComponent = GetComponent<PlayerController>();
        _navMeshAgentComponent = GetComponent<NavMeshAgent>();
        TryGetComponent<KiterController>(out KiterController _kiterController);
        TryGetComponent<RusherController>(out RusherController _rusherController);
    }

    public void SlowPlayerSpeed(float slowdown)
    {
        _playerControllerComponent.SetPlayerSpeed(_playerControllerComponent.GetPlayerSpeed() * slowdown);
    }

    public void RemoveSlowPlayerSpeed(float slowdown)
    {
        _playerControllerComponent.SetPlayerSpeed(_playerControllerComponent.GetPlayerSpeed() / slowdown);
    }

    public void SlowAiSpeed(float slowdown)
    { 
        _navMeshAgentComponent.speed = _navMeshAgentComponent.speed * slowdown;
    }

    public void RemoveSlowAiSpeed(float slowdown)
    {
        _navMeshAgentComponent.speed = _navMeshAgentComponent.speed / slowdown;
    }
}
