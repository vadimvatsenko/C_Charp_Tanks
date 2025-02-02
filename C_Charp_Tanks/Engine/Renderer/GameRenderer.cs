using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Renderer;

public class GameRenderer : IUpdatable
{
    private BlocksController _blocksController;
    private Player _player;
    private IRenderer _renderer;

    public GameRenderer(Player player, BlocksController blocksController, IRenderer renderer)
    {
        _player = player;
        _blocksController = blocksController;
        _renderer = renderer;
    }

    public void DrawGame()
    {
        
    }

    public void Update(double deltaTime)
    {
        Console.WriteLine("Game Update");
        _blocksController.GenerateBlocks();
        _player.Draw();
        
        _renderer.Render();
    }
}