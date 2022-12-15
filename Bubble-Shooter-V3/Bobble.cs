using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace Bubble_Shooter_V3
{
	public class Bobble
	{
		MyVector dir = new MyVector();
		Point pos = new Point();
		double v = 10;
		Circle obj = new Circle();
		double radio;
		System.Random rnd = new System.Random();
		double color;
		bool col = false;
		bool vida = true;
		public Bobble()
		{
			radio = 30;
		}
		public Bobble(Point pos, double r)
		{
			this.pos = pos;
			this.radio = r;
		}
		public Bobble(Point pos, double r, double c)
		{
			this.pos = pos;
			this.radio = r;
			AsigColor(c);
		}
		public void Set(MyVector dir, Point pos, double r)
		{
			this.dir = dir;
			this.pos = pos;
			this.radio = r;
		}
		
		public Point Pos{	set { pos = value; }	get { return pos; } }
		public double posx{	set { pos.X = value; }	get { return pos.X; } }
		public double posy{	set { pos.Y = value; }	get { return pos.Y; } }
		
		public MyVector Dir{	set { dir = value; }	get { return dir; } }
		public double dirx{	set { dir.X = value; }	get { return dir.X; } }
		public double diry{	set { dir.Y = value; }	get { return dir.Y; } }
		
		public double Radio{	set { radio = value; }	get { return radio; } }
		
		public double Velocidad{	set { v = value; }	get { return v; } }
		public double Color{	set { color = value; }	get { return color; } }
		
		
		
		public bool Colision{	set { col = value; }	get { return col; } }
		public bool Vida{	set { vida = value; }	get { return vida; } }
		
		public void rgb(float r, float g, float b)
		{
			obj.color(r, g, b);
		}
		
		public void Trayecto()
		{
			pos.X = pos.X + v * dir.X;
			pos.Y = pos.Y + v * dir.Y;
		}
		public void Draw(double giro)
		{
			Engine centro = new Engine();
			double tam =  radio-(radio/4);
			obj.R = radio;
			obj.Set(pos);
			obj.Draw();
			switch ((int)color) {
				case 0:
					centro.Fig(posx, posy, 8, giro, tam, 0.1f, 0.1f, 0.1f, PrimitiveType.Polygon);
					break; //negro
				case 1:
					centro.Fig(posx, posy, 7, giro, tam, 0.5f, 0.5f, 0.5f, PrimitiveType.Polygon);
					break; //blanco
				case 2:
					centro.Fig(posx, posy, 6, giro, tam, 0.5f, 0, 0, PrimitiveType.Polygon);
					break; //rojo
				case 3:
					centro.Fig(posx, posy, 5, giro, tam, 0, 0.5f, 0, PrimitiveType.Polygon);
					break; //verde
				case 4:
					centro.Fig(posx, posy, 4, giro, tam, 0, 0, 0.5f, PrimitiveType.Polygon);
					break; //azul
				case 5:
					centro.Fig(posx, posy, 3, giro, tam, 0.5f, 0.5f, 0, PrimitiveType.Polygon);
					break; //amarrillo
				case 6:
					centro.Fig(posx, posy, 4, giro, tam, 0.5f, 0, 0.5f, PrimitiveType.Polygon);
					break; //morado
				case 7:
					centro.Fig(posx, posy, 5, giro, tam, 0.5f, -0.5f, 0.5f, PrimitiveType.Polygon);
					break; //naranja
			}
			
		}
		public void GenerarColor()
		{
			switch ((int)rnd.Next(0, 7)) {
				case 0:
					color = 0;
					obj.color(0, 0, 0);
					break; //negro
				case 1:
					color = 1;
					obj.color(1, 1, 1);
					break; //blanco
				case 2:
					color = 2;
					obj.color(1, 0, 0);
					break; //rojo
				case 3:
					color = 3;
					obj.color(0, 1, 0);
					break; //verde
				case 4:
					color = 4;
					obj.color(0, 0, 1);
					break; //azul
				case 5:
					color = 5;
					obj.color(1, 1, 0);
					break; //amarrillo
				case 6:
					color = 6;
					obj.color(1, 0, 1);
					break; //morado
				case 7:
					color = 7;
					obj.color(1, 0.5f, 1);
					break; //naranja
			}
		}
		public void AsigColor(double c)
		{
			color = c;
			switch ((int)c) {
				case 0:
					obj.color(0, 0, 0);
					break; //negro
				case 1:
					obj.color(1, 1, 1);
					break; //blanco
				case 2:
					obj.color(1, 0, 0);
					break; //rojo
				case 3:
					obj.color(0, 1, 0);
					break; //verde
				case 4:
					obj.color(0, 0, 1);
					break; //azul
				case 5:
					obj.color(1, 1, 0);
					break; //amarrillo
				case 6:
					obj.color(1, 0, 1);
					break; //morado
				case 7:
					obj.color(1, 0.5f, 1);
					break; //naranja
			}
		}
	}
}
