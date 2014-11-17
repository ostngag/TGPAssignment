using System;

namespace Game
{
	public class Weapon
	{
		//Private variables
		private static int weaponChosen;		
		private static int reloadSpeed;
		
		public Weapon()
		{
			//Load all weapon bullets
			
			weaponChosen = 1;
		}
		
		public void chooseWeapon()
		{
			if(weaponChosen == 2)
			{
				//SMG
			}
			else if(weaponChosen == 3)
			{
				//Shotgun
			}
			else if(weaponChosen == 4)
			{
				//Laser
			}
			else
			{
				//Pistol
			}
		}
	}
}

