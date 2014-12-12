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
			sound = new Sound[3];		
			sound[0] = new Sound("/Application/audio/aaahhh.wav");
			sound[1] = new Sound("/Application/audio/hit.wav");
			sound[2] = new Sound("/Application/audio/fire.wav");

			soundPlayer = new SoundPlayer[3];
			soundPlayer[0] = sound[0].CreatePlayer();
			soundPlayer[1] = sound[1].CreatePlayer();
			soundPlayer[2] = sound[2].CreatePlayer();			
		}
		
		public void Play(int index, float volume, bool soundLoop)
		{
			soundPlayer[index].Play();
			soundPlayer[index].Volume = volume;
			soundPlayer[index].Loop = soundLoop;
		}
		
		public void Stop(int index)
		{
			soundPlayer[index].Stop();
		}	
	}
}

