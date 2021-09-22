using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public TrailController TrailController;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;

    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _statusInfo;
    public Text judul;

    private bool _isGameEnded = false;

    void Start()
    {
        judul.text = SceneManager.GetActiveScene().name;
        for(int i = 0; i < Birds.Count; i++)
        {
            if(Birds.Count-1 != i){
                Birds[i].OnBirdDestroyed += ChangeBird;
            }else{
                Birds[i].OnBirdDestroyed += BurungHabis;
            }
            Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;

        SlingShooter.InitiateBird(Birds[0]);

        _shotBird = Birds[0];
    }

    void Update(){
        if (!_isGameEnded)
        {
            return;
        }
        if (Input.GetKeyDown (KeyCode.R))
        {
            SceneManager.LoadScene ("Level 0");
        }
    }

    public void BurungHabis(){
        _isGameEnded = true;
        if(Enemies.Count == 0 && SceneManager.GetActiveScene().name == "Level 0"){
            SceneManager.LoadScene("Level 1");
            return;
        }
        _statusInfo.text = Enemies.Count == 0 ? "You Win!" : "You Lose!";
        _statusInfo.color = Enemies.Count == 0 ? Color.blue : Color.red;
        _panel.gameObject.SetActive (true);
    }

    public void ChangeBird()
    {   

        if (_isGameEnded)
        {
            return;
        }

        TapCollider.enabled = false;

        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count == 0)
        {
            BurungHabis();
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}
