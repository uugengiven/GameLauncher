import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import Game from './game';

class App extends Component {
    constructor(props) {
      super(props);
  
      this.state = { 
        games: [],
    };
      this.get_games = this.get_games.bind(this);
    }


  get_games(event) {
    axios.get(`http://localhost:61016/game`).then(
      (response) => {
        this.setState({games: response.data})
      }
    );
  }
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to React</h1>
          <button onClick = {this.get_games}> Game On </button>
        </header>
        {this.state.games.map((game, index) => {
          return <Game fullGame={game} key={index} />;
        } )
        }
      </div>
    );
  }
}

export default App;
