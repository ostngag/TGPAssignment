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
		//Private Variables
		private Player 		player;
		private Enemy[] 	enemy;
		
		private TextureInfo textureInfo;
		private SpriteUV 	background;
		
		private SceneObstruction[] sceneObject;
		
		private Collision collChecker;
		
		private float 		previousAnalogRightX = 0.0f;
		private float       previousAnalogRightY = 0.0f;
		private bool 		attacking = false;
		
		//enum EntityType{bullet, enemy, player, scene};
		
		public GameScene()
		{
			this.Camera.SetViewFromViewport();			
			
			collChecker = new Collision();
			
			textureInfo 		= new TextureInfo("/Application/textures/Arena2.jpg");
			background 			= new SpriteUV();
			background 			= new SpriteUV(textureInfo);
			background.Quad.S 	= textureInfo.TextureSizef;
			background.Position = new Vector2(250.0f, 0.0f);
			background.Pivot 	= new Vector2(background.Quad.S.X/2, background.Quad.S.Y/2);
			background.Scale 	= new Vector2(5.0f, 5.0f);
			AddChild(background);
			
			player = new Player(this);
			
			enemy = new Enemy[20];
			Random dX = new Random();
			Random dY = new Random();
			float x = 0;
			float y = 0;
			
			for(int i = 0; i < 20; i++)
			{
				x = (float)(10000 * dX.NextDouble());
				y = (float)(10000 * dY.NextDouble());
				
				enemy[i] = new Enemy(this, player, x, y);
			}	
			
			// All scene objects
			textureInfo			= new TextureInfo("/Application/textures/Trans.png");
			
			SetUpSceneObjects(textureInfo);
			
			
			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);
		}
		
		public void Dispose()
		{
			// Cleanup
			player.Dispose();
		}
		
		public void SetUpSceneObjects(TextureInfo textureInfo)
		{
			sceneObject 		= new SceneObstruction[24];
			
			sceneObject[0] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 150, background.Position.Y + 177,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[1] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 170, background.Position.Y + 177,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[2] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 170, background.Position.Y + 157,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
		    sceneObject[3] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 295, background.Position.Y + 177,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[4] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 275, background.Position.Y + 177,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[5] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 275, background.Position.Y + 157,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
		    sceneObject[6] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 295, background.Position.Y + 282,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[7] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 275, background.Position.Y + 282,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[8] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 275, background.Position.Y + 302,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
			sceneObject[9] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 150, background.Position.Y + 282,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[10] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 170, background.Position.Y + 282,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[11] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + 170, background.Position.Y + 302,
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
			sceneObject[12] = new SceneObstruction(this, textureInfo,
                                      			  background.Position.X + 47, background.Position.Y + 27,
                                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
			sceneObject[13] = new SceneObstruction(this, textureInfo,
				                      			  background.Position.X + 362, background.Position.Y + 27,
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
			sceneObject[14] = new SceneObstruction(this, textureInfo,
				                      			  background.Position.X + 362, background.Position.Y + 397,
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
			sceneObject[15] = new SceneObstruction(this, textureInfo,
				                      			  background.Position.X + 47, background.Position.Y + 397,
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
		}
		
		public override void Update(float dt)
		{		
			// Add all entities to a list
			List<Entity> entities = new List<Entity>();	
			
			entities.Add(player);
			
			for(int i = 0; i < 20; i++)			
				entities.Add(enemy[i]);
			
			for(int i = 0; i < 16; i++)			
				entities.Add(sceneObject[i]);
			
			foreach(PistolBullet bullets in player.weapon.pistolBullet)
				entities.Add(bullets);
				
			
			// Update all entities
			foreach(Entity entries in entities)
				entries.Update(dt);
			
			
			// Query gamepad for current state
			var gamePadData = GamePad.GetData(0);
			
			if(player.IsAlive())
			{				
				for(int i = 0; i < 20; i++)
					enemy[i].Move(gamePadData.AnalogLeftX * -5.0f, gamePadData.AnalogLeftY * 5.0f);
				
				//player.Move(gamePadData.AnalogLeftX * 5.0f, -gamePadData.AnalogLeftY * 5.0f);
				background.Position = new Vector2(background.Position.X - (gamePadData.AnalogLeftX * 5.0f),
				                                  background.Position.Y - (-gamePadData.AnalogLeftY * 5.0f));
				
				int o = 0;
				foreach(PistolBullet bulletEntries in player.weapon.pistolBullet)
				{
					player.weapon.pistolBullet[o].Move(gamePadData.AnalogLeftX * -5.0f, gamePadData.AnalogLeftY * 5.0f);
					o++;
				}
					
				player.Rotate(gamePadData.AnalogRightX, gamePadData.AnalogRightY);
				
				if(gamePadData.AnalogRightX != 0 || gamePadData.AnalogRightY != 0)
					attacking = true;
				else
					attacking = false;
				
				if(attacking)
					player.Attack();
			}
			
			CheckCollisions(entities);
		}
		
		public void CheckCollisions(List<Entity> entities)
		{
						// Use i to access array variables
			int k = 0;
			
			for(int j = 0; j < 20; j++)
			{
				k = 0;
				
				foreach(PistolBullet entries in player.weapon.pistolBullet)
				{	
					
					if(collChecker.calcCollision(player.weapon.pistolBullet[k].sprite, enemy[j].sprite))
					{
							enemy[j].Killed();
							player.weapon.pistolBullet[k].HitEntity();
					}	
					
					k++;
				}
			
				if(collChecker.calcCollision(player.GetSprite(), enemy[j].sprite))
				{
					player.Killed();
				}
				
				for(int i = 0; i < 20; i++)
					if(collChecker.calcCollision(enemy[i].sprite, enemy[j].sprite))
					{
						enemy[j].PathFind(enemy[j].sprite, enemy[i].sprite);				
					}
			}

			
			// Reuse i variable
			k = 0;	
			
			foreach(SceneObstruction entries in sceneObject)
			{
				if(collChecker.calcCollision(player.GetSprite(), sceneObject[k].GetSprite()))
				   //player.Move(-gamePadData.AnalogLeftX * 5.0f, gamePadData.AnalogLeftY * 5.0f);
				{;}
				
				//if(collChecker.calcCollision(enemy.sprite, sceneObject[k].GetSprite()))
				//{}
				
				int j = 0;
				foreach(PistolBullet bulletEntries in player.weapon.pistolBullet)
				{
					if(collChecker.calcCollision(player.weapon.pistolBullet[j].sprite, sceneObject[k].GetSprite()))
					{;}//player.weapon.pistolBullet[j].HitEntity();
					
					j++;
				}
				
					
					
				k++;
				if(k > 11)
					k = 0;
			}
		}
	}
}

	
