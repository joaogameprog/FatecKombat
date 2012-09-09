using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FateCombat
{
	abstract class Personagem : clsSprite
	{

		string nome;
		//Rectangle location;
		int hp;
		int atk;
		int def;
		int special;
		int peso;
		bool noAr;
		int stageFloor;
		Point idle_anim_st;
		Point idle_anim_fn;
		bool isAgachado = false;
		bool isNormal = true;
		protected bool isPulando= false;
		bool isDefendendo = false;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="textureRef">Nome da referência da textura.</param>
		/// <param name="position">Posição Inicial na tela.</param>
		/// <param name="frameSize">Tamanho de cada frame.</param>
		/// <param name="velocity">Velocidade aplicada no movimento.</param>
		/// <param name="currentFrame">Frame atual</param>
		/// <param name="frameInicial">Inicio da animação de Idle - Frame inicial Padrão</param>
		/// <param name="frameFinal">Fim da animação de Idle - Frame final Padrão</param>
		/// <param name="sheetSize">Tamanho da FrameSheet com origem (0,0). Ex FrameSheet tamanho 4x4 será iniciado com [(3,3)]</param>
		/// <param name="millisecondsPerFrame">Tempo para mudança de frame em miliSegundo (mS).</param>
		/// <param name="color">Filtro de Cor - Padrão Color.White </param>
		/// <param name="scale">Escala - Tamanho natural = 1</param>
		/// <param name="imgFx">Efeito Espelhado</param>
		/// <param name="layer">Camada da Sprite</param>
		/// <param name="imgFx"></param>
		/// <param name="color"></param>
		/// <param name="nome"></param>
		/// <param name="hp"></param>
		/// <param name="atk"></param>
		/// <param name="def"></param>
		/// <param name="peso"></param>
		/// <param name="stageFloor"></param>
		public Personagem(String textureRef, Vector2 position, Vector2 velocity,
			Vector2 frameSize, Point sheetSize, Point currentFrame, Point frameInicial, Point frameFinal,
			int millisecondsPerFrame, float scale, float layer, SpriteEffects imgFx, Color color,
			// propriedades do personagem
			string nome, int hp, int atk, int def, int peso, int stageFloor) :
			base(textureRef, position, velocity,
			 frameSize, sheetSize, currentFrame, frameInicial, frameFinal,
			 millisecondsPerFrame, scale, layer, imgFx, color)
		{
			
			this.idle_anim_st = frameInicial;
			this.idle_anim_fn = frameFinal;
			this.nome = nome;
			this.hp = hp;
			this.def = def;
			this.peso = peso;
			this.stageFloor = stageFloor;
		}

		private void ColisaoChao()
		{
			if (Bottom > stageFloor){
				setY(stageFloor - frameSize.Y);
				isPulando = false;
			}
		}

		private void Gravidade()
		{
			if (noAr || position.Y < stageFloor) 
			{
				setY(position.Y + (peso * 0.5f));
				ColisaoChao();
			}
		}

		




		public override void Update(GameTime gameTime) 
		{
			base.Update(gameTime);
			Gravidade();
			ColisaoBorda();
			//if (this == 
		}

		private void UsaSpecial()
		{
			if (special > 100)
			{
				special -= 100;
				Especial();
			}
		}

		virtual protected void Especial() { }

		protected void ladoOlhar(Personagem otherPlayer)
		{
			if (position.X < otherPlayer.position.X) // player está do lado esquerdo
			{
				imgFx = SpriteEffects.None;
				otherPlayer.imgFx = SpriteEffects.FlipHorizontally;
			}
			else // if (Right > otherPlayer.Right) // player está do lado direito
			{
				imgFx = SpriteEffects.FlipHorizontally;
				otherPlayer.imgFx = SpriteEffects.None;
			}
		}

		protected void DistanciaMinima(Personagem otherPlayer)
		{
			Rectangle rectPlayer = new Rectangle((int)(Left + (MediumSize.X / 4)), (int)Top, (int)(MediumSize.X / 4), (int)(frameSize.Y));
			if (Collides(otherPlayer,rectPlayer))
			{
				if (position.X < otherPlayer.position.X) // player está do lado esquerdo
				{
					imgFx = SpriteEffects.None;
					otherPlayer.imgFx = SpriteEffects.FlipHorizontally;
					int deltaIntersect = (distancia(otherPlayer,rectPlayer).Width);
					if (deltaIntersect < 0)
					{
						setX(position.X - (int)(Math.Abs(deltaIntersect) * 0.25));				// <--
						otherPlayer.setX(otherPlayer.position.X + (int)(Math.Abs(deltaIntersect)));	// ----->
					}
				}
				else // if (Right > otherPlayer.Right) // player está do lado direito
				{
					imgFx = SpriteEffects.FlipHorizontally;
					otherPlayer.imgFx = SpriteEffects.None;
					int deltaIntersect = (distancia(otherPlayer,rectPlayer).Left);
					if (deltaIntersect <0)
					{
						setX(position.X + (int)Math.Abs(deltaIntersect * 0.25));				//    -->
						otherPlayer.setX(otherPlayer.position.X - (int)Math.Abs(deltaIntersect));	// <-----
					}
				}
			}
		}
	}
}
