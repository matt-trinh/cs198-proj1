using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

	public float slowdownFactor = .90f;
	public float slowdownLength = 3f;
	public float timeMeter = 100;

	public float drainRate = 0.66f;
	public float restoreRate = 0.66f * 2f;
	public float waitTime = 1.5f;

	private bool restoring = false;

	public Slider slider;
	public GameObject player;
	private PlayerController playerController;
	private float originalSpeed;
	private float originalTilt;

	void Start () {
		playerController = player.GetComponent("PlayerController") as PlayerController;
		originalSpeed = playerController.speed;
		originalTilt = playerController.tilt;
	}

	void Update ()
	{	if (!PauseMenu.isPaused) {
			Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
			Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
			timeMeter = Mathf.Clamp(timeMeter, 0f, 100f);
			slider.value = timeMeter;
			if (restoring && timeMeter < 100f) {
				timeMeter += restoreRate;
			}
		}
	}

	public void BulletTime()
	{
		// Drain time meter at a certain rate. Slows down time according to slowdownFactor.
		if (timeMeter > 0) {
			Time.timeScale = slowdownFactor;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			timeMeter -= drainRate;
			restoring = false;
			adjustPlayer(5f);
		} else {
			adjustPlayer(1f);
		}
	}

	public void RestoreTime()
	{
		// Create a coroutine to count down a couple of seconds to prevent overusage.
		adjustPlayer(1f);
		StartCoroutine (Rest ());
	}

	IEnumerator Rest ()
	{
		// Simple timer then sets desired property to true.
		yield return new WaitForSeconds (waitTime);
		restoring = true;
		yield return null;
	}

	private void adjustPlayer(float speedMod) {
		playerController.speed = originalSpeed * speedMod;
		playerController.tilt = originalTilt / speedMod; // make tilting less strange
	}
}
