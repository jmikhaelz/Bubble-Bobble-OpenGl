using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Bubble_Shooter_V3
{
	public class MyVector
	{
		double cx, cy, cz;
		float r, g, b;
		public MyVector()
		{
			cx = cy = cz = 0;
			r = g = b = 1;
		}
		public MyVector(Point a, Point b)
		{
			cx = b.X - a.X;
			cy = b.Y - a.Y;
			cz = 0;
		}
		
		public MyVector(double x, double y, double z)
		{
			cx = x;
			cy = y;
			cz = z;
		}
		
		public void Set(double x, double y, double z)
		{
			cx = x;
			cy = y;
			cz = z;
		}
		
		public void Set(Point a, Point b)
		{
			cx = b.X - a.X;
			cy = b.Y - a.Y;
			cz = 0;
		}
		
		public double X{	set { cx = value; }	get { return cx; } }
		public double Y{	set { cy = value; }	get { return cy; } }
		public double Z{	set { cz = value; }	get { return cz; } }
		
		public void color(float R, float G, float B)
		{
			r = R;
			g = G;
			b = B;
		}
		
		public double ProdPunto(MyVector a, MyVector b)
		{
			double x = a.X * b.X;
			double y = a.Y * b.Y;
			double z = a.Z * b.Z;
			return x + y + z;
		}
		
		public double Magnitud(MyVector a)
		{
			double x = Math.Pow(a.X, 2);
			double y = Math.Pow(a.Y, 2);
			double z = Math.Pow(a.Z, 2);
			return Math.Sqrt(x + y + z);
		}
		
		public double Coseno(MyVector a, MyVector b)
		{
			double aux = ProdPunto(a, b) / (Magnitud(a) * Magnitud(b));
			return aux;
		}
		
		public MyVector ProdCruz(MyVector v, MyVector w)
		{
			return new MyVector((v.Y * w.Z) - (w.Y * v.Z), -((v.X * w.Z) - (w.X * v.Z)), (v.X * w.Y) - (w.X * v.Y));
		}
		public double Distancia(Point a)
		{
			double dx = Math.Pow((cx - a.Y), 2);
			double dy = Math.Pow((cy - a.Y), 2);
			return Math.Sqrt(dx + dy);
		}
		
		public static MyVector operator*(int a, MyVector b)
		{
			return new MyVector(b.X * a, b.Y * a, b.Z * a);
		}
		
		public void Draw(Point a)
		{
			GL.Begin(PrimitiveType.LineLoop);
			GL.Color3(r, g, b);
			for (int i = 0; i < 10; i++) {
				GL.Vertex2(a.X + i, a.Y + i);
				GL.Vertex2(a.X + cx, a.Y + cy);
			}
			GL.End();
		}
		
		public void Draw(Point pi, Point pf)
		{
			GL.Begin(PrimitiveType.LineLoop);
			GL.Color3(r,g,b);
			GL.Vertex2(pi.X,pi.Y);
			GL.Vertex2(pf.X,pf.Y);
			GL.Vertex2(pi.X-1,pi.Y);
			GL.Vertex2(pf.X-1,pf.Y);
			GL.Vertex2(pi.X+1,pi.Y);
			GL.Vertex2(pf.X+1,pf.Y);
			GL.Vertex2(pi.X-1,pi.Y-1);
			GL.Vertex2(pf.X-1,pf.Y);
			GL.Vertex2(pi.X+1,pi.Y);
			GL.Vertex2(pf.X+1,pf.Y+1);
			GL.End();
		}
	}
}
