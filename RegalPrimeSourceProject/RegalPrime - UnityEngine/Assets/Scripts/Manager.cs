using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {


    GameController gameController;

    

    public bool win;

    private int populationSize = 2;
    public int iter = 0;
    private int generationNumber = 0;
    //private int[] layers = new int[] { 1, 10, 10, 1 }; //1 input and 1 output
    private int[] layers = new int[]    {74,/*Input
                                            * [0-1] Character x, character y
                                            * 
                                            * [2-10] North (-y) Distance (clockwise input: 2-5 is left of center, 6 is directly above, 7-10 is to the right)
                                            * [11-19] North Block Type (Corresponds 1:1 to the distance inputs) 
                                            * 
                                            * [20-28] East Distance
                                            * [29-37] East Block Type
                                            * 
                                            * [38-46] South Distance
                                            * [47-55] South Block Type
                                            * 
                                            * [56-64] West Distance
                                            * [66-73] West Block Type
                                            */
                                        100,
                                        50,
                                        25,
                                        3 // [0] Move (-1 = Left, +1 = Right)
                                          // [1] Jump (0 = No Jump, 1 = TIny Jump, 2 = Medium Jump, 3 = Biggest Jump)
                                          // [2] SHoot (Not needed, jsut so it's "there")
                                        };
    public List<NeuralNetwork> nets;
    //private bool leftMouseDown = false;
    // private List<Boomerang> boomerangList = null;


    /*void Timer()
    {
        isTraning = false;
    }*/

    public void Update()
    {
        if (generationNumber == 0)
        {
            InitCharacterNeuralNetworks();
        }
        else
        {
            if (win == false)
                nets[iter].AddFitness(0f);
            else
                nets[iter].AddFitness(1f);

            gameController.OnButtonPressed_ResetScreen();
            nets.Sort();
            for (int x = 0; x < populationSize / 2; x++)
            {
                nets[x] = new NeuralNetwork(nets[x + (populationSize / 2)]);
                nets[x].Mutate();

                nets[x + (populationSize / 2)] = new NeuralNetwork(nets[x + (populationSize / 2)]);
            }
            for (int x = 0; x < populationSize; x++)
            {
                nets[x].SetFitness(0f);
            }

            generationNumber++;
            Invoke("Timer", 1f);

            iter = (iter + 1) % populationSize; //Cycle through the population
            win = false;

        }
    }

    /*void InitBoomerangNeuralNetworks()
    {
        //population must be even, just setting it to 20 incase it's not
        if (populationSize % 2 != 0)
        {
            populationSize = 20; 
        }

        nets = new List<NeuralNetwork>();
        

        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Mutate();
            nets.Add(net);
        }
    }*/

    public void InitCharacterNeuralNetworks()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        win = false;
        nets = new List<NeuralNetwork>();
        for(int x = 0; x < populationSize; x++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Mutate();
            nets.Add(net);
        }
    }

    /*void Update ()
    {
        if (isTraning == false)
        {
            if (generationNumber == 0)
            {
                InitBoomerangNeuralNetworks();
            }
            else
            {
                nets.Sort();
                for (int i = 0; i < populationSize / 2; i++)
                {
                    nets[i] = new NeuralNetwork(nets[i+(populationSize / 2)]);
                    nets[i].Mutate();

                    nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]); //too lazy to write a reset neuron matrix values method....so just going to make a deepcopy lol
                }

                for (int i = 0; i < populationSize; i++)
                {
                    nets[i].SetFitness(0f);
                }
            }

           
            generationNumber++;
            
            isTraning = true;
            Invoke("Timer",15f);
            CreateBoomerangBodies();
        }


        if (Input.GetMouseButtonDown(0))
        {
            leftMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouseDown = false;
        }

        if(leftMouseDown == true)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hex.transform.position = mousePosition;
        }
    }*/


    /* private void CreateBoomerangBodies()
     {
         if (boomerangList != null)
         {
             for (int i = 0; i < boomerangList.Count; i++)
             {
                 GameObject.Destroy(boomerangList[i].gameObject);
             }

         }

         boomerangList = new List<Boomerang>();

         for (int i = 0; i < populationSize; i++)
         {
             Boomerang boomer = ((GameObject)Instantiate(boomerPrefab, new Vector3(UnityEngine.Random.Range(-10f,10f), UnityEngine.Random.Range(-10f, 10f), 0),boomerPrefab.transform.rotation)).GetComponent<Boomerang>();
             boomer.Init(nets[i],hex.transform);
             boomerangList.Add(boomer);
         }

     }*/
}
