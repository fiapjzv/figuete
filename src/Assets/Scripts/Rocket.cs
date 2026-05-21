using UnityEngine;

public partial class Rocket : MonoBehaviour
{
    [SerializeField]
    private Transform _tipPivot = null!;

    private IGameGrid _gameGrid = null!;

    public void Awake()
    {
        _gameGrid = Service.Get<IGameGrid>();
        PopulateWobbleRandomOffsets();
    }

    public void Update()
    {
        WobbleAroundQuadrantCenter();
    }

    // Vector3 GetQuadrantCenter(int col, int row)
    // {
    //     var viewportX = (col + 0.5f) / columns;
    //     var viewportY = (row + 0.5f) / rows;
    //     return _cam.ViewportToWorldPoint(new Vector3(viewportX, viewportY, distanceFromCamera));
    // }
}
