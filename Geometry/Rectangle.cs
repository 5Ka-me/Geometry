namespace Geometry
{
    internal class Rectangle
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        internal Player Player { get; set; }

        public Rectangle(int width, int height, Player player)
        {
            X1 = 0;
            Y1 = 0;
            Width = width;
            Height = height;

            Player = player;
        }

        public void MoveTo(int x, int y)
        {
            X1 = x - 1;
            Y1 = y - 1;
            X2 = X1 + Width;
            Y2 = Y1 + Height;
        }

        public int CalculateScore()
        {
            return Width * Height;
        }
    }
}
