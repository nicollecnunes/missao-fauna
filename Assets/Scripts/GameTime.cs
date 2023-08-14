using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour {
   [Header ("Timer UI references :")]
   [SerializeField] private Image uiFillImage ;
   [SerializeField] private TMP_Text uiText ;
   
   private int time;

   public int Duration { get; private set; }

   public bool IsPaused { get; private set; }

   private int remainingDuration ;

   // Events --
   private UnityAction onTimerBeginAction ;
   private UnityAction<int> onTimerChangeAction ;
   private UnityAction onTimerEndAction ;
   private UnityAction<bool> onTimerPauseAction ;

   public void SetTime() {
      int dificuldade = PlayerPrefs.GetInt("NivelDeDificuldade", 0);
      
      if(dificuldade == 0) time = 150;
      else if(dificuldade == 1) time = 120;
      else if(dificuldade == 2) time = 90;
      else time = 60;
   }

   public int GetTime()
   {
      return time;
   }

   private void Awake () {
      ResetTimer () ;
   }

   private void ResetTimer () {
      uiText.text = "00:00" ;
      uiFillImage.fillAmount = 0f ;

      Duration = remainingDuration = 0 ;

      onTimerBeginAction = null ;
      onTimerChangeAction = null ;
      onTimerEndAction = null ;
      onTimerPauseAction = null ;

      IsPaused = false ;
   }

   public void SetPaused (bool paused) {
      IsPaused = paused ;

      if (onTimerPauseAction != null)
         onTimerPauseAction.Invoke (IsPaused) ;
   }


   public GameTime SetDuration (int seconds) {
      Duration = remainingDuration = seconds ;
      return this ;
   }

   //-- Events ----------------------------------
   public GameTime OnBegin (UnityAction action) {
      onTimerBeginAction = action ;
      return this ;
   }

   public GameTime OnChange (UnityAction<int> action) {
      onTimerChangeAction = action ;
      return this ;
   }

   public GameTime OnEnd (UnityAction action) {
      onTimerEndAction = action ;
      return this ;
   }

   public GameTime OnPause (UnityAction<bool> action) {
      onTimerPauseAction = action ;
      return this ;
   }


   public void CheckTimeIsOver()
   {

   }


   public void Begin () {
      if (onTimerBeginAction != null)
         onTimerBeginAction.Invoke () ;

      StopAllCoroutines () ;
      StartCoroutine (UpdateTimer ()) ;
   }

   private IEnumerator UpdateTimer () {
      while (remainingDuration > 0) {
         if (!IsPaused) {
            if (onTimerChangeAction != null)
               onTimerChangeAction.Invoke (remainingDuration) ;

            UpdateUI (remainingDuration) ;
            remainingDuration-- ;
         }
         yield return new WaitForSeconds (1f) ;
      }
      End () ;
   }

   private void UpdateUI (int seconds) {
      uiText.text = string.Format ("{0:D2}:{1:D2}", seconds / 60, seconds % 60) ;
      uiFillImage.fillAmount = Mathf.InverseLerp (0, Duration, seconds) ;
   }

   public void End () {
      if (onTimerEndAction != null)
         onTimerEndAction.Invoke () ;

      SceneManager.LoadScene(0);
   }


   private void OnDestroy () {
      StopAllCoroutines () ;
   }
}
