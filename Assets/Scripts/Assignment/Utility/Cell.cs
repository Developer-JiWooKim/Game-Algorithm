public class Cell
{
    public int col;
    public int row;

    public bool visited;

    // 각 방향 벽 (true = 벽 있음)
    public bool upWall = true;
    public bool downWall = true;
    public bool leftWall = true;
    public bool rightWall = true;

    public UnityEngine.Vector3 worldCenter;

    public Cell(int col, int row, UnityEngine.Vector3 worldCenter)
    {
        this.col = col;
        this.row = row;
        this.worldCenter = worldCenter;
    }
}
