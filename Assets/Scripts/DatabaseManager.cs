using UnityEngine;
using System.Linq;
using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

public class DatabaseManager : MonoBehaviour
{
    private SQLiteConnection _connection;

    void Awake()
    {
        string dbPath = Application.persistentDataPath + "/GameDB.db";
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        _connection.CreateTable<History>();
        Debug.Log("Banco conectado em: " + dbPath);
    }
    
    void Start()
    {
        // Caminho do banco
        string dbPath = Application.persistentDataPath + "/GameDB.db";

        // Conectar/criar banco
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Banco conectado em: " + dbPath);

        // Criar tabela
        _connection.CreateTable<History>();

    }

    void OnDestroy()
    {
        _connection.Close();
    }

    public void InsertHistory(string name, int score)
    {
        _connection.Insert(new History { Name = name, Score = score });
    }

    public List<History> GetTopRank()
    {
    return _connection.Table<History>()
                      .OrderByDescending(h => h.Score)
                      .Take(5)
                      .ToList();
    }

}
