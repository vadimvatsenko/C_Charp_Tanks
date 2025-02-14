using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Bullet : Shell
{
    private double moveCooldown = 0;
    public Bullet(FabricController fabricController, Vector2 position, Vector2 dir) : base(fabricController, position, dir)
    {
        Speed = 8;
    }

    public override void Update(double deltaTime)
    {
        moveCooldown += deltaTime;

        // Рассчитываем, сколько шагов можно сделать за накопленное время
        while (moveCooldown >= 1.0 / Speed)
        {
            moveCooldown -= 1.0 / Speed; // Уменьшаем накопленное время
            Position += Direction; // Делаем шаг в направлении
        }
    }

    public override void Render(IRenderer renderer)
    {
        renderer.SetPixel(Position.X, Position.Y, View, 4);
    }
    
    public override void Destroy()
    {
        IsDestroyed = true;
        _fabricController.ShellsFabric.RemoveShell(this);
    }
    
}