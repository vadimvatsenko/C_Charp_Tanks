namespace C_Charp_Tanks.Renderer;

public interface IRenderer
{
    public void SetPixel(int w, int h, char val, byte colorIdx);
    public void Render();
    public void Clear();
}