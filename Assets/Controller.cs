using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject blockPrefab;
     [SerializeField] private TextMesh score;
     [SerializeField] private TextMesh go;
     [SerializeField] private TextMesh highScore;

    bool _x, _inc;
     private GameObject _camera;
    float x_min , x_max, z_min, z_max;
    private int _block_count;
    private Color[] colors=new Color[]{Color.green,Color.red , Color.yellow,Color.blue,};

    private GameObject block ;
    private int k;
    private int hss;
    void Start()
    {
        _camera=GameObject.Find("Cam");
        block=Instantiate(blockPrefab) as GameObject;
        block.GetComponent<Rigidbody>().useGravity=false;

        block.transform.position=new Vector3(5.5f,0.5f,0);
        block.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        _block_count=0;

        _x=true;
        _inc=false;
        score.text="0";
        k=0;
       go.text="";

       hss=PlayerPrefs.GetInt("Score",0);
        

        x_min=-2.5f;
        x_max=2.5f;
        z_min=-2.5f;
        z_max=2.5f;




        
    }

    void updateDims(GameObject block)
    {
         x_min=block.transform.position.x-block.transform.localScale.x/2;x_max=block.transform.position.x+block.transform.localScale.x/2;
            z_min=block.transform.position.z-block.transform.localScale.z/2;z_max=block.transform.position.z+block.transform.localScale.z/2;


    }

    void gameOver()
    {
        block.GetComponent<Rigidbody>().useGravity=true;
       // Destroy(block.GetComponent<Collider>());
        go.text="Game Over";
        if(Int32.Parse(score.text)>hss)
        hss=Int32.Parse(score.text);
        PlayerPrefs.SetInt("Score", hss);


        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Score", hss);

        highScore.text=hss.ToString();

        if(/*Input.GetMouseButtonDown(0) || */(Input.touchCount>0 && Input.GetTouch(0).phase.Equals(TouchPhase.Began)))
        {


            if(k==1)  ////if touch on game over
            {
                SceneManager.LoadScene("SampleScene");
                
            }
            else{  ////////if game is not over
           
            
         
           if(_x)   /////cut on x
            {
               

                if(block.transform.position.x-block.transform.localScale.x/2<x_min){
                    
                if(CubeCut.Cut_z(block.transform , new Vector3(x_min, block.transform.position.y  ,block.transform.position.z)))
               { GameObject rr=GameObject.Find("right");
                updateDims(rr);
                block=Instantiate(rr) as GameObject;
                Destroy(rr.GetComponent<Rigidbody>());
                block.GetComponent<Rigidbody>().useGravity=false;
                rr.name="block";
                Destroy(GameObject.Find("left").GetComponent<Collider>());
                GameObject.Find("left").name="block";
                block.name="block";
               }
               else{
                   gameOver();
                   k=1;

               }



                }

                else if(block.transform.position.x+block.transform.localScale.x/2>x_max)
                {
                    
               if(CubeCut.Cut_z(block.transform , new Vector3(x_max, block.transform.position.y  ,block.transform.position.z)))
               {
                GameObject rr=GameObject.Find("left");
                 updateDims(rr);
                block=Instantiate(rr) as GameObject;
                Destroy(rr.GetComponent<Rigidbody>());
                block.GetComponent<Rigidbody>().useGravity=false;
                rr.name="block";
                 Destroy(GameObject.Find("right").GetComponent<Collider>());
                GameObject.Find("right").name="block";
                block.name="block";
               }
               else{
                   gameOver();
                   k=1;

               }


                }


                if(k==0)
                {
                 

               block.transform.Translate(0,0.5f,0);
               block.transform.position=new Vector3(block.transform.position.x, block.transform.position.y, 5.5f);
                }
             

            }
            else ///////// on z
           {
               

                if(block.transform.position.z-block.transform.localScale.z/2<z_min){
                    
                if(CubeCut.Cut_x(block.transform , new Vector3(block.transform.position.x, block.transform.position.y  ,z_min)))
                {
                GameObject rr=GameObject.Find("right");
                 updateDims(rr);
                block=Instantiate(rr) as GameObject;
                Destroy(rr.GetComponent<Rigidbody>());
                block.GetComponent<Rigidbody>().useGravity=false;
                rr.name="block";
                 Destroy(GameObject.Find("left").GetComponent<Collider>());
                GameObject.Find("left").name="block";
                block.name="block";
                }
                else{
                    gameOver();
                    k=1;


                }


                }

                else if(block.transform.position.z+block.transform.localScale.z/2>z_max)
                {
                    
               if( CubeCut.Cut_x(block.transform , new Vector3(block.transform.position.x, block.transform.position.y  ,z_max)))
               {
                GameObject rr=GameObject.Find("left");
                 updateDims(rr);
                block=Instantiate(rr) as GameObject;
                Destroy(rr.GetComponent<Rigidbody>());
                block.GetComponent<Rigidbody>().useGravity=false;
                rr.name="block";
                 Destroy(GameObject.Find("right").GetComponent<Collider>());
                GameObject.Find("right").name="block";
                block.name="block";
               }else{
                   gameOver();
                   k=1;



               }


                }

                if(k==0)
                {
                 

               block.transform.Translate(0,0.5f,0);
               block.transform.position=new Vector3(5.5f, block.transform.position.y, block.transform.position.z);
                }

            }


            if(k==0)
            {

            _block_count++;
            score.text=_block_count.ToString();

            block.GetComponent<Renderer>().material.SetColor("_Color", colors[_block_count%4]);


         
            _camera.transform.Translate(0,0.5f, 0, Space.World);
            GameObject.Find("Text").transform.Translate(0,0.5f, 0, Space.World);
            GameObject.Find("goo").transform.Translate(0,0.5f, 0, Space.World);
            GameObject.Find("Best").transform.Translate(0,0.5f, 0, Space.World);
            GameObject.Find("HighScore").transform.Translate(0,0.5f, 0, Space.World);


            _x=!_x;
            }


            }


            
           
        }

        /////////////////////////////////////////
        // handled touch ends here /////////



        /////////////////////////


        if(k==0)
        {



        if(_x)
        {
            if(_inc && block.transform.position.x<5.5f)
                block.transform.Translate(0.1f,0,0);
            else if(!_inc && block.transform.position.x>-5.5f)
                block.transform.Translate(-0.1f,0,0);


            if(block.transform.position.x>=5.5f || block.transform.position.x<=-5.5f)
                _inc=!_inc;


        }


        else
        {
            if(_inc && block.transform.position.z<5.5f)
                block.transform.Translate(0,0,0.1f);
            else if(!_inc && block.transform.position.z>-5.5f)
                block.transform.Translate(0,0,-0.1f);


            if(block.transform.position.z>=5.5f || block.transform.position.z<=-5.5f)
                _inc=!_inc;


        }
        }


        

        


        
    }
}
