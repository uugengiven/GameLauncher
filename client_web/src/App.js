import React, { Component } from 'react';
import logo from './newlogo.png';
import './App.css';
import axios from 'axios';
import Game from './game';
import GameUsed from './gameused';
import SETTINGS from './settings';

class App extends Component {
    constructor(props) {
      super(props);
  
      this.state = { 
        games: [],
        status: "ready",
        filterGames: [],
        searchGenre: "",
        errorMessage: "Burb McBurb Burb", 
        errorStatus: "ok", 
        currentGame: {
          name: "Overcooked",
          steamId: 448510, 

        }
    };
      this.get_games = this.get_games.bind(this);
      //you need this when working with functions only
      this.searchFunction = this.searchFunction.bind(this);
      this.get_status = this.get_status.bind(this);
      this.setErrorValues = this.setErrorValues.bind(this); 
      this.get_status();
            
    }

    get_status() {
      axios.get(`${SETTINGS.clientUrl}api/games/status`).then(
        (response) => {
          this.setState({status: response.data.status, currentGame: response.data.game});
          setTimeout(this.get_status, 1000);
        }
      );
    }
  get_games(event) {
    axios.get(`${SETTINGS.serverUrl}game`).then(
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

  setErrorValues(status, message) {
    this.setState({errorStatus: status, errorMessage: message})
  }

  componentWillMount() {
    this.get_games()
  }

  searchFunction(){
    this.state.filterGames = this.state.games;
    var searchResults = this.state.filterGames.filter((variable) => {
      return variable.name.toLowerCase().includes(this.state.searchText.toLowerCase())&& variable.genre.toLowerCase().includes(this.state.searchGenre.toLowerCase());

    })
    //console.log (this.state.searchText)
    this.setState({filterGames: searchResults})

  }

  render() {
    //if this.state.status = ready, then return below
    //else, return whatever status
    //create a function that does the return/ if then for you
    //create a new variable that is going to hold an error message/box
    var errorBox = "" ;
    if (this.state.errorStatus === "failed")
    {
      errorBox = <div className = "errorMessage"> {this.state.errorMessage} <br></br> <button className="okButton" onClick={e => {this.setState({errorStatus: "ok"})}} type="submit">Ok</button></div> 
    }

    if (this.state.status === "ready"){
    return (
      <div className="App">
        <header className="App-header">

          <img src={logo} className="App-logo" alt="logo" />

          <h1 className="App-title">Welcome to LFG's Game Portal </h1>

        </header>

      {
        errorBox
      }

        <h2>
        <input value={this.state.searchText} type="text" onChange={e => {this.setState({searchText: e.target.value},this.searchFunction)}} id="variable" placeholder="Search"></input>
        <span>                     </span>
          <div className="dropdown"><button className="sortBy" onClick={this.myFunction}>Filter by genre</button>
          <div id="myDropdown" className="dropdown-content">
          <input type="text" placeholder="Search Genre" id="myInput" onKeyUp={this.filterFunction}></input>
          <a href="#">Action</a>
          <a href="#">Adventure</a>
          <a href="#">Casual</a>
          <a href="#">Indie</a>
          <a href="#">Massively Multiplier</a>
          <a href="#">Racing</a>
          <a href="#">RPG</a>
          <a href="#">Simulation</a>
          <a href="#">Sports</a>
          <a href="#">Strategy</a>
        </div>
      </div>
        </h2>
        {this.state.filterGames.map((game, index) => {
          return <Game fullGame={game} key={index} setErrorValues={this.setErrorValues} />;
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
          <GameUsed fullGame={this.state.currentGame}/>
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