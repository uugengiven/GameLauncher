import React, { Component } from 'react';
import logo from './newlogo.png';
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
      this.searchFunction = this.searchFunction.bind(this);
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

  searchFunction(){
    var searchResults = this.state.games.filter((variable) => {
      return variable.name.includes(this.state.searchText)
        
    })
    //console.log (this.state.searchText)
    this.setState({games: searchResults})

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

          <h1 className="App-title">Welcome to LFG's Game Portal </h1>

        </header>
        <h2>
        <input value={this.state.searchText}type="text" onChange={e => this.setState({searchText: e.target.value})} id="variable" placeholder="Search" className="inputBox"></input>
        <button type="submit" className= "search" onClick={this.searchFunction}>Click to Search</button>
        <span>                     </span>
          <button className="sortBy" >Filter by genre</button>
          
        
        </h2>
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