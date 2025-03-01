using System.Numerics;

namespace C_Charp_Tanks.Engine.Ray;

public struct RayCast
{
    private Vector2 _origin;
    private Vector2 _direction;
    private int _length;

    public RayCast(Vector2 origin, Vector2 direction, int length)
    {
        _origin = origin;
        _direction = direction;
        _length = length;
    }

    public bool CheckDetection(Collider2D other)
    {
        for (int i = 1; i <= _length; i++) // i идёт от 1, чтобы не проверять свою же позицию
        {
            Vector2 tempPos = _origin + _direction * i; // Смещаемся в направлении игрока
            BoxCollider2D tempColl = new BoxCollider2D(tempPos, Vector2.One);

            if (tempColl.IsColliding(other)) return true;
        }

        return false;
    }
}