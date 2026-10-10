namespace Slumber;

public class AudioManager
{
  public Song Gardens { get; set; }

  public AudioManager()
  {
    Gardens = Core.Resource.Load<Song>("Audio/Music/gardens2radiomixed");

    MediaPlayer.IsRepeating = true;
  }

  public void Update(GameTime gameTime)
  {
    if (Core.Token.Anchor.GetCurrentAnchor().GetType().Name.Contains("Gardens"))
    {
      if (MediaPlayer.State != MediaState.Playing)
      {
        MediaPlayer.Play(Gardens);
      }
    }
  }
}
