using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_Hoa_May_Tinh
{
    public partial class Form1 : Form
    {
        private DrawAPI currApi = AlgoFactory.GetInstance.GetAPI(AlgoFactory.Algo.Midpoint);
        private string currentShapeName;
        public GraphicList listshape = new GraphicList();
        public Form1()
        {
            InitializeComponent();
            foreach (Button button in panel2.Controls)
            {
                button.Click += UpdateCurrent;
            }
        }

        private void UpdateCurrent(object sender, EventArgs e)
        {
            currentShapeName = (sender as Button).Tag as string;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            listshape.Draw(e.Graphics, currApi);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ToolFactory.GetInstance.GetTool(currentShapeName).OnMouseDown(this, e);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ToolFactory.GetInstance.GetTool(currentShapeName).OnMouseMove(this, e);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ToolFactory.GetInstance.GetTool(currentShapeName).OnMouseUp(this, e);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Size = this.Size;
            
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ToolFactory.GetInstance.GetTool(currentShapeName).OnMouseDoubleClick(this, e);
            }
        }

    }

    public class AlgoFactory
    { 
        public enum Algo
        {
            Bresenham,
            Midpoint,
        };
        private Dictionary<Algo, DrawAPI> APIList;
        private static readonly AlgoFactory Instance = new AlgoFactory();

        public static AlgoFactory GetInstance
        {
            get { return Instance; }
        }
        public DrawAPI GetAPI(Algo apiname)
        {
            return APIList[apiname] as DrawAPI;
        }

        private AlgoFactory()
        {
            APIList = new Dictionary<Algo, DrawAPI>();
            APIList.Add(Algo.Bresenham, new BresenhamAlgo());
            APIList.Add(Algo.Midpoint, new MidpointAlgo());
        }
    }

    public abstract class DrawAPI
    {
        protected Pen PenDraw = new Pen(Color.Black, 1);

        protected virtual void SetPixel(Graphics g, int x, int y)
        {
            g.DrawLine(PenDraw, x, y, x + PenDraw.Width, y);
        }
        protected void SetPixel2Circle(Graphics g, int x1, int y1, int x, int y)
        {
            SetPixel(g, x + x1, y + y1);
            SetPixel(g, x - x1, y + y1);
            SetPixel(g, x + x1, y - y1);
            SetPixel(g, x - x1, y - y1);
            SetPixel(g, x + y1, y + x1);
            SetPixel(g, x - y1, y + x1);
            SetPixel(g, x + y1, y - x1);
            SetPixel(g, x - y1, y - x1);
        }
        protected void SetPixel2Ellipse(Graphics g, int x1, int y1, int x, int y)
        {
            SetPixel(g, x + x1, y + y1);
            SetPixel(g, x - x1, y + y1);
            SetPixel(g, x + x1, y - y1);
            SetPixel(g, x - x1, y - y1);
        }
        public abstract void DrawLine(Graphics g, Line line);
        public abstract void DrawCircle(Graphics g, Circle circle);
        public abstract void DrawEllipse(Graphics g, Ellipse ellipse);
        public void DrawBezier(Graphics g, Bezier bezier)
        {
            TinhToan tinh = new TinhToan();
            int L = bezier.points.Count - 1;
            int SoDiem = 500;
            Point Diem = new Point();
            double dx = 1.0 / SoDiem;
            double x = 0;
            if (L > 0)
            {
                for (int i = 1; i <= SoDiem + 1; i++)
                {
                    tinh.Pt(x, L, bezier.points, ref Diem);
                    SetPixel(g, Diem.X, Diem.Y);
                    x = x + dx;
                }
            }
        }
    

    };

    class TinhToan
    {
        public double Tich(int x, int y)
        {
            if (y > 1)
            {
                int s = 1;
                for (int i = x; i <= y; i++)
                    s = s * i;
                return s;
            }
            return 1;
        }

        public double CLK(int l, int k)
        {
            return Tich(k + 1, l) / Tich(1, l - k);
        }

        public double BKL(double t, int l, int k)
        {
            return (CLK(l, k) * Math.Pow(1 - t, l - k) * Math.Pow(t, k));
        }

        public void Pt(double t, int l, List<Point> A, ref Point diem)
        {
            double s, x = 0, y = 0;
            for (int i = 0; i <= l; i++)
            {
                s = BKL(t, l, i);
                x = x + A[i].X * s;
                y = y + A[i].Y * s;
            }
            diem.X = (int)x;
            diem.Y = (int)y;
        }

    };

    public class BresenhamAlgo : DrawAPI
    {
        public override void DrawLine(Graphics g, Line line)
        {
            PenDraw.Color = line.PenColor;  PenDraw.Width = line.StrokeWidth;
            int x1 = line.PointStart.X, y1 = line.PointStart.Y;
            int x2 = line.PointEnd.X, y2 = line.PointEnd.Y;
            int x = x1, y = y1;
            int deltaX = x2 - x1;
            int deltaY = y2 - y1;
            int increaseX = (deltaX < 0) ? -1 : 1;
            int increaseY = (deltaY < 0) ? -1 : 1;
            deltaX = Math.Abs(deltaX);
            deltaY = Math.Abs(deltaY);
            SetPixel(g, x, y);
            if (deltaX > deltaY)
            {
                int case1 = 2 * deltaY;
                int case2 = 2 * (deltaY - deltaX);
                int currentcheckvalue = 2 * deltaY - deltaX;
                while (x != x2)
                {
                    if (currentcheckvalue < 0)
                        currentcheckvalue += case1;
                    else
                    {
                        currentcheckvalue += case2;
                        y += increaseY;
                    }
                    x += increaseX;
                    SetPixel(g, x, y);
                }
            }
            else
            {
                int case1 = 2 * deltaX;
                int case2 = 2 * (deltaX - deltaY);
                int currentcheckvalue = 2 * deltaX - deltaY;
                while (y != y2)
                {
                    if (currentcheckvalue < 0)
                        currentcheckvalue += case1;
                    else
                    {
                        currentcheckvalue += case2;
                        x += increaseX;
                    }
                    y += increaseY;
                    SetPixel(g, x, y);
                }
            }
        }

        public override void DrawCircle(Graphics g, Circle circle)
        {
            PenDraw.Color = circle.PenColor; PenDraw.Width = circle.StrokeWidth;
            int x = circle.CenterPoint.X; int y = circle.CenterPoint.Y;
            int r = circle.Radius;
            int currentX = 0;
            int currentY = r;
            int currentcheckvalue = 3 - 2 * r;
            while (currentX <= currentY)
            {
                SetPixel2Circle(g, currentX, currentY, x, y);
                if (currentcheckvalue < 0)
                    currentcheckvalue += 4 * currentX + 6;
                else
                {
                    currentcheckvalue += 4 * (currentX - currentY) + 10;
                    --currentY;
                }
                ++currentX;
            }
        }
        public override void DrawEllipse(Graphics g, Ellipse ellipse) { }
    };
    public class MidpointAlgo : DrawAPI
    {
        public override void DrawLine(Graphics g, Line line)
        {
            PenDraw.Color = line.PenColor; PenDraw.Width = line.StrokeWidth;
            int x1 = line.PointStart.X, y1 = line.PointStart.Y;
            int x2 = line.PointEnd.X, y2 = line.PointEnd.Y;
            int x = x1, y = y1;
            int deltaX = x2 - x1;
            int deltaY = y2 - y1;
            int increaseX = (deltaX < 0) ? -1 : 1;
            int increaseY = (deltaY < 0) ? -1 : 1;
            deltaX = Math.Abs(deltaX);
            deltaY = Math.Abs(deltaY);
            SetPixel(g, x, y);
            if (deltaX > deltaY)
            {
                int case1 =  2*deltaY;
                int case2 = 2*(deltaY - deltaX);
                int currentcheckvalue = 2*deltaY - deltaX;
                while (x != x2)
                {
                    if (currentcheckvalue < 0)
                        currentcheckvalue += case1;
                    else
                    {
                        currentcheckvalue += case2;
                        y += increaseY;
                    }
                    x += increaseX;
                    SetPixel(g, x, y);
                }
            }
            else
            {
                int case1 = 2*deltaX;
                int case2 = 2*(deltaX - deltaY);
                int currentcheckvalue = 2*deltaX - deltaY ;
                while (y != y2)
                {
                    if (currentcheckvalue < 0)
                        currentcheckvalue += case1;
                    else
                    {
                        currentcheckvalue += case2;
                        x += increaseX;
                    }
                    y += increaseY;
                    SetPixel(g, x, y);
                }
            }

        }
        public override void DrawCircle(Graphics g, Circle circle)
        {
            PenDraw.Color = circle.PenColor; PenDraw.Width = circle.StrokeWidth;
            int x = circle.CenterPoint.X; int y = circle.CenterPoint.Y;
            int r = circle.Radius;
            int currentX = 0;
            int currentY = r;
            int currentcheckvalue = 1 - r;
            while (currentX <= currentY)
            {
                SetPixel2Circle(g, currentX, currentY, x, y);
                if (currentcheckvalue < 0)
                    currentcheckvalue += 2 * currentX + 3;
                else
                {
                    currentcheckvalue += 2 * (currentX - currentY) + 5;
                    --currentY;
                }
                ++currentX;
            }

        }

        public override void DrawEllipse(Graphics g, Ellipse ellipse)
        {
            PenDraw.Color = ellipse.PenColor; PenDraw.Width = ellipse.StrokeWidth;
            int x = ellipse.Center.X; int y = ellipse.Center.Y;
            int rX = ellipse.RadiusX; int rY = ellipse.RadiusY;
            int currentX = 0;
            int currentY = rY;
            int currentcheckvalue = rY * rY - rX * rX * rY + 1 / 4 * rX * rX;
            while (2 * rY * rY * currentX < 2 * rX * rX * currentY)
            {
                SetPixel2Ellipse(g, currentX, currentY, x, y);
                if (currentcheckvalue < 0)
                    currentcheckvalue += rY * rY * (2 * currentX + 3);
                else
                {
                    currentcheckvalue += rY * rY * (2 * currentX + 3) - 2 * rX * rX * (currentY - 1);
                    currentY--;
                }
                currentX++;
            }

            currentcheckvalue = rY * rY * (currentX + 1 / 2) * (currentX + 1 / 2) + rX * rX * (currentY - 1) * (currentY - 1) - rX * rX * rY * rY;
            while (currentY != 0)
            {
                SetPixel2Ellipse(g, currentX, currentY, x, y);
                if (currentcheckvalue > 0)
                    currentcheckvalue += - rX * rX * (2 * currentY - 3);
                else
                {
                    currentcheckvalue += 2 * rY * rY * (currentX + 1) - rX * rX * (2 * currentY - 3);
                    currentX++;
                }
                currentY--;

            }
        }
    };

    public abstract class Shape
    {
        public Color PenColor
        { get; set; }
        public float StrokeWidth
        { get; set; }

        public Shape()
        {
            PenColor = Color.Black;
            StrokeWidth = 5;
        }

        public abstract void Draw(Graphics g, DrawAPI drawapi);
        public virtual void MoveTo(Point point, int handlenumber)
        {
            throw new Exception("Co Loi Xay Ra");
        }
    };

    public class Line : Shape
    {
        public Point PointStart
        {
            get;
            private set;
        }
        public Point PointEnd
        {
            get;
            private set;
        }

        public Line(int x1, int y1, int x2, int y2):this(new Point(x1, y1),
            new Point(x2, y2))
        { 
            
        }
        public Line():this(new Point(0,0),
            new Point(0,0))
        { 
            
        }
        public Line(Point pointstart, Point pointend)
        {
            PointStart = pointstart;
            PointEnd = pointend;
        }

        public override void Draw(Graphics g, DrawAPI drawapi)
        {
            drawapi.DrawLine(g, this);
        }

        public override void MoveTo(Point point, int handlenumber)
        {
            PointEnd = point;
        }
    };

    public class Circle : Shape
    {
        private Point StartPoint;
        public Point CenterPoint;
        public int Radius
        {
            get;
            private set;
        }

        public Circle(int x, int y, int r):this(new Point(x, y), r)
        { 
            
        }
        public Circle():this(new Point(0,0), 0)
        { 
            
        }
        public Circle(Point centerpoint, int radius)
        {
            CenterPoint = StartPoint = centerpoint;
            Radius = radius;
        }

        public override void Draw(Graphics g, DrawAPI drawapi)
        {
            drawapi.DrawCircle(g, this);
        }

        public override void MoveTo(Point point, int handlenumber)
        {
            //Tạo điểm mới theo tiêu chuẩn hình tròn dựa vào vị trí chuột
            int D1=Math.Abs(point.X - StartPoint.X);
            int D2=Math.Abs(point.Y - StartPoint.Y);
            if (D1 < D2)
                if (point.Y < StartPoint.Y)
                    point.Y = StartPoint.Y - D1;
                else
                    point.Y = StartPoint.Y + D1;
            else
                if (point.X < StartPoint.X)
                    point.X = StartPoint.X - D2;
                else
                    point.X = StartPoint.X + D2;
            //------------------------------------------
            CenterPoint.X = (point.X + StartPoint.X) / 2; CenterPoint.Y = (point.Y + StartPoint.Y) / 2;
            Radius = Math.Abs((point.X - StartPoint.X)) / 2;
        }
    };

    public class Ellipse : Shape
    {
        private Point StartPoint;
        public Point Center;
        public int RadiusX
        { get; private set; }
        public int RadiusY
        { get; private set; }

        public Ellipse(int x, int y, int rX, int rY)
            : this(new Point(x, y), rX, rY)
        {

        }
        public Ellipse(Point center, int rX, int rY)
        {
            Center = StartPoint = center;
            RadiusX = rX;
            RadiusY = rY;
        }
        public override void Draw(Graphics g, DrawAPI drawapi)
        {
            drawapi.DrawEllipse(g, this);
        }
        public override void MoveTo(Point point, int handlenumber)
        {
            Center.X = (point.X + StartPoint.X) / 2; Center.Y = (point.Y + StartPoint.Y) / 2;
            RadiusX = Math.Abs((point.X - StartPoint.X)) / 2;
            RadiusY = Math.Abs((point.Y - StartPoint.Y)) / 2;
        }
    };

    public class Bezier : Shape
    {
        public List<Point> points;

        public Bezier(Point p1, Point p2, Point p3, Point p4)
        {
            points = new List<Point>();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
        }

        public Bezier(List<Point> p)
        {
            points=new List<Point>();
            for (int i = 0; i < p.Count; i++)
            {
                points.Add(p[i]);
            }
        }

        public override void Draw(Graphics g, DrawAPI drawapi)
        {
            drawapi.DrawBezier(g, this);
        }

    };

    public class NBezier:Shape
    {
        public List<Bezier> Nbezier;

        public NBezier()
        {
            Nbezier = new List<Bezier>();
        }

        public void Add(Bezier b)
        {
            Nbezier.Add(b);
        }

        public override void Draw(Graphics g, DrawAPI drawapi)
        {
            foreach (Bezier b in Nbezier)
                b.Draw(g, drawapi);
        }

    }

    public class GraphicList : Shape
    {
        private List<Shape> ListShape;
        public GraphicList()
        {
            ListShape = new List<Shape>();
        }
        public Shape this[int index]
        {
            get
            {
                return ListShape[index] as Shape;
            }
        }

        public void Add(Shape shape)
        {
            ListShape.Insert(0, shape);
        }

        public void Remove(int index)
        {
            ListShape.RemoveAt(index);
        }

        public override void Draw(Graphics g, DrawAPI drawapi)
        {
            foreach (Shape shape in ListShape)
                shape.Draw(g, drawapi);
        }
    }



    public class ToolFactory
    { 
        private Dictionary<string, Tool> ToolList;
        private static readonly ToolFactory Instance = new ToolFactory();

        public static ToolFactory GetInstance
        {
            get { return Instance; }
        }
        public Tool GetTool(string toolname)
        {
            return ToolList[toolname] as Tool;
        }

        private ToolFactory()
        {
            ToolList = new Dictionary<string, Tool>();
            ToolList.Add("Pointer", new ToolPointer());
            ToolList.Add("Line", new ToolLine());
            ToolList.Add("Circle", new ToolCircle());
            ToolList.Add("Ellipse", new ToolEllipse());
            ToolList.Add("Bezier", new ToolBezier());
        }
    }
    public interface Tool
    {
        void OnMouseDown(Form1 form, MouseEventArgs e);
        void OnMouseMove(Form1 form, MouseEventArgs e);
        void OnMouseUp(Form1 form, MouseEventArgs e);
        void OnMouseDoubleClick(Form1 form, MouseEventArgs e);
        
    }
    public class ToolPointer : Tool
    {

        public void OnMouseDown(Form1 form, MouseEventArgs e)
        {
            
        }

        public void OnMouseMove(Form1 form, MouseEventArgs e)
        {
            
        }

        public void OnMouseUp(Form1 form, MouseEventArgs e)
        {
            
        }

        public void OnMouseDoubleClick(Form1 form, MouseEventArgs e)
        { }
    }
    public class ToolLine : Tool
    {
        public void OnMouseDown(Form1 form, MouseEventArgs e)
        {
            form.listshape.Add(new Line(e.X, e.Y, e.X + 1, e.Y + 1));
            form.Refresh();
        }

        public  void OnMouseMove(Form1 form, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            form.listshape[0].MoveTo(point, 1);
            form.Refresh();
        }

        public void OnMouseUp(Form1 form, MouseEventArgs e)
        {
            form.Refresh();
        }

        public void OnMouseDoubleClick(Form1 form, MouseEventArgs e)
        { }
    }
    public class ToolCircle : Tool
    {
        public void OnMouseDown(Form1 form, MouseEventArgs e)
        {
            form.listshape.Add(new Circle(e.X, e.Y, 1));
            form.Refresh();
        }

        public void OnMouseMove(Form1 form, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            form.listshape[0].MoveTo(point, 2);
            form.Refresh();
        }

        public void OnMouseUp(Form1 form, MouseEventArgs e)
        {
            form.Refresh();
        }

        public void OnMouseDoubleClick(Form1 form, MouseEventArgs e)
        { }
    }
    public class ToolEllipse : Tool
    {
        public void OnMouseDown(Form1 form, MouseEventArgs e)
        {
            form.listshape.Add(new Ellipse(e.X, e.Y, 1, 1));
            form.Refresh();
        }

        public void OnMouseMove(Form1 form, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            form.listshape[0].MoveTo(point, 2);
            form.Refresh();
        }

        public void OnMouseUp(Form1 form, MouseEventArgs e)
        {
            form.Refresh();
        }

        public void OnMouseDoubleClick(Form1 form, MouseEventArgs e)
        { }
    }
    //Implement cach ve cua Bezier
    public class ToolBezier : Tool
    {
        private List<Point> ListPoints=new List<Point>();
        private Point point;
        private int Count = 0;
        private bool first = true;


        public void OnMouseDown(Form1 form, MouseEventArgs e)
        {
            point = e.Location;
            ListPoints.Add(point);
            form.listshape.Add(new Line(point, point));
            Count++;
        }

        public void OnMouseMove(Form1 form, MouseEventArgs e)
        {
            if (Count == 1)
            {
                form.listshape[0].MoveTo(e.Location, 1);
                form.Refresh();
            }
            else
                if (Count == 3)
                {
                    Point p = new Point();
                    p.X = point.X - (e.X - point.X);
                    p.Y = point.Y - (e.Y - point.Y);
                    form.listshape[0].MoveTo(p, 1);
                    form.Refresh();
                }

        }

        public void OnMouseUp(Form1 form, MouseEventArgs e)
        {
            if (Count == 1)
            {
                ListPoints.Add(e.Location);
                Count++;
            }
            else
                if (Count == 3)
                {
                    Point p = new Point();
                    p.X = point.X - (e.X - point.X);
                    p.Y = point.Y - (e.Y - point.Y);
                    ListPoints.Insert(2,p);
                    if (first == true)
                    {
                        form.listshape.Remove(0);
                        first = false;
                    }

                    form.listshape.Remove(0);
                    form.listshape.Add(new Bezier(ListPoints));
                    Count = 2;
                    ListPoints.Clear();
                    ListPoints.Add(point);
                    ListPoints.Add(e.Location);
                    form.Refresh();
                }
        }

        public void OnMouseDoubleClick(Form1 form, MouseEventArgs e)
        {
            ListPoints.Clear();
            Count = 0;
            first = true;
        }
    }



}

