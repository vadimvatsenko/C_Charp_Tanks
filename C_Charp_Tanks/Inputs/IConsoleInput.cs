namespace C_Charp_Tanks;

public interface IConsoleInput
{
    public void TryToMoveUp();
    public void TryToMoveDown();
    public void TryToMoveLeft();
    public void TryToMoveRight();
    public void TryToShoot();
}