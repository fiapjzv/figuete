using UnityEngine;

public partial class Rocket : GameBehavior
{
    [SerializeField]
    private Transform _tipPivot = null!;

    private IGameGrid _gameGrid = null!;

    protected override void Init()
    {
        _gameGrid = Service.Get<IGameGrid>();
        PopulateWobbleRandomOffsets();
    }

    public void Update()
    {
        WobbleAroundQuadrantCenter();
    }
}
