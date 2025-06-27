using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ScareCrow : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;

    private SpriteRenderer _spriteSC;
    private SpriteRenderer _spritePlayer;
    private Color _colorSC;
    private Color _colorPlayer;
    private GameObject _player;
    private float timeLife = 7.7f;
    private float timeWait = 2;
    private float timeCount = 0;

    private void Start()
    {

    }

    private IEnumerator BurnSC()
    {
        timeCount = 0;
        _colorSC.a = 1;
        _colorPlayer.a = 0;
        _spriteSC.color = _colorSC;
        _spritePlayer.color = _colorPlayer;
        _ps.Play();
        yield return new WaitForSeconds(timeWait);
        while (timeCount < 3)
        {
            timeCount += Time.deltaTime;
            _colorSC.a -= (Time.deltaTime / (2));
            _colorPlayer.a += (Time.deltaTime / (2));
            _spriteSC.color = _colorSC;
            _spritePlayer.color = _colorPlayer;
            yield return null;
        }
        while(_ps.IsAlive(true))
        {
            yield return null;
        }
        _player.GetComponent<PlayerMovement>().SetCanMove(true);
        _player.GetComponent<PlayerMovement>().SetHasTarget(true);
        gameObject.SetActive(false);
        _colorSC.a = 1;
        _spriteSC.color = _colorSC;
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }   

    public void Burn()
    {
        _spriteSC = GetComponent<SpriteRenderer>();
        _spritePlayer = _player.GetComponent<SpriteRenderer>();
        _colorSC = _spriteSC.color;
        _colorPlayer = _spritePlayer.color;
        StartCoroutine(BurnSC());
    }    
}
