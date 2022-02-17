using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	public GameObject lightingParticles;
	public GameObject burstParticles;
    public float cureValue = 1f;

	private SpriteRenderer _rederer;
    private Collider2D _collider;
    private AudioSource _audio;

	private void Awake()
	{
		_rederer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _audio = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) {
			_rederer.enabled = false;
            _audio.Play();
			lightingParticles.SetActive(false);
			burstParticles.SetActive(true);

            collision.SendMessageUpwards("AddHealth", cureValue);
            _collider.enabled = false;

			Destroy(gameObject, 2f);
		}
	}
}
