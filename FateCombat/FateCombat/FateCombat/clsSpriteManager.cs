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
	public class clsSpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
	{
		SpriteBatch spriteBatch;
		Personagem player1;
		Personagem player2;
		clsSprite Estagio;
		
		public ContentManager content;


		public clsSpriteManager(Game game)
			: base(game)
		{
			this.content = game.Content;
			// TODO: Construct any child components here
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			//font = content.Load<SpriteFont>("arial");

			base.Initialize();
		}


		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(Game.GraphicsDevice);

			// TODO: Add your initialization code here


			Estagio = new clsSprite("fatecEntrada", Vector2.Zero, Vector2.Zero);
			//Estagio = new clsSprite("busStop", Vector2.Zero, Vector2.Zero);
			player2 = new Eike(new Vector2(600,100), SpriteEffects.FlipHorizontally, 700);
			player1 = new Allyson(new Vector2(100), SpriteEffects.None, 700, player2);
			//spriteList.Add(new clsMinions(Game.Content.Load<Texture2D>("Bola"),
			//    new Vector2(100f, 100f), new Vector2(64f, 64f), 0, Point.Zero, Point.Zero, new Vector2(5)));

			base.LoadContent();
		}


		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			// TODO: Add your update code here
			player1.Update(gameTime);
			player2.Update(gameTime);
			
			
			/*foreach (clsSprite s in spriteList)
			{
				s.Update(gameTime, clsTela.getBounds());
			}

			foreach (clsMinions m in spriteList)
			{
				if (m.Collides(player))
				{
					m.seColidido();
					//				player.seColidido();
				}
			}
			*/
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
			Estagio.Draw(spriteBatch);
			player1.Draw(spriteBatch);
			player2.Draw(spriteBatch);
			/*foreach (clsSprite s in spriteList)
			{
				s.Draw(spriteBatch);

			}
			spriteBatch.DrawString(font, "Rotacao" + MathHelper.ToRadians(MathHelper.ToDegrees(player.vertRotation()) % 360).ToString() //player.rotation.ToString() 
				+ " Vert " + (MathHelper.ToDegrees(player.vertRotation()) % 360).ToString(), new Vector2(100, 200), Color.Black);
			spriteBatch.DrawString(font, "Tartaruga" + player.pontosPos.ToString() //player.rotation.ToString() 
				, new Vector2(100, 0), Color.Black);
			spriteBatch.DrawString(font, "Coelho" + coelho.pontosPos.ToString() //player.rotation.ToString() 
					, new Vector2(100, 100), Color.Black);
			
			*/
			spriteBatch.End();
			
			base.Draw(gameTime);
		}
	}
}
