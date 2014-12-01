using System;
using System.Collections.Generic;

using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.Core.Input;

namespace Game
{
	class SoundManager
	{
		Sound[] sound;
		SoundPlayer[] soundPlayer;
		
		public SoundManager()
		{
			////Small Explosion
			//sound = new Sound[]
			//{
			//	new Sound("/Application/sounds/smallexplosion.wav"),
			//};
			//
			//soundPlayer = new SoundPlayer[]
			//{
			//	sound[0].CreatePlayer(),
			//};
			
		}
		
		//public void Play(int index, float volume, bool soundLoop)
		//{
		//	soundPlayer[index].Play();
		//	soundPlayer[index].Volume = volume;
		//	soundPlayer[index].Loop = soundLoop;
		//}
		//
		//public void Stop(int index)
		//{
		//	soundPlayer[index].Stop();
		//}	
	}
}

