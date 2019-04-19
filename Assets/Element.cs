using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    public bool mine;

    //variables de textura
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    // Start is called before the first frame update
    void Start()
    {
        //esta funcion decide si es una mina o no
        
        mine = Random.value < 0.15; //Random.value devuelve un numero aleatori entre 0 y 1 queremos la probabilidad de 15% de que un elemento sea una mina

        //registro de cada elemento en la cuadricula 
        int x = (int)transform.position.x;
        int y = (int)transform.position.y; //el transform.position x,y es de tio flotante asi que los convertimos a int antes de usarlos
        Grid.elements[x, y] = this;
       
    }

    //uso de las variables Sprite con la funcion de loadTexture
public void loadTexture(int adjacentCount)
    {
        //cambiamos la textura
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture; 
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];

    }

    //funcion que compara el nombre de la textura actual con el determinado
    public bool isCovered(){
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    //funcion para detectar los clics del mouse
    private void OnMouseUpAsButton()
    {
        //si es una mina
        if (mine)
        {
            //todaslas demas minas deberan revelarse
            Grid.uncoverMines();

            //el juego acaba
            print("you lose");
        }
        //sino es una mina 
        else
        {
            //muestra el numero de minas adyacente
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Grid.adjacentMines(x, y));

            //descubrir todos los elementos vacios cuando el usuario haga clic en uno 
            Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);

            //averiguar si se descubrieron todos los elementos sin minas si se ganó el juego
            if (Grid.isFinished())
                print("you win");
        }
    }


}
