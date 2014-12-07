using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class GameScene : Scene
	{
		// Private		
		// All entities within the GameScene
		private Player 				player;
		private Enemy[] 			enemy;
		private SceneObstruction[] 	sceneObject;
		
		// Two TextureInfo variables so that more than one variable can be passed into entities
		private TextureInfo 		textureInfo1;
		private TextureInfo 		textureInfo2;
		private SpriteUV 			background;
		private Collision 			collChecker;	
		
		// Allow for continous firing
		private bool 				attacking = false;
		
		
		// Public
		public GameScene()
		{
			this.Camera.SetViewFromViewport();			
			
			collChecker = new Collision();
			
			// Setup all entities and sprites    (0.0f * (background.Scale.X - 1.0f)
			
			// Background
			textureInfo1 		= new TextureInfo("/Application/textures/Arena.png");
			background 			= new SpriteUV();
			background 			= new SpriteUV(textureInfo1);
			background.Quad.S 	= textureInfo1.TextureSizef;
			background.Scale 	= new Vector2(5.0f, 5.0f);
			background.Pivot 	= new Vector2(background.Quad.S.X/2, background.Quad.S.Y/2);
			background.Position = new Vector2((Director.Instance.GL.Context.GetViewport().Width*0.5f) + background.Quad.S.X*1.75f,
			                                  (Director.Instance.GL.Context.GetViewport().Height*0.5f) - ((background.Quad.S.Y/2) - 55.0f));
			AddChild(background);
			
			
			// Player
			textureInfo1 		= new TextureInfo("/Application/textures/Player.png");
			textureInfo2 		= new TextureInfo("/Application/textures/Black.png");
			player 				= new Player(this, textureInfo1, textureInfo2);			
			
			// Enemies
			textureInfo1 		= new TextureInfo("/Application/textures/Bullet.png");
			enemy 				= new Enemy[20];			
			// These Random variables are used to spawn the enemies at different positions
			Random dX 			= new Random();
			Random dY 			= new Random();
			float x 			= 0;
			float y 			= 0;
			
			for(int i = 0; i < 20; i++)
			{
				x 				= (float)(1200 * dX.NextDouble());
				y 				= (float)(1200 * dY.NextDouble());
				
				enemy[i] 		= new Enemy(this, player, x, y, textureInfo1);
			}	
			
			
			// All scene objects
			textureInfo1			= new TextureInfo("/Application/textures/Black.png");
			SetUpSceneObjects(textureInfo1);

			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);
		}
		
		public void Dispose()
		{
			// Cleanup
			textureInfo1.Dispose();
			textureInfo2.Dispose();
		}
		
		public void SetUpSceneObjects(TextureInfo textureInfo)
		{
			sceneObject 	= new SceneObstruction[20];
			
			// Small Blocks
			sceneObject[0]  = new SceneObstruction(this, textureInfo,
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (49 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (183 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						    
			sceneObject[1]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (-33 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (183 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						    
			sceneObject[2]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (-138 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (47 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						 												   
		    sceneObject[3]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (-138 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (-35 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						    
			sceneObject[4]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (-33 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (-171 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						    
			sceneObject[5]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (49 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (-171 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						 												   
		    sceneObject[6]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (171 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (-35 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						    
			sceneObject[7]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (171 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (47 * background.Scale.Y)) - background.Scale.Y * 12),
                                       			  background.Scale.X * 12, background.Scale.Y * 12);
						    
						    
			// Boxes	    
			sceneObject[8]  = new SceneObstruction(this, textureInfo,	   
                                       			  (background.Position.X + ((background.Quad.S.X/2) - (43 * background.Scale.X))),
			                                       (background.Position.Y + ((background.Quad.S.Y/2) + (43 * background.Scale.Y)) - background.Scale.Y * 21),
                                       			  background.Scale.X * 21, background.Scale.Y * 21);
						 												   
			sceneObject[9]  = new SceneObstruction(this, textureInfo,	   
                                      			  (background.Position.X + ((background.Quad.S.X/2) - (-19 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (43 * background.Scale.Y)) - background.Scale.Y * 21),
                                      			  background.Scale.X * 21, background.Scale.Y * 21);
			
			sceneObject[10] = new SceneObstruction(this, textureInfo,	  
                                      			  (background.Position.X + ((background.Quad.S.X/2) - (-19 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (-19 * background.Scale.Y)) - background.Scale.Y * 21),
                                      			  background.Scale.X * 21, background.Scale.Y * 21);
			
			sceneObject[11] = new SceneObstruction(this, textureInfo,	  
                                      			  (background.Position.X + ((background.Quad.S.X/2) - (43 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (-19 * background.Scale.Y)) - background.Scale.Y * 21),
                                      			  background.Scale.X * 21, background.Scale.Y * 21);
																		  
			
			// Large Blocks
			sceneObject[12] = new SceneObstruction(this, textureInfo,	  
                                      			  (background.Position.X + ((background.Quad.S.X/2) - (95 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (109 * background.Scale.Y)) - background.Scale.Y * 29),
                                      			  background.Scale.X * 29, background.Scale.Y * 29);
																		   
			sceneObject[13] = new SceneObstruction(this, textureInfo,	  
				                      			  (background.Position.X + ((background.Quad.S.X/2) - (-63 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (109 * background.Scale.Y)) - background.Scale.Y * 29),
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
																		   
			sceneObject[14] = new SceneObstruction(this, textureInfo,	  
				                      			  (background.Position.X + ((background.Quad.S.X/2) - (-63 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (-75 * background.Scale.Y)) - background.Scale.Y * 29),
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
																		   
			sceneObject[15] = new SceneObstruction(this, textureInfo,	  
				                      			  (background.Position.X + ((background.Quad.S.X/2) - (95 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (-75 * background.Scale.Y)) - background.Scale.Y * 29),
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
			// Scene Boundaries
			sceneObject[16] = new SceneObstruction(this, textureInfo,	  
				                      			  (background.Position.X + ((background.Quad.S.X/2) - (-236 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (336 * background.Scale.Y)) - background.Scale.Y * 700),
				                      			  background.Scale.X * 100, background.Scale.Y * 700);
			
			sceneObject[17] = new SceneObstruction(this, textureInfo,	  
				                      			  (background.Position.X + ((background.Quad.S.X/2) - (336 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (336 * background.Scale.Y)) - background.Scale.Y * 700),
				                      			  background.Scale.X * 100, background.Scale.Y * 700);
			
			sceneObject[18] = new SceneObstruction(this, textureInfo,	  
				                      			  (background.Position.X + ((background.Quad.S.X/2) - (336 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (336 * background.Scale.Y)) - background.Scale.Y * 100),
				                      			  background.Scale.X * 700, background.Scale.Y * 100);
			
			sceneObject[19] = new SceneObstruction(this, textureInfo,	  
				                      			  (background.Position.X + ((background.Quad.S.X/2) - (336 * background.Scale.X))),
			                                      (background.Position.Y + ((background.Quad.S.Y/2) + (-236 * background.Scale.Y)) - background.Scale.Y * 100),
				                      			  background.Scale.X * 700, background.Scale.Y * 100);			
		}
		
		public override void Update(float dt)
		{		
			// Add all entities to a list
			List<Entity> entities = new List<Entity>();	
			
			entities.Add(player);
			
			for(int i = 0; i < 20; i++)			
				entities.Add(enemy[i]);
			
			for(int i = 0; i < 20; i++)			
				entities.Add(sceneObject[i]);
			
			foreach(PistolBullet bullets in player.weapon.pistolBullet)
				entities.Add(bullets);
				
			
			// Update all entities
			foreach(Entity entries in entities)
				entries.Update(dt);
			
			CheckInput (entities);
			
			CheckArenaBoundaries(entities);
			
			CheckCollisions(entities);	
			
			CheckPlayerPosition(entities);
		}
		
		public void CheckInput(List<Entity> entities)
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData(0);
			
			if(player.IsAlive())
			{							
				// Movement
				// The player sprite does not move from the middle of the screen, instead the world scene moves around the player.
				
				foreach(Entity entries in entities)
					if(!entries.Equals(player))
						entries.Move(gamePadData.AnalogLeftX * -5.0f, gamePadData.AnalogLeftY * 5.0f);
				
				background.Position = new Vector2(background.Position.X - (gamePadData.AnalogLeftX * 5.0f),
				                                  background.Position.Y - (-gamePadData.AnalogLeftY * 5.0f));
				
				// Player Rotation
				player.Rotate(gamePadData.AnalogRightX, gamePadData.AnalogRightY);
				
				// Firing your weapon
				if(gamePadData.AnalogRightX != 0 || gamePadData.AnalogRightY != 0)
					attacking = true;
				else
					attacking = false;
				
				if(attacking)
					player.Attack();
			}
		}
		
		public void CheckArenaBoundaries(List<Entity> entities)
		{
			SpriteUV sprite = player.GetSprite();
			float xDiff = sprite.Position.X - (background.Position.X + background.Quad.S.X/2.0f);			
			float yDiff = sprite.Position.Y - (background.Position.Y + background.Quad.S.Y/2.0f);
			
			// Distance from the middle of the arena
			float radialDistance = FMath.Sqrt(FMath.Pow(xDiff, 2.0f)
			                                  + FMath.Pow(yDiff, 2.0f));			
			
			// If the distance + the player sprite's longest side is more than 230, then the boundary has been reached
			if(radialDistance + sprite.Quad.S.Y > (235.0f * background.Scale.Y))
			{
				if(yDiff > 0)
				{					
					float angle = FMath.PI - FMath.Atan(xDiff/yDiff);				
					
				 	foreach(Entity entries in entities)
				 		if(!entries.Equals(player))
				 			entries.Move(10.0f * FMath.Sin(angle), 10.0f * -FMath.Cos(angle));
				 	
				 	background.Position = new Vector2(background.Position.X - (10.0f * -FMath.Sin(angle)),
				                                 background.Position.Y - (10.0f * FMath.Cos(angle)));					
				}
				else
				{
					float angle = FMath.Atan(xDiff/-yDiff);
					
				 	foreach(Entity entries in entities)
				 		if(!entries.Equals(player))
				 			entries.Move(10.0f * FMath.Sin(angle), 10.0f * -FMath.Cos(angle));
				 	
				 	background.Position = new Vector2(background.Position.X - (10.0f * -FMath.Sin(angle)),
				                                 background.Position.Y - (10.0f * FMath.Cos(angle)));	
				}
			}		
		}
		
		public void CheckCollisions(List<Entity> entities)
		{		
			foreach(Entity currentEntries in entities)
				foreach(Entity otherEntries in entities)
					if(collChecker.calcCollision(currentEntries.GetSprite(), otherEntries.GetSprite()))
					{
						currentEntries.SortCollision(otherEntries);
						otherEntries.SortCollision(currentEntries);
					}
		}
		
		public void CheckPlayerPosition(List<Entity> entities)
		{
			float x = player.GetSprite().Position.X;
			float y = player.GetSprite().Position.Y;
			
			if(x != Director.Instance.GL.Context.GetViewport().Width*0.5f)
			{
				if(x > (Director.Instance.GL.Context.GetViewport().Width*0.5f) + 5.0f)
				{
					foreach(Entity entries in entities)
							entries.Move(-5.0f, 0.0f);
					
					background.Position = new Vector2(background.Position.X - 5.0f,				                                  
					                                  background.Position.Y);
				}
				
				if(x < (Director.Instance.GL.Context.GetViewport().Width*0.5f) - 5.0f)
				{
					foreach(Entity entries in entities)
							entries.Move(5.0f, 0.0f);
					
					background.Position = new Vector2(background.Position.X + 5.0f,				                                  
					                                  background.Position.Y);
				}
			}
			
			if(y != Director.Instance.GL.Context.GetViewport().Height*0.5f)
			{
				if(y > (Director.Instance.GL.Context.GetViewport().Height*0.5f) + 5.0f)
				{
					foreach(Entity entries in entities)
							entries.Move(0.0f, -5.0f);
					
					background.Position = new Vector2(background.Position.X,				                                  
					                                  background.Position.Y - 5.0f);
				}
				
				if(y < (Director.Instance.GL.Context.GetViewport().Height*0.5f) - 5.0f)
				{
					foreach(Entity entries in entities)
							entries.Move(0.0f, 5.0f);
					
					background.Position = new Vector2(background.Position.X,				                                  
					                                  background.Position.Y + 5.0f);
				}
			}

		}
	}
}

	
