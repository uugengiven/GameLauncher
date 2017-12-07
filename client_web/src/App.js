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
        status: "not connected",
        filterGames: [],
        searchGenre: "",
        searchText: "",
        errorMessage: "", 
        errorStatus: "ok", 
        searchText:"",
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

  // myFunction(event) {
  //   this.setState({searchGenre: event.target.value});
  // }

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
      errorBox = <div className = "errorMessage"> <div> {this.state.errorMessage} <br></br> <button className="okButton" onClick={e => {this.setState({errorStatus: "ok"})}} type="submit">Ok</button></div></div> 
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
        <div className="dropdown">
        
        <label>
          <select value={this.state.searchGenre} className="sortBy" onChange={e => {this.setState({searchGenre: e.target.value},this.searchFunction)}}>
          
          <option value="">Filter by Genre</option>
            <option value="Action">Action</option>
            <option value="Adventure">Adventure</option>
            <option value="Casual">Casual</option>
            <option value="Indie">Indie</option>
            <option value="Massively Multiplier">Massively Multiplier</option>
            <option value="Racing">Racing</option>
            <option value="RPG">RPG</option>
            <option value="Simulation">Simulation</option>
            <option value="Sports">Sports</option>
            <option value="Strategy">Strategy</option>
            <option value="">Clear Filter</option>
          </select>
          </label>
          
        
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
            <h1 className="App-title">This computer is not connected. Please reach out to Ed. </h1>
          </header>
        </div>
      );
    }

  }
}

 export default App;