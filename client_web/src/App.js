import React, { Component } from 'react';
import logo from './lfglogo.svg.jpg';
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
        response.data.sort((a, b) => {
          if (a.name > b.name) 
          {
            return 1; 
          }

          if (a.name == b.name) 
          {
            return 0; 
          }

            return -1; 
        })
        this.setState({games: response.data})   
      }
    );
  }

  componentWillMount() {
    this.get_games()
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to LFG! Please select a game!</h1>
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