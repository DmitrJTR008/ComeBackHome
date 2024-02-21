using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIPresenter
{
    public GameUIView  _view;
    public GameUIModel _model;
    public GameUIPresenter(GameUIView view)
    {
        _view = view;
        _model = new GameUIModel(this);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
}
