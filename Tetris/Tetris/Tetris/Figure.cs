namespace Tetris
{
    abstract class Figure
    {
        protected int XPos;
        protected int YPos;
        protected bool[,] Grid;


        public int Xpos { get; set; }
        public int Ypos { get; set; }



        protected Figure()
        {
            Grid = new bool[4, 4];
        }
    }

    class Square : Figure
    {
        public Square()
        {
            Grid = bool[4,4]
            {
                {
                    false
                }
            , false, false, false,}
                false, true, true, false,
                false, true, true, false,
                false, false, false, false
            };
        }
    }

    class Hook : Figure
    {

    }
}