namespace Slumber;

public class AudioManager
{
  public Song Gardens { get; set; }
  public Song TitleScreen { get; set; }

  public Song CurrentSong { get; set; }

  private bool isFading = false;
  private float fadeDuration = 2f;
  private float fadeTimer = 0f;
  private float startVolume = 1f;
  private float endVolume = 0f;
  private Action? onFadeFinish = null;

  public AudioManager()
  {
    Gardens = Core.Resource.Load<Song>("Audio/Music/gardens2radiomixed");
    TitleScreen = Core.Resource.Load<Song>("Audio/Music/menuchords");
    
    MediaPlayer.IsRepeating = true;
  }

  public void Update(GameTime gameTime)
  {
    if (isFading)
    {
      fadeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

      float progress = MathHelper.Clamp(fadeTimer / fadeDuration, 0f, 1f);

      MediaPlayer.Volume = MathHelper.Lerp(startVolume, endVolume, progress);

      if (progress >= 1f)
      {
        if (endVolume <= 0)
          Stop();
        onFadeFinish?.Invoke();
        isFading = false;
      }
    }
  }

  public void FadeIn(Action? onFinish = null, float duration = 2f)
  {
    isFading = true;
    fadeTimer = 0f;
    startVolume = MediaPlayer.Volume;
    endVolume = 1f;
    onFadeFinish = onFinish;
    fadeDuration = duration;
  } 

  public void FadeOut(Action? onFinish = null, float duration = 2f)
  {
    isFading = true;
    fadeTimer = 0f;
    startVolume = MediaPlayer.Volume;
    endVolume = 0f;
    onFadeFinish = onFinish;
    fadeDuration = duration;
  }

  public void Play(Song newSong)
  {
    if (CurrentSong != newSong)
    {
      MediaPlayer.Play(newSong);
      CurrentSong = newSong;
    }
  }

  public void Stop()
  {
    MediaPlayer.Stop();
  }

}
