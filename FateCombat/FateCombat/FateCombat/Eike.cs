using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FateCombat
{
	class Eike : Personagem
	{
		public Eike(Vector2 position, SpriteEffects imgFx, int stageFloor)
			: base(
				"Eike",					//textura
				position,				//posicao
				new Vector2(5f),		//velocidade
				new Vector2(240, 300),	//frameSize
				new Point(9, 0),		//sheetSize
				Point.Zero,				//currentFrame
				Point.Zero,				//frameInicialIdle
				new Point(1, 0),		//frameFinalIdle
				600,					//milliSeconds Per Frame
				1f,						//Escala
				0.9f,					//Layer
				imgFx,					//Imagem Virada?
				Color.White,			//cor
				"Allyson",				//nome do personagem
				300,					//HP
				15,						//atk
				5,						//def
				10,						//peso
				stageFloor) { }

		public override void Update(GameTime gameTime)
		{
			if ((GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed) ||
				(Keyboard.GetState().IsKeyDown(Keys.NumPad1)))
			{
				this.frameFinal = sheetSize;
				millisecondsPerFrame = 600;
				animNormal = false;
			}
			if ((GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0) ||
				(Keyboard.GetState().IsKeyDown(Keys.Right)))
			{
				this.frameInicial = new Point(1, 0);
				this.frameFinal = new Point(4, 0);
				millisecondsPerFrame = 100;
				this.setX(Left + velocity.X);
				animNormal = false;
			}
			if ((GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0) ||
				(Keyboard.GetState().IsKeyDown(Keys.Left)))
			{
				this.frameInicial = new Point(1, 0);
				this.frameFinal = new Point(4, 0);
				millisecondsPerFrame = 100;
				this.setX(Left - velocity.X);
				animNormal = false;
			}
			if (((GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y > 0) ||
				(Keyboard.GetState().IsKeyDown(Keys.Up))) && isPulando == false)
			{
				this.frameInicial = new Point(1, 0);
				this.frameFinal = new Point(4, 0);
				millisecondsPerFrame = 100;
				this.setY(Top - (velocity.Y * 15));
				animNormal = false;
				isPulando = true;
			}
			base.Update(gameTime);
		}
	}
}
