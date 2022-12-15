using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;


namespace Bubble_Shooter_V3
{
	public class Engine
	{
		public Engine()
		{
		}
		public void Fig(double _x, double _y, double lados, double 	angulo, double tam, float r, float g, float b, PrimitiveType type)
		{
			double inc = (Math.PI * 2 / lados); 		//Dividimos los lados entra la circuferencias 2PI/l
			double ang = (angulo / 180) * Math.PI;		//Convertimos angulo en radianes (a/180)PI
			GL.Color3(r, g, b);
			GL.Begin(type);
			for (double x = ang; x <= (Math.PI * 2) + ang; x += inc) {
				GL.Vertex2(_x + Math.Cos(x - inc) * tam, _y + Math.Sin(x - inc) * tam);
				GL.Vertex2(_x + Math.Cos(x) * tam, _y + Math.Sin(x) * tam);
			}
			GL.End();
		}
		public int colision(double x1, double y1, double r1, double x2, double y2, double r2)
		{
			double Vx = (x1 - x2);
			double Vy = (y1 - y2);
			double distancia = Math.Sqrt(Math.Pow(Vx, 2) + Math.Pow(Vy, 2));
			if (distancia < (r1 + r2)) {
				return 1;
			}
			return 0;
		}
		public int encasillar(double x1, double y1, double r1, double x2, double y2, double r2)
		{
			double Vx = (x1 - x2);
			double Vy = (y1 - y2);
			double distancia = Math.Sqrt(Math.Pow(Vx, 2) + Math.Pow(Vy, 2));
			if (distancia >= (r1+r2)) {
				return 1;
			}
			return 0;
		}
		public double angulo(double rad)
		{
			return (rad / Math.PI * 2);
		}
		//Metodo de direccion
		public MyVector dir_act(double _x, double _y, Circle arco, MyVector flecha)
		{
			MyVector vPerpendicular = new MyVector();
			MyVector vMano = new MyVector(0, 0, 1);
			double xp = 0, xa = 0, xf = 0;
			MyVector direccion = new MyVector();
			if (_x >= 300) {
				//Perpendicular
				vPerpendicular = vPerpendicular.ProdCruz(vMano, new MyVector(new Point(arco.X, arco.Y), new Point(arco.X, arco.Y + arco.R)));
				xp = vPerpendicular.Coseno(vPerpendicular, new MyVector(new Point(arco.X, arco.Y), new Point(arco.X + arco.R / 2, arco.Y)));
				
				xf = flecha.Coseno(flecha, vPerpendicular);
				xa = angulo(xf - xp);
				
			} else if (_x < 300) {
				//Perpendicular
				vPerpendicular = vPerpendicular.ProdCruz(new MyVector(new Point(arco.X, arco.Y), new Point(arco.X, arco.Y + arco.R)), vMano);
				xp = vPerpendicular.Coseno(new MyVector(new Point(arco.X, arco.Y), new Point(arco.X + arco.R / 2, arco.Y)), vPerpendicular);
				
				xf = flecha.Coseno(flecha, vPerpendicular);
				xa = angulo(xf + xp);
			}
			flecha.X *= Math.Acos(xa);
			flecha.Y *= Math.Asin(xa);
			
			direccion.Set(flecha.X, flecha.Y, 0);
			direccion.X = (direccion.X / (direccion.Magnitud(direccion)));
			direccion.Y = (direccion.Y / (direccion.Magnitud(direccion)));
			return direccion;
		}
		public void Div(double d, double tam, float r, float g, float b, PrimitiveType type){
				GL.Begin(type);
				GL.Color3(r,g,b);
				for (double i = -tam; i < tam; i+=(tam/d)) {
					GL.Vertex2(i,-1*i);
					GL.Vertex2(i,tam);
					GL.Vertex2(-1*i,i);
					GL.Vertex2(tam,i);
				}
				GL.End();
		}
	}
}
