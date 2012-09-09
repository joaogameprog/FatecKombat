using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace FateCombat
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class clsSprite
	{

		/////////////////////////////////////////
		///     Propriedades do Draw   //////////
		/////////////////////////////////////////
		public Texture2D texture { get; set; }  //ok
		public Vector2 position { get; set; }	//ok
		//public Vector2 screenSize { get; set; }
		public Vector2 frameSize { get; set; }	//ok
		public Point currentFrame;				//ok
		public Point frameInicial;
		public Point frameFinal;
		private Rectangle rectSize() { return new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y); }
		private Rectangle rectDesenhado(){return new Rectangle(currentFrame.X * (int)frameSize.X, currentFrame.Y * (int)frameSize.Y, (int)frameSize.X, (int)frameSize.Y);}
		private Color filterColor;		//ok
		public float rotation { get; set; }		
		public Vector2 origem { get; set; }		
		public float scale;					//ok
		public SpriteEffects imgFx { get; set; }//ok
		public float layer { get; set; }		//ok
		public Vector2 velocity { get; set; }
		protected bool animNormal = true;
		private Point animNormalIni;
		private Point animNormalFin;
		private int animNormalMillissecondPerFrame;



		public void setX(float newPosX){position = new Vector2(newPosX, position.Y);}
		public void setY(float newPosY) { position = new Vector2(position.X, newPosY); }
		/// <summary>
		/// Retorna o tamanho médio da sprite
		/// </summary>
		public Vector2 MediumSize { get { return (frameSize / 2); } } // centro da sprite
		
		/// <summary>
		/// Retorna a posicao na tela do CENTRO da sprite
		/// </summary>
		public Point center { get { return new Point((int)(position.X + frameSize.X / 2), (int)(position.Y + frameSize.Y / 2)); } } // centro da sprite
		
		/// <summary>
		/// Retorna a posicao na tela da parte da ESQUERDA da sprite
		/// </summary>
		public float Left { get { return (position.X); } }	
		
		/// <summary>
		/// Retorna a posicao na tela da parte da DIREITA da sprite
		/// </summary>
		public float Right { get { return (position.X + frameSize.X); } }	// 
	
		/// <summary>
		/// Retorna a posicao na tela da parte de CIMA da sprite
		/// </summary>
		public float Top { get { return (position.Y); } }	
		
		/// <summary>
		/// Retorna a posicao na tela da parte de BAIXO da sprite
		/// </summary>
		public float Bottom { get { return (position.Y + frameSize.Y); } }

		public bool isAlive = true;
		//public float vertRotation() { return (float)(this.rotation - Math.PI / 2); }

		const int defaultMillisecondsPerFrame = 16;
		private int timeSinceLastFrame = 0;
		protected int millisecondsPerFrame;
		protected Point sheetSize;



		/// <summary>
		/// Objetos Simples
		/// </summary>
		/// <param name="textureRef">Nome da referência da textura.</param>
		/// <param name="position">Posição Inicial na tela.</param>
		/// <param name="velocity">Velocidade aplicada no movimento.</param>
		public clsSprite(String texture, Vector2 position, Vector2 velocity) :
			this(texture, position, velocity, 
			Vector2.Zero, Point.Zero, Point.Zero, Point.Zero, Point.Zero)
		{
		}

		/// <summary>
		/// Objetos com Sprite Sheet com tempo de Update DEFAULT.
		/// </summary>
		/// <param name="textureRef">Nome da referência da textura.</param>
		/// <param name="position">Posição Inicial na tela.</param>
		/// <param name="frameSize">Tamanho de cada frame.</param>
		/// <param name="velocity">Velocidade aplicada no movimento.</param>
		/// <param name="currentFrame">Frame atual</param>
		/// <param name="frameInicial">Frame inicial Padrão</param>
		/// <param name="frameFinal">Frame final Padrão</param>
		/// <param name="sheetSize">Tamanho da FrameSheet com origem (0,0). Ex FrameSheet tamanho 4x4 será iniciado com [(3,3)]</param>
		public clsSprite(String textureRef, Vector2 position, Vector2 velocity,
			Vector2 frameSize, Point currentFrame, Point frameInicial, Point frameFinal, Point sheetSize) :
			this(textureRef, position, velocity,
			frameSize, sheetSize, currentFrame, frameInicial, frameFinal,
			defaultMillisecondsPerFrame)
		{
		}


		/// <summary>
		/// Objetos com Sprite Sheet E tempo de Update Personalizado
		/// </summary>
		/// <param name="textureRef">Nome da referência da textura.</param>
		/// <param name="position">Posição Inicial na tela.</param>
		/// <param name="frameSize">Tamanho de cada frame.</param>
		/// <param name="velocity">Velocidade aplicada no movimento.</param>
		/// <param name="currentFrame">Frame atual</param>
		/// <param name="frameInicial">Frame inicial Padrão</param>
		/// <param name="frameFinal">Frame final Padrão</param>
		/// <param name="sheetSize">Tamanho da FrameSheet com origem (0,0). Ex FrameSheet tamanho 4x4 será iniciado com [(3,3)]</param>
		/// <param name="millisecondsPerFrame">Tempo para mudança de frame em miliSegundo (mS).</param>
		public clsSprite(String textureRef, Vector2 position, Vector2 velocity,
			Vector2 frameSize, Point sheetSize, Point currentFrame, Point frameInicial, Point frameFinal,
			int millisecondsPerFrame) :
			this(textureRef, position, velocity,
			 frameSize, sheetSize, currentFrame, frameInicial, frameFinal,
			 defaultMillisecondsPerFrame,1, 0)
		{
		}


		/// <summary>
		/// Objetos com Sprite Sheet E tempo de Update Personalizado
		/// </summary>
		/// <param name="textureRef">Nome da referência da textura.</param>
		/// <param name="position">Posição Inicial na tela.</param>
		/// <param name="frameSize">Tamanho de cada frame.</param>
		/// <param name="velocity">Velocidade aplicada no movimento.</param>
		/// <param name="currentFrame">Frame atual</param>
		/// <param name="frameInicial">Frame inicial Padrão</param>
		/// <param name="frameFinal">Frame final Padrão</param>
		/// <param name="sheetSize">Tamanho da FrameSheet com origem (0,0). Ex FrameSheet tamanho 4x4 será iniciado com [(3,3)]</param>
		/// <param name="millisecondsPerFrame">Tempo para mudança de frame em miliSegundo (mS).</param>
		public clsSprite(String textureRef, Vector2 position, Vector2 velocity,
			Vector2 frameSize, Point sheetSize, Point currentFrame, Point frameInicial, Point frameFinal,
			int millisecondsPerFrame, float scale, float layer) :
			this(textureRef, position, velocity,
			 frameSize, sheetSize, currentFrame, frameInicial, frameFinal,
			 defaultMillisecondsPerFrame, scale, layer, 
			SpriteEffects.None) 
		{
		}

		/// <summary>
		/// Objetos com Sprite Sheet E tempo de Update Personalizado
		/// </summary>
		/// <param name="textureRef">Nome da referência da textura.</param>
		/// <param name="position">Posição Inicial na tela.</param>
		/// <param name="frameSize">Tamanho de cada frame.</param>
		/// <param name="velocity">Velocidade aplicada no movimento.</param>
		/// <param name="currentFrame">Frame atual</param>
		/// <param name="frameInicial">Frame inicial Padrão</param>
		/// <param name="frameFinal">Frame final Padrão</param>
		/// <param name="sheetSize">Tamanho da FrameSheet com origem (0,0). Ex FrameSheet tamanho 4x4 será iniciado com [(3,3)]</param>
		/// <param name="millisecondsPerFrame">Tempo para mudança de frame em miliSegundo (mS).</param>
		public clsSprite(String textureRef, Vector2 position, Vector2 velocity,
			Vector2 frameSize, Point sheetSize, Point currentFrame, Point frameInicial, Point frameFinal,
			int millisecondsPerFrame, float scale, float layer, SpriteEffects imgFx) :
			this(textureRef, position, velocity,
			 frameSize, sheetSize, currentFrame, frameInicial, frameFinal,
			 millisecondsPerFrame, scale, layer, imgFx, Color.White)
		{
		}


		/// <summary>
		/// Sprites totalmente personalizada.
		/// </summary>
		/// <param name="textureRef">Nome da referência da textura.</param>
		/// <param name="position">Posição Inicial na tela.</param>
		/// <param name="frameSize">Tamanho de cada frame.</param>
		/// <param name="velocity">Velocidade aplicada no movimento.</param>
		/// <param name="currentFrame">Frame atual</param>
		/// <param name="frameInicial">Frame inicial Padrão</param>
		/// <param name="frameFinal">Frame final Padrão</param>
		/// <param name="sheetSize">Tamanho da FrameSheet com origem (0,0). Ex FrameSheet tamanho 4x4 será iniciado com [(3,3)]</param>
		/// <param name="millisecondsPerFrame">Tempo para mudança de frame em miliSegundo (mS).</param>
		/// <param name="color">Filtro de Cor - Padrão Color.White </param>
		/// <param name="scale">Escala - Tamanho natural = 1</param>
		/// <param name="imgFx">Efeito Espelhado</param>
		/// <param name="layer"></param>
		public clsSprite(String textureRef, Vector2 position, Vector2 velocity,
			Vector2 frameSize, Point sheetSize, Point currentFrame, Point frameInicial, Point frameFinal,
			int millisecondsPerFrame, float scale, float layer, SpriteEffects imgFx, Color color)
		{
			this.texture = LoadTexture(Game1.conteudo, textureRef);
			this.position = position;
			this.velocity = velocity;
			if (frameSize == Vector2.Zero)
			{
				frameSize = new Vector2(texture.Width, texture.Height);
				currentFrame = Point.Zero;
				frameInicial = Point.Zero;
				frameFinal = Point.Zero;
				sheetSize = Point.Zero;
			}
			this.frameSize = frameSize;
			this.currentFrame = frameInicial;
			this.frameInicial = frameInicial;
			this.animNormalIni = frameInicial;
			this.frameFinal = frameFinal;
			this.animNormalFin = frameFinal;
			this.animNormalMillissecondPerFrame = millisecondsPerFrame;
			this.sheetSize = sheetSize;
			this.millisecondsPerFrame = millisecondsPerFrame;
			this.filterColor = color;
			this.scale = scale;
			this.layer = layer;
			this.imgFx = imgFx;
			this.rotation = 0;
			this.origem = Vector2.Zero;
		}



		private Texture2D LoadTexture(ContentManager content, string textureRef)
		{ 
			
			Texture2D texture = content.Load<Texture2D>(textureRef);
			return texture;
		}

		public virtual void Update(GameTime gametime)
		{
			timeSinceLastFrame += gametime.ElapsedGameTime.Milliseconds;
			if (timeSinceLastFrame > millisecondsPerFrame)
			{
				timeSinceLastFrame = 0;
				currentFrame.X++;
				if (currentFrame.X > sheetSize.X)
				{
					currentFrame.X = 0;
					currentFrame.Y++;
					if (currentFrame.Y > sheetSize.Y)
						currentFrame.Y = 0;
				}
			}
			if (currentFrame.X > frameFinal.X && currentFrame.Y >= frameFinal.Y && animNormal)
				currentFrame = frameInicial;
			else if (currentFrame == frameFinal && animNormal == false)
			{
				AtivarAnimNormal();
			}
		}

		private void AtivarAnimNormal()
		{
			frameInicial = animNormalIni;
			currentFrame = frameInicial;
			frameFinal = animNormalFin;
			millisecondsPerFrame = animNormalMillissecondPerFrame;
			animNormal = true;
		}

		protected bool FinalFramePassed()
		{
			if (currentFrame.X > frameFinal.X && currentFrame.Y >= frameFinal.Y)
				return true;
			else
				return false;
		}
			

		public virtual void seColidido() { }


		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (isAlive)
			{
				spriteBatch.Draw(texture,
					position,
					rectDesenhado(),
					Color.White,
					rotation,
					Vector2.Zero,
					scale,
					imgFx,
					layer);
			}
			else
			{
				texture.Dispose();
			}
		}

		

		/// <summary>
		/// Testa colisão com a outra sprite
		/// </summary>
		/// <param name="otherSprite"></param>
		/// <returns></returns>
		public bool Collides(clsSprite otherSprite)
		{
			// confere se as duas caixas colidem
			if (this.rectSize().Intersects(otherSprite.rectSize()))
				return true;
			else
				return false;
		}

		public bool Collides(clsSprite otherSprite, Rectangle rectOffset)
		{
			// confere se as duas caixas colidem
			Rectangle otherRect = otherSprite.rectSize();
			if (rectOffset.Intersects(otherRect))
				return true;
			else
				return false;

			if (otherRect.Intersects(rectOffset))
				return true;
			else
				return false;
		}


		/// <summary>
		/// Testa se esta Sprite está contida na outra sprite.
		/// </summary>
		/// <param name="otherSprite"></param>
		/// <param name="retangleOfset"></param>
		/// <returns></returns>
		public bool Contains(clsSprite otherSprite)
		{
			if (this.Right > otherSprite.Left &&
				this.Left < otherSprite.Right &&
				this.Bottom > otherSprite.Top &&
				this.Top < otherSprite.Bottom)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Testa se o Retângulo de teste está contido na outra sprite.
		/// </summary>
		/// <param name="otherSprite"></param>
		/// <param name="retangleOfset"></param>
		/// <returns></returns>
		public bool Contains(clsSprite otherSprite, Rectangle retangleOfset)
		{
			// confere se as duas caixas colidem
			if (retangleOfset.Right > otherSprite.Left &&
				retangleOfset.Left < otherSprite.Right &&
				retangleOfset.Bottom > otherSprite.Top &&
				retangleOfset.Top < otherSprite.Bottom)
				return true;
			else
				return false;
		}

		protected void ColisaoBorda()
		{
			if (Left < clsGraphics.getBounds().Left)
			{
				setX(clsGraphics.getBounds().Left);
			}
			if (Right > clsGraphics.getBounds().Right)
			{
				setX(clsGraphics.getBounds().Right - frameSize.X);
			}
		}

		public bool CircleCollides(clsSprite otherSprite)
		{	// Verifica se duas sprites circulares se tocam
			return (Vector2.Distance(this.MediumSize, otherSprite.MediumSize) <
				this.MediumSize.X + otherSprite.MediumSize.X);
		}

		/// <summary>
		/// Retorna a distancia pelos lados em relação a outra sprite. Se negativo então pode estar colidindo
		/// </summary>
		/// <param name="otherSprite"></param>
		/// <returns>"Propriedade = Distância em relação à" --- X = Esquerda; Y = Cima; Width = Direita; Height = Baixo</returns>
		public Rectangle distancia(clsSprite otherSprite)
		{
			Rectangle thisRect = this.rectSize();
			Rectangle otherRect = otherSprite.rectSize();
			return new Rectangle(thisRect.Left - otherRect.Right, thisRect.Top - otherRect.Bottom,
								otherRect.Left - thisRect.Right, otherRect.Top - thisRect.Bottom);
		}

		/// <summary>
		/// Retorna a distancia pelos lados em relação a outra sprite. Se negativo então pode estar colidindo
		/// </summary>
		/// <param name="otherSprite"></param>
		/// <returns>"Propriedade = Distância em relação à" --- X = Esquerda; Y = Cima; Width = Direita; Height = Baixo</returns>
		public Rectangle distancia(clsSprite otherSprite, Rectangle rectOffset)
		{
			Rectangle otherRect = otherSprite.rectSize();
			return new Rectangle(rectOffset.Left - otherRect.Right, rectOffset.Top - otherRect.Bottom,
								otherRect.Left - rectOffset.Right, otherRect.Top - rectOffset.Bottom);
		}
	}
}
