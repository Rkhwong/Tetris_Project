using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playfield : MonoBehaviour
{
    //Score
    private int score;
    public Text scoreText;
    public static int pontos = 100;

    //Criar uma matriz para servir de Malha para limitar a movimentacao e verificar pontuação no jogo, no caso uma Matriz 10x20
    //Criando a Malha
    //Largura
    public static int w = 10;
    public static int h = 20;
    //Criando de forma Trasform, nao precisamos digitar [nome].transform.postion toda hora, e como GameObject ja possui Transform, nao existe problema.
    public static Transform[,] grid = new Transform[w,h];

    private void Start()
    {
        resetScore();
    }
    //Arredondar Vetores
    public static Vector2 roundVec2( Vector2 vetor)
    {
        return new Vector2(Mathf.Round(vetor.x),Mathf.Round(vetor.y));
    }

    //Verificar se a coordenadas esta dentro ou fora do Grid
    //Verifica se a posicao 'x' do objeto nao atravessou o chao, Se esta dentro da Largura do Grid 'w' , e se esta acima do Chao 'y'
    public static bool insideBorder(Vector2 posicao)
    {
        return
            (
                (int)posicao.x >= 0 &&
                (int)posicao.x < w &&
                (int)posicao.y >= 0
            );
    }

    //Funcao de Deletar a Linha quando Feita em Y
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; x++ )
        {
            //Deleta todos os objetos na linha Y de x 0 a W
            Destroy(grid[x, y].gameObject);
            //Completa com Vazio
            grid[x, y] = null;
        }

        Playfield linha = FindObjectOfType<Playfield>();
        linha.Score(pontos);
    }

    //Todas os Objetos a cima da linha devem descer 1 de acordo com Y
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //Desce as linhas
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    //Verifica se a Linha esta Cheia
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    //Funcao de Deletar as Linhas
    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    //Funcao dos pontos
    public void Score(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString();
    }

    public void resetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
