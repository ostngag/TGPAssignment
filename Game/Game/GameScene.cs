using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class GameScene : Scene
	{
		// Private		
		// Lose Screen		
		public MenuScene menu;
		private BgmPlayer musicPlayer = new Bgm("/Application/audio/fight.mp3").CreatePlayer();
		
		private SpriteUV loseBackground = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Lose/loseScreen.png"));
		private SpriteUV restart = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Lose/restartY.png")); 
		private SpriteUV quit = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Lose/saveQuitY.png")); 
		private static TouchStatus  currentTouchStatus;
		
		// All entities within the GameScene
		private static Sce.PlayStation.HighLevel.GameEngine2D.Label scoreLabel;
		private static Sce.PlayStation.HighLevel.GameEngine2D.Label highScoreLabel;
		private static Sce.PlayStation.HighLevel.GameEngine2D.Label waveLabel;
		
		private Player 				player;
		private SceneObstruction[] 	sceneObject;
		
		private EnemySpawner[] enemySpawner;
		
		// Two TextureInfo variables so that more than one variable can be passed into entities
		private TextureInfo 		textureInfo1;
		private TextureInfo 		textureInfo2;
		
		private SpriteUV 			background;
		private Collision 			collChecker;	
		
		// Allow for continous firing
		private bool 				attacking = false;		
		
		// Wave
		private static int 			wave = 1;
		
		// Score
		private static int			highScore = 0;
		private static int 			currentScore = 0;
		
		// Public
		public GameScene(MenuScene menu)
		{
			this.Camera.SetViewFromViewport();						
				
			collChecker = new Collision();
			
			this.menu = menu;
			
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
			SpriteUV[,] playerSprites = new SpriteUV[2,2];
			playerSprites[0,0] = new SpriteUV(new TextureInfo("/Application/textures/PlayerWalk1.png"));
			playerSprites[0,1] = new SpriteUV(new TextureInfo("/Application/textures/PlayerWalk2.png"));
			playerSprites[1,0] = new SpriteUV(new TextureInfo("/Application/textures/PlayerFire1.png"));
			playerSprites[1,1] = new SpriteUV(new TextureInfo("/Application/textures/PlayerFire2.png"));
			
			textureInfo2 		= new TextureInfo("/Application/textures/Trans.png");
			player 				= new Player(this, playerSprites, textureInfo2);			
			
			// Enemy Textures
			SpriteUV[,] enemySprites = new SpriteUV[2,2];
			enemySprites[0,0] = new SpriteUV(new TextureInfo("/Application/textures/EnemyWalk1.png"));
			enemySprites[0,1] = new SpriteUV(new TextureInfo("/Application/textures/EnemyWalk2.png"));
			enemySprites[1,0] = new SpriteUV(new TextureInfo("/Application/textures/EnemyAttack1.png"));
			enemySprites[1,1] = new SpriteUV(new TextureInfo("/Application/textures/EnemyAttack2.png"));
			
			// FOR DEBUGGING LOCATION OF SPAWNERS
			//textureInfo2		= new TextureInfo("/Application/textures/Black.png");
			
			// Enemy Spawners
			enemySpawner = new EnemySpawner[4];	
			// Left Side of Arena
			enemySpawner[0] = new EnemySpawner(this, player, enemySprites,
			                                   (background.Position.X-((background.Quad.S.X*background.Scale.X)/2)) + 250.0f,
			                                   ((background.Quad.S.Y/2) + background.Position.Y) - 50.0f,
			                                   -200.0f, 0.0f); 
			// Right Side of Arena
			enemySpawner[1] = new EnemySpawner(this, player, enemySprites,
			                                   (background.Position.X+((background.Quad.S.X*background.Scale.X)/2)) + 125.0f,
			                                   (background.Quad.S.Y/2) + background.Position.Y,
			                                   1000.0f, 0.0f); 
			// Top of Arena
			enemySpawner[2] = new EnemySpawner(this, player, enemySprites,
			                                   background.Position.X + 200.0f, 
			                                   ((background.Quad.S.Y*background.Scale.Y)/2.0f) + 200.0f,
			                                   0.0f, 700.0f); 
			// Bottom of Arena
			enemySpawner[3] = new EnemySpawner(this, player, enemySprites,
			                                   background.Position.X + 200.0f, 
			                                   ((-background.Quad.S.Y*background.Scale.Y)/2.0f) + 350.0f,
			                                   0.0f, -250.0f); 
			
			// All scene objects
			textureInfo1	= new TextureInfo("/Application/textures/Trans.png");
			SetUpSceneObjects(textureInfo1);
			
			// Initialise Lose Screen
			InitialiseLoseScreen();
			
			// Labels
			scoreLabel = new Sce.PlayStation.HighLevel.GameEngine2D.Label();
			//scoreLabel.Color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
			scoreLabel.Position = new Vector2(670.0f, 450.0f);
			scoreLabel.Scale = new Vector2(2.5f, 2.5f);
			scoreLabel.Text = "Score: 0";
			AddChild(scoreLabel);
			
			highScoreLabel = new Sce.PlayStation.HighLevel.GameEngine2D.Label();
			//highScoreLabel.Color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
			highScoreLabel.Position = new Vector2(600.0f, 500.0f);
			highScoreLabel.Scale = new Vector2(2.5f, 2.5f);
			highScoreLabel.Text = "Highscore: " + highScore;
			AddChild(highScoreLabel);
			
			waveLabel = new Sce.PlayStation.HighLevel.GameEngine2D.Label();
			//waveLabel.Color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
			waveLabel.Position = new Vector2(50.0f, 500.0f);
			waveLabel.Scale = new Vector2(2.5f, 2.5f);
			waveLabel.Text = "Wave: " + wave;
			AddChild(waveLabel);
			
			// Music
			musicPlayer.Volume = 0.5f;
			musicPlayer.LoopStart = 3.245d; 
			musicPlayer.LoopEnd = 54.425d;
			musicPlayer.Loop = true;
			musicPlayer.Play();
			
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
		
		public void InitialiseLoseScreen()
		{
			// Background
			loseBackground.Quad.S.X = Director.Instance.GL.Context.GetViewport().Width;
			loseBackground.Quad.S.Y  = Director.Instance.GL.Context.GetViewport().Height;
			loseBackground.Position = new Vector2(-10000, -10000);
			//loseBackground.Position = new Vector2(0.0f,0.0f);
			AddChild(loseBackground);
			
			restart.Quad.S.X = 166;
			restart.Quad.S.Y = 81;
			restart.Position = new Vector2(-10000, -10000);
			//restart.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,330.0f);
			AddChild(restart);
			
			quit.Quad.S.X = 166;
			quit.Quad.S.Y = 81;
			quit.Position = new Vector2(-10000, -10000);
			//quit.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,130.0f);
			AddChild(quit);
			
		}
		
		public override void Update(float dt)
		{	
			if(!player.IsAlive())
			{		
				musicPlayer.Stop();
				LoseScreen();			
			}				
			else
			{
				SortLabels();			
			
				// Add all entities to a list
				List<Entity> entities = new List<Entity>();	
				
				entities.Add(player);
				
				for(int i = 0; i < 4; i++)
					for(int j = 0; j < enemySpawner[i].GetNoOfEnemies(); j++)			
						entities.Add(enemySpawner[i].GetEnemy(j));
				
				for(int i = 0; i < 20; i++)			
					entities.Add(sceneObject[i]);
				
				foreach(PistolBullet bullets in player.weapon.pistolBullet)
					entities.Add(bullets);
					
				
				// Update all entities
				for(int i = 0; i < 4; i++)
					enemySpawner[i].Update(dt, wave);
				
				foreach(Entity entries in entities)
					entries.Update(dt, wave);			
				
				
				// Run check methods
				// Control
				CheckInput (entities);
				
				// Ensure the player doesn't go outside the arena boundaries
				CheckArenaBoundaries(entities);
				
				CheckCollisions(entities);	
				
				// Ensure the player is always centered in the middle of the screen
				CheckPlayerPosition(entities);
				
				// Check to see if the wave is complete
				if(enemySpawner[0].GetEnemy(0).AreAllEnemiesDead())
					wave++;
			}
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
				
				// For animating the player
				if(gamePadData.AnalogLeftX != 0 || gamePadData.AnalogLeftY != 0)
					player.SetMoving(true);
				else
					player.SetMoving(false);
				
				background.Position = new Vector2(background.Position.X - (gamePadData.AnalogLeftX * 5.0f),
				                                  background.Position.Y - (-gamePadData.AnalogLeftY * 5.0f));
				for(int i = 0; i < 4; i++)
					enemySpawner[i].AddToPosition((gamePadData.AnalogLeftX * -5.0f), (gamePadData.AnalogLeftY * 5.0f));
				
				// Player Rotation
				player.Rotate(gamePadData.AnalogRightX, gamePadData.AnalogRightY);
				
				// Firing your weapon
				if(gamePadData.AnalogRightX != 0 || gamePadData.AnalogRightY != 0)
				{
					attacking = true;
					player.SetAttacking(true);
				}					
				else
				{
					attacking = false;
					player.SetAttacking(false);
				}				

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
					
					for(int i = 0; i < 4; i++)
						enemySpawner[i].AddToPosition((10.0f * FMath.Sin(angle)), (10.0f * -FMath.Cos(angle)));
				}
				else
				{
					float angle = FMath.Atan(xDiff/-yDiff);
					
				 	foreach(Entity entries in entities)
				 		if(!entries.Equals(player))
				 			entries.Move(10.0f * FMath.Sin(angle), 10.0f * -FMath.Cos(angle));
				 	
				 	background.Position = new Vector2(background.Position.X - (10.0f * -FMath.Sin(angle)),
				                                 background.Position.Y - (10.0f * FMath.Cos(angle)));	
					
					for(int i = 0; i < 4; i++)
						enemySpawner[i].AddToPosition((10.0f * FMath.Sin(angle)), (10.0f * -FMath.Cos(angle)));
				}
			}		
		}
		
		public void CheckCollisions(List<Entity> entities)
		{		
			// Check all entites against themselves for collisions			
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
			// Acquire player position
			float x = player.GetSprite().Position.X;
			float y = player.GetSprite().Position.Y;
			
			
			// If the player's position is not within -0.5 and 0.5 on both axis, then move player and all entities relative to the scene
			if(x != Director.Instance.GL.Context.GetViewport().Width*0.5f)
			{
				if(x > (Director.Instance.GL.Context.GetViewport().Width*0.5f) + 5.0f)
				{
					foreach(Entity entries in entities)
							entries.Move(-5.0f, 0.0f);
					
					background.Position = new Vector2(background.Position.X - 5.0f,				                                  
					                                  background.Position.Y);
					
					for(int i = 0; i < 4; i++)
						enemySpawner[i].AddToPosition(-5.0f, 0.0f);
				}
				
				if(x < (Director.Instance.GL.Context.GetViewport().Width*0.5f) - 5.0f)
				{
					foreach(Entity entries in entities)
							entries.Move(5.0f, 0.0f);
					
					background.Position = new Vector2(background.Position.X + 5.0f,				                                  
					                                  background.Position.Y);
					
					for(int i = 0; i < 4; i++)
						enemySpawner[i].AddToPosition(5.0f, 0.0f);
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
					
					for(int i = 0; i < 4; i++)
						enemySpawner[i].AddToPosition(0.0f, -5.0f);
				}
				
				if(y < (Director.Instance.GL.Context.GetViewport().Height*0.5f) - 5.0f)
				{
					foreach(Entity entries in entities)
							entries.Move(0.0f, 5.0f);
					
					background.Position = new Vector2(background.Position.X,				                                  
					                                  background.Position.Y + 5.0f);
					
					for(int i = 0; i < 4; i++)
						enemySpawner[i].AddToPosition(0.0f, 5.0f);
				}
			}

		}
		
		public void SortLabels()
		{
			currentScore = player.GetScore();
			
			if(currentScore > highScore)
				highScore = currentScore;
			
			scoreLabel.Text = "Score: " + currentScore;
			highScoreLabel.Text ="Highscore: " + highScore;
			waveLabel.Text = "Wave: " + wave;
		}
		
		public void LoseScreen()
		{
			// Background
			scoreLabel.Position = new Vector2(-6000.0f, -5000.0f);
			highScoreLabel.Position = new Vector2(300.0f, 500.0f);
			waveLabel.Position = new Vector2(-6000.0f, -5000.0f);
			loseBackground.Position = new Vector2(0.0f,0.0f);		
			restart.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83, 330.0f);		
			
			quit.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83, 130.0f);
			
			// Query gamepad for current state
			var gamePadData = GamePad.GetData(0);
			
			List<TouchData> touches = Touch.GetData(0);
			
			foreach (TouchData data in touches)
			{
				currentTouchStatus = data.Status;
				float xPos = ((data.X + 0.5f) * Director.Instance.GL.Context.GetViewport().Width);
				float yPos = -((data.Y + 0.5f) * Director.Instance.GL.Context.GetViewport().Height)
								+ Director.Instance.GL.Context.GetViewport().Height;
				
				if(data.Status == TouchStatus.Down)
				{					
					if( xPos > restart.Position.X &&
					   xPos < restart.Position.X + restart.Quad.S.X &&
					   yPos > restart.Position.Y &&
					   yPos < restart.Position.Y + restart.Quad.S.Y)
					{
						musicPlayer.Play();
						Reset();
					}
				}
				
				if(data.Status == TouchStatus.Down)
				{
					if( xPos > quit.Position.X &&
						xPos < quit.Position.X + quit.Quad.S.X &&
					    yPos > quit.Position.Y &&
					    yPos < quit.Position.Y + quit.Quad.S.Y)
					{
						menu.quitGame = true;
					}
				}
			}		

		}
		
		public void Reset()
		{
			// All of GameScene's changable variables are reset to their original values
			attacking = false;		
			wave = 1;		
			currentScore = 0;

			// Reset all entities and sprites
			loseBackground.Position = new Vector2(-10000, -10000);
			restart.Position = new Vector2(-10000, -10000);
			quit.Position = new Vector2(-10000, -10000);	
			
			scoreLabel.Position = new Vector2(670.0f, 450.0f);
			highScoreLabel.Position = new Vector2(600.0f, 500.0f);
			waveLabel.Position = new Vector2(50.0f, 500.0f);
			
			background.Position = new Vector2((Director.Instance.GL.Context.GetViewport().Width*0.5f) + background.Quad.S.X*1.75f,
			                                  (Director.Instance.GL.Context.GetViewport().Height*0.5f) - ((background.Quad.S.Y/2) - 55.0f));

			player.Reset();			

			for(int i = 0; i < 4; i++)			
				enemySpawner[i].Reset();
								
			for(int i = 0; i < 20; i++)
				sceneObject[i].Reset();            			          
		}
		
		public int GetHighScore(){ return highScore; }
		
		public int GetScore(){ return currentScore; }
	}
}

	
