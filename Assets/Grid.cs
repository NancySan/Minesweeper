using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    //la cuadricula debe seguir todos los elementos del juego usaremos una matriz bidimensional para esto.
    public static int w = 10; //anchura
    public static int h = 13; //altura
    public static Element[,] elements = new Element[w, h]; // se crea una matriz bidimensional con el ancho de10 y altura de 13 (10*13) elementos

   //descubriendo todas las minas (revisar cada elemento, averiguar si es mina y cargar la textura de la mina 
   public static void uncoverMines()
    {
        foreach(Element elem in elements)
        {
            if (elem.mine)
                elem.loadTexture(0);
        }
    }

    //funcion que verifica si hay una mina en una posicion determinada
    public static bool mineAt(int x,int y)
    {
        if(x >= 0 && y >= 0 && x < w && y < h) //tenemos que verificar si las coordenadas estan dentro del rango de la matriz de elementos
            return elements[x, y].mine;
        return false;

    }

    //funcion para las minas adyacentes con las coordenadas (x, y) como paramentros y el contador que se devolvera
    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x, y + 1)) ++count;  //parte superior
        if (mineAt(x + 1, y + 1)) ++count;  //parte superior derecha
        if (mineAt(x + 1, y)) ++count;  //derecha
        if (mineAt(x + 1, y - 1)) ++count; //parte inferior derecha
        if (mineAt(x, y - 1)) ++count;  //inferior
        if (mineAt(x - 1, y - 1)) ++count;  //inferior izquierda
        if (mineAt(x -1, y)) ++count;  //izquierda
        if (mineAt(x - 1, y + 1)) ++count;  //parte superior izquierda
         
        return count;
    }
    //Flood Flll para descubrir automaticamente todo el area de elementos sin minas adyacentes
    public static void FFuncover(int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        { //aseguramos que nuestro algoritmo no visite un elemento fuera de la cuadricula comprobando si las oordenadas x e y estan entre 0 y el ancho o la altura


            if (visited[x, y])
                return;

            //descubrir elemento que visita
            elements[x, y].loadTexture(adjacentMines(x, y));

            //no debe continuar cuando un elemento esta cerca de una mina
            if (adjacentMines(x, y) > 0)
                return;

            visited[x, y] = true;

            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited); // la varaible visited es una matriz 2D querealiza un seguimiento de si el algoritmo ya visitó 
            FFuncover(x, y - 1, visited); //un elemento determinado
            FFuncover(x, y + 1, visited);//el algoritmo comienza en algun elemento y luego continua con los elementos en la partesuperior, derecha, inferior y a la izquierda de ese elemento de forma recursiva hasta que visita cada elemento
        }
    }

    //averiguar si cada elemento que aun no fue descubierto es una mina
    public static bool isFinished()
    {
        foreach (Element elem in elements)
            if (elem.isCovered() && elem.mine) //encoontrar un elemento que este cubierto y no sea mina, devuelve falseporque todavia hay trabajo por hacerpor el usuario, sino encuentra dicho elemento devuelve verdadero porque el juego fue ganado
                return false;
        //no hay ninguno ==> todos son minas ==> juego ganado
        return true;
    }
}
