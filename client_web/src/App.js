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
        status: "ready",
        currentGame: {
          name: "Overcooked",
          steamId: 448510 ,

        }
    };
      this.get_games = this.get_games.bind(this);
      //you need this when working with functions only
    }

  get_games(event) {
    axios.get(`http://localhost:61016/game`).then(
      (response) => {
        response.data.sort((a, b) => {
          if (a.name > b.name) 
          {
            return 1; 
          }

          if (a.name === b.name) 
          {
            return 0; 
          }

            return -1; 
        })
        this.setState({games: response.data})   
      }
    ).catch(error => console.warn(`get_games ${error}`));
  }



  componentWillMount() {
    this.get_games()
  }

  render() {
    //if this.state.status = ready, then return below
    //else, return whatever status
    //create a function that does the return/ if then for you
    if (this.state.status === "ready"){
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to LFG! Please select a game!</h1>
        </header>
        {this.state.games.map((game, index) => {
          return <Game fullGame={game} key={index} startGame={this.startGame} />;
        } )
        }
      </div>      
    );
  }
    else if(this.state.status === "anything else"){
      return (
        <div className="App">
          <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">You are currently playing {this.state.currentGame.name}</h1>
          <Game fullGame={this.state.currentGame}/>
        </header>
      </div>
    );
  }
    else if(this.state.status === "not connected"){
      return (
        <div className="App">
          <header className="App-header">
            <img src={logo} className="App-logo" alt="logo" />
            <h1 className="App-title">This computer is not connected.</h1>
          </header>
        </div>
      );
    }

  }
}

 export default App;