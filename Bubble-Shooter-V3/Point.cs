using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Bubble_Shooter_V3
{
	public class Point
	{
		double x,y;
		public Point(){	x=y=0;	}
		public Point(double x_, double y_){	x=x_;	y=y_;	}
		public void Set(double x_, double y_){	x=x_;	y=y_;	}
		public void Set(Point a){	x=a.X;	y=a.Y;	}
		public double X{	set{x = value;}	get{return x;}	}
		public double Y{	set{y = value;}	get{return y;}	}
	}
}
