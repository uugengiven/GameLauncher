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
        filterGames: [],
        currentGame: {
          name: "Overcooked",
          steamId: 448510, 
        }
    };
      this.get_games = this.get_games.bind(this);
      //you need this when working with functions only
      this.searchFunction = this.searchFunction.bind(this);
      this.get_status = this.get_status.bind(this);
      this.get_status();
    }

    get_status() {
      axios.get(`http://localhost:8099/api/games/status`).then(
        (response) => {
          this.setState({status: response.data.status, currentGame: response.data.game});
          setTimeout(this.get_status, 1000);
        }
      );
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
        this.setState({games: response.data, filterGames: response.data})   
      }
    ).catch(error => console.warn(`get_games ${error}`));
  }



  componentWillMount() {
    this.get_games()
  }

  searchFunction(){
    this.state.filterGames = this.state.games;
    var searchResults = this.state.filterGames.filter((variable) => {
      return variable.name.toLowerCase().includes(this.state.searchText.toLowerCase());
        
    })
    //console.log (this.state.searchText)
    this.setState({filterGames: searchResults})

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
        <h2>
        <input value={this.state.searchText} type="text" onChange={e => {this.setState({searchText: e.target.value},this.searchFunction)}} id="variable" placeholder="Search"></input>
        <button type="submit" onClick={this.searchFunction}>Click to Search</button>
        <span>                     </span>
          <button className="sortBy" >Filter by genre</button>
          
        </h2>
        {this.state.filterGames.map((game, index) => {
          return <Game fullGame={game} key={index} startGame={this.startGame} />;
        } )
        }
      </div>      
    );
  }
    else if(this.state.status === "running"){
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