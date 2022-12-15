using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Bubble_Shooter_V3
{
	public class Circle
	{
		double cx, cy, radio;
		float r, g, b;
		
		//Aparte Mov de bubble
		bool vida = true;
		double v = 5;
		
		public Circle()
		{
			cx = cy = radio = 0;
			r = g = b = 0;
		}
		public Circle(double x, double y, double r)
		{
			cx = x;
			cy = y;
			radio = r;
			r = 0.5;
			g = 0;
			b = 0.8f;
		}
		public void Set(double x, double y, double r)
		{
			cx = x;
			cy = y;
			radio = r;
		}
		
		public void Set(Point z)
		{
			cx = z.X;
			cy = z.Y;
		}
		
		public double X{	set { cx = value; }	get { return cx; } }
		public double Y{	set { cy = value; }	get { return cy; } }
		public double R{	set { radio = value; }	get { return radio; } }
		public double D{	set { radio = value / 2; }	get { return (radio * 2); } }
		
		public bool Vida{	set { vida = value; }	get { return vida; } }
		public double V{	set { v = value; }	get { return v; } }
		
		
		public Point Centro {
			set {
				cx = value.X;
				cy = value.Y;
			}
			get{ return new Point(cx, cy); }
		}
		
		public void color(float R, float G, float B)
		{
			r = R;
			g = G;
			b = B;
		}
		
		public void DrawEje()
		{	
			GL.Begin(PrimitiveType.Polygon);
			GL.Color3(0.5f,0.5f,0.5f);
			for (double i = 0; i < Math.PI*2; i += 0.01) {
				GL.Vertex2(cx + Math.Cos(i) * radio / 3, cy + 16 + Math.Sin(i) * radio / 3);
			}
			GL.End();			
			GL.Begin(PrimitiveType.Points);
			GL.Color3(r, g, b);
			for (double i = 0; i < Math.PI; i += 0.1) {
				GL.Vertex2(cx + Math.Cos(i) * radio, cy + Math.Sin(i) * radio);
			}
			for (double i = 0; i < Math.PI*2; i += 0.01) {
				GL.Vertex2(cx + Math.Cos(i) * radio / 3, cy + 16 + Math.Sin(i) * radio / 3);
			}
			GL.End();
		}
		
		public void Draw()
		{
			GL.Begin(PrimitiveType.Points);
			GL.Color3(r, g, b);
			for (double i = 0; i < Math.PI * 2; i += 0.01) {
				GL.Vertex2(cx + Math.Cos(i) * radio, cy + Math.Sin(i) * radio);
			}
			GL.End();
			GL.Begin(PrimitiveType.Polygon);
			if (r==0&&g==0&&b==0) {
				GL.Color3(0.05f, 0.05f, 0.05f);
			for (double i = 0; i < Math.PI * 2; i += 0.01) {
				GL.Vertex2(cx + Math.Cos(i) * radio, cy + Math.Sin(i) * radio);
			}
			}
			else{
				GL.Color3(r-0.2f, g-0.2f, b-0.2f);
			for (double i = 0; i < Math.PI * 2; i += 0.01) {
				GL.Vertex2(cx + Math.Cos(i) * radio, cy + Math.Sin(i) * radio);
			}
			}
			GL.End();
		}
	}
}
