using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
   public Snake snake;
   public Text gameOverText;
   public Text scoreText;
   
   private Vector2 _direction = Vector2.right;
   private List<Transform> _segments = new List<Transform>();
   public Transform segmentPrefab;

  public int score { get; private set; }
  public int lives { get; private set; }
   
   public int InitialSize = 4;

   private void Start()
   {

     ResetState();

   }



   private void Update()
   {
    if (Input.GetKeyDown(KeyCode.W)){
        _direction = Vector2.up;
    } else if (Input.GetKeyDown(KeyCode.S)){
        _direction = Vector2.down;
    }else if (Input.GetKeyDown(KeyCode.A)){
        _direction = Vector2.left;
    }else if (Input.GetKeyDown(KeyCode.D)){
        _direction = Vector2.right;
    }

    
   }
     
   private void FixedUpdate()
   {  
      for (int i = _segments.Count -1; i > 0; i--){
        _segments[i].position = _segments[i - 1].position;
      }
      this.transform.position = new Vector3(
        Mathf.Round(this.transform.position.x) + _direction.x,
        Mathf.Round(this.transform.position.y) + _direction.y,
        0.0f
      );

      
   }

   private void Grown()
   {
      Transform segment = Instantiate(this.segmentPrefab);
      segment.position = _segments[_segments.Count -1].position;

      _segments.Add(segment);

   }
   private void SetLives(int lives)
    {
        this.lives = lives;
        
       
    }
    
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }
   private void ResetState()
   {     
    snake.gameObject.SetActive(true);  
    gameOverText.enabled = false;
    for (int i =  1; i < _segments.Count; i++ ){
        Destroy(_segments[i].gameObject);

    }

    _segments.Clear();
    _segments.Add(this.transform);

    for(int i = 1; i < this.InitialSize; i++){
        _segments.Add(Instantiate(this.segmentPrefab)); 
    }

    this.transform.position = Vector3.zero;
   SetScore(0);
   SetLives(1);
    
   }
   private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Food"){
            Grown ();
            SetScore(score + 1);
            
       } else if(other.tag == "Obstacle"){
      snake.gameObject.SetActive(false);  
      SetLives(lives - 1);    
      gameOverText.enabled = true;
      Invoke(nameof(ResetState), 1.5f);
        

       } 
       
    }

}
