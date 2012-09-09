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
	public class clsGraphics : Microsoft.Xna.Framework.GameComponent
	{
		public GraphicsDeviceManager g;
		public clsGraphics(Game game)
			: base(game)
		{
			g = new GraphicsDeviceManager(game);
			g.PreferredBackBufferWidth = 1024;
			g.PreferredBackBufferHeight = 760;
			g.IsFullScreen = false;
			bounds = new Rectangle(0,0,g.PreferredBackBufferWidth, g.PreferredBackBufferHeight);
			// TODO: Construct any child components here
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{

			// TODO: Add your initialization code here

			base.Initialize();
		}
		
		private static Rectangle bounds;
		public static Rectangle getBounds() { return bounds; }
		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			// TODO: Add your update code here

			base.Update(gameTime);
		}
	}
}
