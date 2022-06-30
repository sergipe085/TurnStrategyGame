public struct GridPosition {
    public int x;
    public int z;

    public GridPosition(int _x, int _z) {
        x = _x;
        z = _z;
    }

    public override string ToString()
    {
        return $"x: {x}; z: {z}";
    }
}