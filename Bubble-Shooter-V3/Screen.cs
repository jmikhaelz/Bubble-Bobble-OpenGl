using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Bubble_Shooter_V3
{
	public class Screen : GameWindow
	{
		//Enterno y operaciones entre objetos
		Engine motor = new Engine();
		
		//GameOver
		bool g_over = false;
		//GoodGame
		int cont_f = 0;
		//Arco
		Circle arco = new Circle();
		
		//Flecha
		MyVector flecha = new MyVector();
		Engine punta = new Engine();
		double ap = 90;
		double _x = 0, _y = 0, i = 0, inc = 2.1;
		bool movf = true;
		
		//Bobble
		Bobble bobble = new Bobble();
		bool disparo = false;
		double giro = 0;
		int cant_b = 4;
		
		//Estaticas
		System.Random rnd = new System.Random();
		Stack<Bobble> estaticas = new Stack<Bobble>();
		
		//Comparar
		bool comp = false;
		Bobble bobble2 = new Bobble();
		Bobble adorno = new Bobble();
		
		//Disparo
		MyVector limite = new MyVector();
		int lim_d = 0;
		int cont_disparo = 0;
		double dec_y = 560;
		
		public Screen(int l, int w)
			: base(l, w)
		{
			Title = "Bobble Shooter V3 - Graficacion";
		}
		protected override void OnLoad(System.EventArgs e)
		{
			GL.ClearColor(Color.Black);
			GL.MatrixMode(MatrixMode.Projection);
			GL.Ortho(0, Width, 0, Height, -1, 1);
			
			//Arco
			arco.Set(300, 40, 80);
			//Pos Punta
			i = Math.PI / 2;
			posflecha(i);
			//bubble
			Clearb(bobble);
			CrearBobble(cant_b);
			//limite de disparos para que aparezca la linea
			lim_d=cant_b+3;
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			//Marco
			motor.Div(20, 600, 0.5f, 0.5f, 1, PrimitiveType.LineLoop);
			motor.Fig(300, 300, 4, 45, 368, 0.12f, 0.12f, 0.15f, PrimitiveType.Polygon);
			motor.Div(10, 600, 0.15f, 0.15f, 0.15f, PrimitiveType.Lines);
			motor.Fig(300, 300, 4, 45, 369, 1, 1, 0, PrimitiveType.LineLoop);
			//Arco
			arco.color(1, 0.5f, 0);
			//Flecha
			flecha.color(1, 1, 0);

		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			cont_f = 0;
			foreach (Bobble b in estaticas) {
				if (b.Vida == false) {
					cont_f++;
				}
			}
			if (cont_f == estaticas.Count) {
				GoodGame();
				Clearb(bobble);
			} else if (g_over) {
				GameOver();
			} else {
				Entorno();
				//Imprimir burbujas sin movimientos
				foreach (Bobble b in estaticas) {
					if (b.Vida == true) { //Si esta en pantalla dibujado se puede comparar con la bubble que disparamos
						if (b.posy <= 150) {//Revision de la linea inferior
							g_over = true;
						} else {
							GL.PointSize(3);
							b.Draw(giro);//imprimir burbujas estaticas
							if (motor.colision(bobble.posx, bobble.posy, bobble.Radio+7, b.posx, b.posy, b.Radio) == 1) {//Revision de colison entre burbujas
								bobble.Colision = true; //para la posicion de la bubble
								if (bobble.Color == b.Color) {
									comp = true;
									bobble2 = b;
								}
							}
							if (comp) {
								comp = false;
								foreach (Bobble rev in estaticas) {
									if (rev.Vida && (bobble2.Color == rev.Color)) {
										if (bobble2 != rev) {
											if (motor.colision(bobble2.posx, bobble2.posy, bobble2.Radio+11, rev.posx, rev.posy, rev.Radio) == 1) {
												rev.Vida = false;
												comp = true;
											}
										}
							
									}
								}
								if (comp) {
									bobble2.Vida = false;
									Clearb(bobble);
									comp = false;
								}
							}
						}
					}
				}
				//Revision del movimiento de la burbuja
				if (bobble.Colision) {//Revision si hay colision se aguarda la posicion posterior a la colision
					Add_bobble();
				} else if (bobble.posy + bobble.Radio >= dec_y) { //Revision del tope del recuadro
					Add_bobble();
					Clearb(bobble);
				} else { //Si no hay colision entre burbujas o tope , hay rebote entre paredes
					Rebote(bobble);
				}
			}
			GL.PointSize(4);
			adorno.Draw(giro);
			bobble.Draw(giro += 5);
			SwapBuffers();
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == 'k' || e.KeyChar == 'K') {
				Exit();
			}
			if (e.KeyChar == 'a' || e.KeyChar == 'A') {//Mov. flecha a la izq.
				if ((i <= Math.PI - 0.3) && movf) {
					i += 0.04;
					ap += inc;
					posflecha(i);
				}
			}
			if (e.KeyChar == 's' || e.KeyChar == 'S') {//Mov. flecha a la der.
				if ((i >= 0.3) && movf) {
					i -= 0.04;
					ap -= inc;
					posflecha(i);
				}
			}
			if (e.KeyChar == 'w' || e.KeyChar == 'W') {//Disparar
				if (!disparo) {
					flecha.Set(new Point(arco.X, arco.Y + 14), new Point(_x, _y));
					bobble.Set(motor.dir_act(_x, _y, arco, flecha), new Point(arco.X, arco.Y + 16), arco.R / 3);
					cont_disparo++;
				}
				disparo = true;
			}
			if (e.KeyChar == 'l' || e.KeyChar == 'L') {//Reinicio
				//Bubbles estaticas
				estaticas.Clear();
				//Flecha
				i = Math.PI / 2;
				posflecha(i);
				movf = true;
				//bubble
				Clearb(bobble);
				Clearb(bobble2);
				//stacks
				estaticas.Clear();
				CrearBobble(cant_b);
				//Punta
				inc = 2;
				ap = 90;
				//gameOver
				g_over = false;
				//comparar
				comp = false;
				//contador de bobble falses
				cont_f = 0;
				//disparos
				dec_y = 560;
				cont_disparo = 0;
			}
		}
		
		//METODOS PARA MANEJOS DE LOS OBJETOS
		
		//Metodos Grafico
		public void Entorno()
		{
			//Dibujo de la flecha
			GL.PointSize(10);
			if (dec_y!=560) {
				limite.color(1, 0, 1);
				limite.Draw(new Point(200, 560), new Point(200, dec_y));
				limite.Draw(new Point(400, 560), new Point(400, dec_y));
			}
			else{
				limite.color(1, 1, 0);
			}
			if (cont_disparo == lim_d) {
				dec_y -= bobble.Radio * 2;
				if (estaticas.Count != 0) {
					foreach (Bobble b in estaticas) {
						b.posy -= bobble.Radio * 2;
					}
				}
				cont_disparo = 0;
			}
			limite.Draw(new Point(40, dec_y), new Point(560, dec_y - 1));
			flecha.Draw(new Point(40, 120), new Point(560, 120));
			flecha.Draw(new Point(arco.X, arco.Y + 14), new Point(_x, _y));
			punta.Fig(_x, _y, 3, ap, 5, 1, 1, 0, PrimitiveType.Polygon);
			//Arco
			GL.PointSize(2);
			arco.DrawEje();
		}
		
		//Crear entorno de Derrota
		public void GameOver()
		{
			movf = false;
			giro = 0;
			//Marco
			motor.Div(20, 600, 0.8f, 0.5f, 0.5f, PrimitiveType.LineLoop);
			motor.Fig(300, 300, 4, 45, 368, 0.15f, 0.12f, 0.12f, PrimitiveType.Polygon);
			motor.Div(10, 600, 0.15f, 0.15f, 0.15f, PrimitiveType.Lines);
			motor.Fig(300, 300, 4, 45, 369, 1, 0, 0, PrimitiveType.LineLoop);
			//limite
			limite.color(1,1,1);
			limite.Draw(new Point(40, dec_y), new Point(560, dec_y - 1));
			//Felcha
			flecha.color(1, 0, 0);
			posflecha(Math.PI / 2);
			flecha.Draw(new Point(40, 120), new Point(560, 120));
			flecha.Draw(new Point(arco.X, arco.Y + 14), new Point(_x, _y));
			//Punta
			inc = 0;
			punta.Fig(_x, _y, 3, 90, 5, 1, 0, 0, PrimitiveType.Polygon);
			//Estaticas
			if (estaticas.Count != 0) {
				foreach (Bobble b in estaticas) {
					if (b.Vida) {
						b.rgb(0.2f, 0.2f, 0.2f);
						b.Draw(giro);
					}
				}
			}
			//Arco
			arco.color(1, 0, 0);
			GL.PointSize(2);
			arco.DrawEje();
			//Bubble
			bobble.rgb(0.2f, 0.2f, 0.2f);
			//Disparo
			disparo = true;
		}
		
		//Crear entorno de Ganador
		public void GoodGame()
		{
			movf = false;
			//Marco
			motor.Div(20, 600, 0.5f, 0.8f, 0.5f, PrimitiveType.LineLoop);
			motor.Fig(300, 300, 4, 45, 368, 0.12f, 0.12f, 0.15f, PrimitiveType.Polygon);
			motor.Div(10, 600, 0.15f, 0.15f, 0.15f, PrimitiveType.Lines);
			motor.Fig(300, 300, 4, 45, 369, 0, 1, 0, PrimitiveType.LineLoop);
			//limite
			limite.color(1,1,1);
			limite.Draw(new Point(40, dec_y), new Point(560, dec_y - 1));
			//Felcha
			flecha.color(0, 1, 0);
			posflecha(Math.PI / 2);
			flecha.Draw(new Point(40, 120), new Point(560, 120));
			flecha.Draw(new Point(arco.X, arco.Y + 14), new Point(_x, _y));
			//Punta
			inc = 0;
			punta.Fig(_x, _y, 3, 90, 5, 0, 1, 0, PrimitiveType.Polygon);
			//Arco
			arco.color(0, 1, 0);
			GL.PointSize(2);
			arco.DrawEje();
		}
		
		//Movimiento de la flecha
		public void posflecha(double i)
		{
			_x = (arco.X + Math.Cos(i) * (arco.R + arco.R / 4));
			_y = (arco.Y + Math.Sin(i) * (arco.R + arco.R / 4));
		}
		
		//Guardar bobble disparado, en la pila
		public void Add_bobble()
		{
			double aux;
			bobble.Velocidad = 0;
			aux = bobble.Color;//Recuperar color
			estaticas.Push(new Bobble(new Point(bobble.posx, bobble.posy), bobble.Radio, aux));//Agregamos a estaticos
			Clearb(bobble);//Limpiamos burbuja auxiliar
		}
		
		//Limpiar bubble disparada
		public void Clearb(Bobble b)
		{
			b.GenerarColor();
			b.posx = arco.X;
			b.posy = arco.Y + 16;
			b.Radio = (arco.R / 3)-4;
			b.dirx = 0;
			b.diry = 0;
			b.Velocidad = 15;
			disparo = false;
			b.Colision = false;
		}
		
		//Rebotar la bubble entre paredes
		public void Rebote(Bobble bubble)
		{
			bool colisionx = false;
			bool colisiony = false;
			
			if ((bubble.posx + bubble.Radio > 560) || (bubble.posx <= bubble.Radio + 40)) {
				colisionx = true;
			}
			if (colisionx) {
				bubble.dirx = -1 * bubble.dirx;
				colisionx = false;
			}
			if ((bubble.posy + bubble.Radio > dec_y) || (bubble.posy <= 20)) {
				colisiony = true;
			}
			if (colisiony) {
				bubble.diry = -1 * bubble.diry;
				colisiony = false;
			}
			bubble.Trayecto();
		}
		
		//Crear Bubbles
		public void CrearBobble(int cant)
		{
			int cont = 0;
			bool agregar = true;
			double x, y;
			x = rnd.Next(70, 535);
			y = rnd.Next(500-cant*5, 535);
			estaticas.Push(new Bobble(new Point(x, y), arco.R / 3, (int)rnd.Next(0, 7)));
			cont++;
			while (cont != cant) {
				x = rnd.Next(70, 535);
				y = rnd.Next(500-cant*5, 535);
				foreach (Bobble b in estaticas) {
					if ((motor.encasillar(x, y, b.Radio+2, b.posx, b.posy, b.Radio) == 1)) {
						agregar = true;
					} else {
						agregar = false;
						break;
					}
				}
				if (agregar) {
					estaticas.Push(new Bobble(new Point(x, y), arco.R / 3, (int)rnd.Next(0, 7)));
					cont++;
				}
			}
		}
	}
}
